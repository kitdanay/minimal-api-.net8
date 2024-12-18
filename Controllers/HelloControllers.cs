using Microsoft.AspNetCore.Mvc;
using System.Data; // ใช้สำหรับ DataTable
using System.Linq; // ใช้สำหรับ LINQ

[ApiController]
[Route("api/[controller]")]
public class HISController : ControllerBase
{
    // ตัวอย่าง GET: api/his/patient?hn=12345
    [HttpGet("patient")]
    public IActionResult GetPatient(string hn)
    {
        try
        {
            // สร้าง DataTable จำลองข้อมูลจาก HIS
            var dt = GetMockPatientData(hn);

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (dt == null || dt.Rows.Count == 0)
            {
                return NotFound($"No patient found with HN: {hn}");
            }

            var result = DataTableHelper.ConvertDataTableToList(dt);
            var response = ResponseHelper.CreateSuccessResponse(result, "Data Successfully.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorResponse = ResponseHelper.CreateErrorResponse<object>("An error occurred: " + ex.Message);
            return StatusCode(500, errorResponse);
        }
    }

    // ฟังก์ชันจำลองการดึงข้อมูลจาก HIS
    private DataTable GetMockPatientData(string hn)
    {
        var dt = new DataTable();
        dt.Columns.Add("HN", typeof(string));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("DOB", typeof(DateTime));
        dt.Columns.Add("Address", typeof(string));

        // ตัวอย่างข้อมูลผู้ป่วย
        dt.Rows.Add("12345", "John Doe", new DateTime(1985, 5, 20), "123 Main St");
        dt.Rows.Add("67890", "Jane Smith", new DateTime(1990, 8, 15), "456 Oak Ave");

        // ค้นหาเฉพาะข้อมูลตาม HN
        return dt.AsEnumerable().Where(row => row["HN"].ToString() == hn).CopyToDataTable();
    }
}
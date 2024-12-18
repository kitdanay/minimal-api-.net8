using Microsoft.AspNetCore.Mvc;
using System.Data; // ใช้สำหรับ DataTable
using System.Linq; // ใช้สำหรับ LINQ

[ApiController]
[Route("api/[controller]")]
public class HISController : ControllerBase
{


    // [HttpGet("patient")]
    // public IActionResult GetPatient([FromQuery] PatientRequest patientRequest)
    // {
    //     try
    //     {
    //         // ตรวจสอบว่าข้อมูล HN ถูกต้องหรือไม่
    //         if (!ModelState.IsValid)
    //         {
    //             return BadRequest(ModelState);
    //         }

    //         // สร้าง DataTable จำลองข้อมูลจาก HIS
    //         var dt = GetMockPatientData(patientRequest.Hn);

    //         // ตรวจสอบว่ามีข้อมูลหรือไม่
    //         if (dt == null || dt.Rows.Count == 0)
    //         {
    //             return NotFound($"No patient found with HN: {patientRequest.Hn}");
    //         }

    //         var result = DataTableHelper.ConvertDataTableToList(dt);
    //         var response = ResponseHelper.CreateSuccessResponse(result, "Data Successfully.");

    //         return Ok(response);
    //     }
    //     catch (Exception ex)
    //     {
    //         var errorResponse = ResponseHelper.CreateErrorResponse<object>("An error occurred: " + ex.Message);
    //         return StatusCode(500, errorResponse);
    //     }
    // }




    [HttpGet("patient")]
    public IActionResult GetPatient([FromQuery] PatientRequest patientRequest)
    {
        try
        {
            // ตรวจสอบว่า ModelState ถูกต้องหรือไม่
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // ส่งคืน BadRequest หากมีข้อผิดพลาดใน ModelState
            }

            // สร้าง DataTable จำลองข้อมูลจาก HIS
            var dt = GetMockPatientData(patientRequest.Hn, patientRequest.Vn);

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (dt == null || dt.Rows.Count == 0)
            {
                var chkData = ResponseHelper.CreateSuccessResponse<object>(null, "No data found.");
                return Ok(chkData);
            }

            var result = DataTableHelper.ConvertDataTableToList(dt);
            var response = ResponseHelper.CreateSuccessResponse(result, "Data Successfully.");

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = ResponseHelper.CreateErrorResponse<object>("An error occurred: " + ex.Message);
            return StatusCode(500, errorResponse);
        }
    }






    // ฟังก์ชันจำลองการดึงข้อมูลจาก HIS
    private DataTable GetMockPatientData(string hn, string vn)
    {
        var dt = new DataTable();
        dt.Columns.Add("HN", typeof(string));
        dt.Columns.Add("VN", typeof(string));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("DOB", typeof(DateTime));
        dt.Columns.Add("Address", typeof(string));

        // ตัวอย่างข้อมูลผู้ป่วย
        dt.Rows.Add("1234567890", "0001", "John Doe", new DateTime(1985, 5, 20), "123 Main St");
        dt.Rows.Add("1234567890", "0021", "John Doe", new DateTime(1985, 5, 20), "123 Main St");
        dt.Rows.Add("0123456789", "0071", "Jane Smith", new DateTime(1990, 8, 15), "456 Oak Ave");

        // การกรองข้อมูลตาม HN และ VN
        var filteredRows = dt.AsEnumerable().Where(row => row["HN"].ToString() == hn);

        // หากมีการกรอก VN เข้ามา ก็ให้กรองตาม VN ด้วย
        if (!string.IsNullOrEmpty(vn))
        {
            filteredRows = filteredRows.Where(row => row["VN"].ToString() == vn);
        }

        // หากไม่พบแถวที่ตรงเงื่อนไข ให้คืนค่าผลลัพธ์ที่เป็น DataTable เปล่า
        if (filteredRows.Any())
        {
            return filteredRows.CopyToDataTable();
        }
        else
        {
            // คืนค่า DataTable เปล่า ถ้าไม่พบข้อมูล
            return new DataTable();
        }
    }


}
using System.ComponentModel.DataAnnotations;


public class PatientRequest
{
    [Required(ErrorMessage = "HN is required.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "HN must be a 10-digit number.")]
    public string Hn { get; set; }

    // VN เป็น Optional หากกรอกต้องเป็น 4 หลัก
    [RegularExpression(@"^\d{4}$", ErrorMessage = "VN must be a 4-digit number.")]
    public string? Vn { get; set; }  // ทำให้ Vn เป็น nullable
}

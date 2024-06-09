namespace PTS.Core.Dto
{
    public class SignUpDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public DateTime Dateofbirth { get; set; }
        public int Status { get; set; } = 0;
        public int? IdRole { get; set; }
    }
}
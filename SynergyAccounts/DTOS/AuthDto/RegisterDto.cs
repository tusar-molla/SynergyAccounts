namespace SynergyAccounts.DTOS.AuthDto
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int? ContactNumber { get; set; }
        public int? RoleId { get; set; } = 1;
    }
}

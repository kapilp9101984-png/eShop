namespace Auth.Application.DTO
{
    public class UserDTO : BaseEntityDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public  string Email { get; set; }
        public  string Password { get; set; }
        public  string UserName { get; set; }
        public  string MobileNumber { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsEmailVerified { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime LockoutEnd { get; set; }
    }
}

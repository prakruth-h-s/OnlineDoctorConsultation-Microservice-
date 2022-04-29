namespace ODC.UserManagement.Model
{
    public class LoginResponse : BaseResponse
    {
        public UserDetails UserDetails { get; set; }
    }

    public class UserDetails
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}

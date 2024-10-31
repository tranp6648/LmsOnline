namespace LMSOnline.Data
{
    public class AccountDTO
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string FullName { get; set; } = null!;
        public string Role { get; set; }
        public string? Avatar { get; set; }
    }
}

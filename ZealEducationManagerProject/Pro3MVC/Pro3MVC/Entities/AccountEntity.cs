namespace Pro3MVC.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public bool Status { get; set; }
        public string? Role { get; set; }
        public string? Fullname { get; set; }
    }
}

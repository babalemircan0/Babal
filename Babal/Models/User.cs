namespace Babal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }    // Bu satırın olduğundan emin ol
        public string Password { get; set; } // Bu satırın olduğundan emin ol
    }
}
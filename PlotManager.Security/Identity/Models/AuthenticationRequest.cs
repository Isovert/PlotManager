namespace PlotManager.Security.Identity.Models
{
    public class AuthenticationRequest
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

}
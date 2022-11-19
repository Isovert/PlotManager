namespace PlotManager.Security.Identity.Models
{
    public class SignInResult
    {
        public bool Succeeded { get; protected set; }
        public List<string>? Errors { get; private set; }
        public static SignInResult Success { get; } = new SignInResult() { Succeeded = true };
        public static SignInResult Failed(List<string> errors)
        {
            return new SignInResult()
            { Succeeded = false, Errors = errors };
        }
    }
}

namespace PlotManager.Security.Identity.Models
{
    public class UserManagerResult
    {
        public bool Succeeded { get; protected set; }
        public List<string>? Errors { get; private set; }
        public static UserManagerResult Success { get; } = new UserManagerResult() { Succeeded = true };
        public static UserManagerResult Failed(List<string> errors)
        {
            return new UserManagerResult()
            { Succeeded = false, Errors = errors };
        }
    }
}
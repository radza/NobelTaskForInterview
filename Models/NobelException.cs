namespace NobelTaskForInterview.Models
{
    public class NobelException : Exception
    {
        public NobelException(string message) : base(message) 
        {
            
        }

        public NobelException() : base()
        {

        }
    }
}

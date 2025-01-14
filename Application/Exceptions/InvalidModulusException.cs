namespace Application.Exceptions
{
    public class InvalidModulusException : Exception
    {
        public InvalidModulusException()
            : base($"Value of the modulus must be greater than zero.")
        {
        }
    }
}

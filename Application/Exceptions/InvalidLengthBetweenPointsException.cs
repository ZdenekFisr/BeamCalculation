namespace Application.Exceptions
{
    public class InvalidLengthBetweenPointsException : Exception
    {
        public InvalidLengthBetweenPointsException()
            : base($"Length between points must be greater than zero.")
        {
        }
    }
}

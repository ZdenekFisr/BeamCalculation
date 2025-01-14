namespace Application.Exceptions
{
    public class InvalidBeamLengthException : Exception
    {
        public InvalidBeamLengthException()
            : base($"Beam length must be greater than zero.")
        {
        }
    }
}

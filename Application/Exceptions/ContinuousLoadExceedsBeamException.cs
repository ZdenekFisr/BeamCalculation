namespace Application.Exceptions
{
    public class ContinuousLoadExceedsBeamException : Exception
    {
        public ContinuousLoadExceedsBeamException()
            : base($"At least one continuous load exceeds the beam length.")
        {
        }
    }
}

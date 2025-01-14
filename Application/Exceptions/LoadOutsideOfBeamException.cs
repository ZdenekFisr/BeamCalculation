namespace Application.Exceptions
{
    public class LoadOutsideOfBeamException : Exception
    {
        public LoadOutsideOfBeamException()
            : base($"At least one load is outside of the beam.")
        {
        }
    }
}

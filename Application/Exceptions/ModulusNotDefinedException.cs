namespace Application.Exceptions
{
    public class ModulusNotDefinedException : Exception
    {
        public ModulusNotDefinedException()
            : base($"Modulus is not defined for the entire length of the beam.")
        {
        }
    }
}

namespace HospitalApi.Exceptions;

public class ToManyMedicamentsException : Exception
{
    public ToManyMedicamentsException()
    {
    }

    public ToManyMedicamentsException(string message)
        : base(message)
    {
    }
}
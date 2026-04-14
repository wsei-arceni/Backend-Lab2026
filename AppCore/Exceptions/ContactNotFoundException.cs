namespace AppCore.Exceptions;

public class ContactNotFoundException: Exception
{
    //TODO: Where should be implemented `throw new ContactNotFoundException($"Person with id={id} not found!");`
    public ContactNotFoundException(string msg): base(msg)
    {
    }
}
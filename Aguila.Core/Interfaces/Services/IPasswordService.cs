namespace Aguila.Core.Interfaces.Services
{
    public interface IPasswordService
    {
        string Hash(string password);

        bool Check(string Hash, string password);

    }

}

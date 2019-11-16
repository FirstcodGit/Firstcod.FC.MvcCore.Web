using Firstcod.FC.Provider.IRepositories;

namespace Firstcod.FC.Provider
{
    public interface IUnitOfWork
    {
        ITransactionRepositories Transaction { get; }
    }
}

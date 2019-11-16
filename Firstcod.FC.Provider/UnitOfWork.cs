using Firstcod.FC.Provider.IRepositories;
using Firstcod.FC.Provider.Repositories;

namespace Firstcod.FC.Provider
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        ITransactionRepositories transactionRepositories;

        public ITransactionRepositories Transaction
        {
            get
            {
                if (transactionRepositories == null)
                    transactionRepositories = new TransactionRepositories(_context);

                return transactionRepositories;
            }
        }
    }
}

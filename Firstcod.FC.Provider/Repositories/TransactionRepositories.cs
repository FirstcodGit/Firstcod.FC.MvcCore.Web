using Firstcod.FC.Provider.IRepositories;
using Firstcod.FC.Provider.Models;
using Firstcod.FC.Provider.Providers;
using Microsoft.EntityFrameworkCore;

namespace Firstcod.FC.Provider.Repositories
{
    public class TransactionRepositories : GenericRepositories<Transaction>, ITransactionRepositories
    {
        public TransactionRepositories(DbContext context) 
            :base(context) { }

        private ApplicationDbContext _applicationDbContext => (ApplicationDbContext)_context;
    }
}

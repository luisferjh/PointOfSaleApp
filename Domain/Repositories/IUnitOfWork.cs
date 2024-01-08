using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWork: IDisposable
    {       
        IUnitOfWorkRepositories UnitOfWorkRepositories { get; }       
        void Save();
        void Rollback();
        void Dispose();
    }
}

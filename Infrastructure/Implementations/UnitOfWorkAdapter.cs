using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class UnitOfWorkAdapter : IUnitOfWorkAdapter
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkAdapter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork Create()
        {
            return _unitOfWork;
        }
    }
}

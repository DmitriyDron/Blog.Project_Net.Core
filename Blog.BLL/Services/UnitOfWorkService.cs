using Blog.BLL.Interfaces;
using Blog.DAL.Interfaces.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> TrySaveChangesAsync()
        {
            try
            {
                await unitOfWork.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
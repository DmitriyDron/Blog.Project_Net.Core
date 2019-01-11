using Blog.DAL.Entities;
using Blog.DAL.Entities.Roles;
using Blog.DAL.Entities.Users;
using Blog.DAL.Interfaces.Repositories;
using Blog.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        Task<int> Commit();

        void Rollback();
    }
}
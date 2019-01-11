using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IUnitOfWorkService
    {
        /// <summary>
        /// Сохраняет все изменения, запланированные другими сервисами.
        /// </summary>
        /// <returns>ture eсли сохранение прошло успешно, иначе false</returns>
        Task<bool> TrySaveChangesAsync();
    }
}
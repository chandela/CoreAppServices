using CoreApp.Models.CoreModels.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.IRepositories
{
#pragma warning disable CS0436 // Type conflicts with imported type
    public partial interface IRepository<T> where T : BaseEntity
#pragma warning restore CS0436 // Type conflicts with imported type
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(long id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Persist(T entity);
    }
}

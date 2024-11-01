using LivroMente.Domain.Models;
using LivroMente.Domain.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace LivroMente.Service.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : Entity
    {
       Task<bool> Add(TEntity entity);
       Task<IEnumerable<TEntity>> GetAll();
       Task<bool> Update(Guid id);
       Task<bool> Delete(Guid id);
       Task<TEntity> GetById(Guid id);
       Task<bool> Save();

    }
}
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Domain.Commons;
using ShopiyfyX.Domain.Configurations;
using ShopiyfyX.Domain.Entities;

namespace ShopiyfyX.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly string Path;

    public Repository()
    {
        if(typeof(User) == typeof(TEntity))
        {
            this.Path = DatabasePath.UserDb;
        }
        else if(typeof(Product) == typeof(TEntity))
        {
            this.Path = DatabasePath.ProductDb;
        }
        else if(typeof(Category) == typeof(TEntity))
        {
            this.Path= DatabasePath.CategoryDb;
        }
        else
        {
            this.Path = DatabasePath.OrderDb;
        }

        var str = File.ReadAllText(Path);
        if (string.IsNullOrEmpty(str))
            File.WriteAllText(Path, "[]");
    }
    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> SelectAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> SelectByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}

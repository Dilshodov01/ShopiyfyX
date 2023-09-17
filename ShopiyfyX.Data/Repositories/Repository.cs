using Newtonsoft.Json;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Domain.Commons;
using ShopiyfyX.Domain.Configurations;
using ShopiyfyX.Domain.Entities;
using System.Xml;

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
    public async Task<bool> DeleteAsync(long id)
    {
        var data = await SelectAllAsync();

        var item = data.FirstOrDefault(x => x.Id == id);
        data.Remove(item);

        var str = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
        await File.WriteAllTextAsync(Path, str);

        return true;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var data = await SelectAllAsync();
        var item = data.FirstOrDefault(e => e.Id == entity.Id);
        if (item == null)
        {
            data.Add(entity);
            return entity;
        }
        else
        {
            return null;
        }

    }

    public async Task<List<TEntity>> SelectAllAsync()
    {
        var str = await File.ReadAllTextAsync(Path);
        var data = JsonConvert.DeserializeObject<List<TEntity>>(str);

        return data;

    }

    public async Task<TEntity> SelectByIdAsync(long id)
    {
        var data = await SelectAllAsync();
        var item = data.FirstOrDefault(x => x.Id == id);

        return item;

    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var data = await SelectAllAsync();

        await File.WriteAllTextAsync(Path, "[]");
        foreach (var item in data)
        {
            if (item.Id == entity.Id)
            {
                await InsertAsync(entity);
                continue;
            }
            await InsertAsync(item);
        }

        return entity;
    }
}

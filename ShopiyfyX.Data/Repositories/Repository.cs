using Newtonsoft.Json;
using ShopiyfyX.Domain.Commons;
using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Domain.Configurations;



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
        var entities = await SelectAllAsync();
        var entity = entities.FirstOrDefault(e => e.Id == id);
        entities.Remove(entity);
        var str = JsonConvert.SerializeObject(entities, Newtonsoft.Json.Formatting.Indented);
        await File.WriteAllTextAsync(Path, str);
        return true;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        string str = await File.ReadAllTextAsync(Path);
        List<TEntity> entities = JsonConvert.DeserializeObject<List<TEntity>>(str);
        entities.Add(entity);
        string result = JsonConvert.SerializeObject(entities, Newtonsoft.Json.Formatting.Indented);
        await File.WriteAllTextAsync(Path, result);

        return entity;
    }

    public async Task<List<TEntity>> SelectAllAsync()
    {
        var str = await File.ReadAllTextAsync(Path);
        var entities = JsonConvert.DeserializeObject<List<TEntity>>(str);
        return entities;
    }

    public async Task<TEntity> SelectByIdAsync(long id)
    {
        return (await SelectAllAsync()).FirstOrDefault(e => e.Id == id);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entities = await SelectAllAsync();
        await File.WriteAllTextAsync(Path, "[]");

        foreach (var data in entities)
        {
            if (data.Id == entity.Id)
            {
                await InsertAsync(entity);
                continue;
            }
            await InsertAsync(data);
        }

        return entity;
    }
}

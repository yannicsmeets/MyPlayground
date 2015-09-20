using Microsoft.Framework.DependencyInjection;
using MyPlayground.Context;
using System;
using System.Threading.Tasks;

namespace MyPlayground.Services
{
  public class BaseService
  {
    private readonly IServiceProvider serviceProvider;
    protected readonly MyPlaygroundDbContext db;

    public BaseService(MyPlaygroundDbContext db, IServiceProvider serviceProvider)
    {
      this.db = db;
      this.serviceProvider = serviceProvider;
    }

    public T GetService<T>()
    {
      return serviceProvider.GetService<T>();
    }

    public async Task Commit()
    {
      await db.SaveChangesAsync();
    }
  }
}

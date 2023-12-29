using Blog.Models;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;

public class Query
{
  public async Task<BlogPage?> GetBlogPageAsync(
    [ID] int id,
    [Service] BlogDbContext database,
    IResolverContext context
  )
  {
    var res = await database.BlogPages.FindAsync(id);
    if (res == null)
    {
      context.ReportError($"Could not find blog page with ID {id}");
    }
    return database.BlogPages.Find(id);
  }

  public async Task<IList<BlogPage?>?> GetBlogPagesAsync(
    [ID] int[] ids,
    [Service] BlogDbContext database,
    IResolverContext context
  )
  {
    var dic = await database.BlogPages.Where(x => ids.Contains(x.Id)).ToDictionaryAsync(i => i.Id);
    return ids.Select(id =>
    {
      if (dic.TryGetValue(id, out var blogPage))
      {
        return blogPage;
      }
      else
      {
        context.ReportError($"Could not find blog page with ID {id}");
        return null;
      }
    }).ToList();
  }
}

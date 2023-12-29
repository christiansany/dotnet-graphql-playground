using Blog.Models;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;

public class Query
{
  public async Task<BlogPage?> GetBlogPageByIdAsync(
    [ID] int id,
    BlogPageBatchDataLoader dataLoader,
      IResolverContext context
  )
  {
    var res = await dataLoader.LoadAsync(id);
    // TODO: Check if the error could be handled better
    if (res == null)
    {
      context.ReportError($"Could not find blog page with ID {id}");
    }
    return res;
  }

  public async Task<IList<BlogPage?>?> GetBlogPagesByIdAsync(
    [ID] int[] ids,
    BlogPageBatchDataLoader dataLoader
  ) => await Task.WhenAll(ids.Select(id => dataLoader.LoadAsync(id)));


  [UsePaging(MaxPageSize = 10, IncludeTotalCount = true, DefaultPageSize = 10)]
  public IEnumerable<BlogPage> GetBlogPages(
    [Service] BlogDbContext database
  ) => database.BlogPages;

  public async Task<Author?> GetAuthorByIdAsync(
    [ID] int id,
    [Service] BlogDbContext database,
    IResolverContext context
  )
  {
    var res = await database.Authors.FindAsync(id);
    if (res == null)
    {
      context.ReportError($"Could not find author with ID {id}");
    }
    return res;
  }

  public async Task<IList<Author?>?> GetAuthorsByIdAsync(
    [ID] int[] ids,
    [Service] BlogDbContext database,
    IResolverContext context
  )
  {
    var dic = await database.Authors.Where(x => ids.Contains(x.Id)).ToDictionaryAsync(i => i.Id);
    return ids.Select(id =>
    {
      if (dic.TryGetValue(id, out var Author))
      {
        return Author;
      }
      else
      {
        context.ReportError($"Could not find author with ID {id}");
        return null;
      }
    }).ToList();
  }

  [UsePaging(MaxPageSize = 10, IncludeTotalCount = true, DefaultPageSize = 10)]
  public IEnumerable<Author> GetAuthors(
    [Service] BlogDbContext database
  )
  {
    return database.Authors;
  }
}

using Blog.Models;
using HotChocolate.Execution;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;

public class Query
{
  public User? GetUserById(
    [ID] int id,
    [Service] BlogDbContext database
  )
  {
    // This is probably fing horrible
    return database.Users
      .Include(u => u.LikedAuthors)
      .Include(u => u.LikedBlogPages)
      .FirstOrDefault(u => u.Id == id);
  }

  public User? GetViewer()
  {
    return new User(1, "Viewer");
  }

  public async Task<BlogPage?> GetBlogPageByIdAsync(
    [ID] int id,
    BlogPageBatchDataLoader dataLoader
  ) => await dataLoader.LoadAsync(id);

  public async Task<IList<BlogPage?>?> GetBlogPagesByIdAsync(
    [ID] int[] ids,
    BlogPageBatchDataLoader dataLoader,
    IResolverContext context
  )
  {
    // Add security from malicous requests
    if (ids.Length > 10)
    {
      throw new GraphQLException(ErrorBuilder.New()
        .SetMessage("Too many ids provided")
        .SetCode("BAD_REQUEST")
        .SetPath(context.Path)
        .Build());
    }
    return await Task.WhenAll(ids.Select(id => dataLoader.LoadAsync(id)));
  }

  [UsePaging(MaxPageSize = 10, IncludeTotalCount = true, DefaultPageSize = 10)]
  public IEnumerable<BlogPage> GetBlogPages(
    [Service] BlogDbContext database
  ) => database.BlogPages;

  public async Task<Author?> GetAuthorByIdAsync(
    [ID] int id,
    [Service] BlogDbContext database,
    IResolverContext context
  ) => await Author.GetAuthorAsync(id, database, context);

  public async Task<IList<Author?>?> GetAuthorsByIdAsync(
    [ID] int[] ids,
    [Service] BlogDbContext database,
    IResolverContext context
  )
  {
    // Add security from malicous requests
    if (ids.Length > 10)
    {
      // QueryException is the same GraphQLException 🤷‍♂️
      throw new QueryException(ErrorBuilder.New()
        .SetMessage("Too many ids provided")
        .SetCode("BAD_REQUEST")
        .SetPath(context.Path)
        .Build());
    }
    return await Task.WhenAll(ids.Select(id => Author.GetAuthorAsync(id, database, context)));
  }

  [UsePaging(MaxPageSize = 10, IncludeTotalCount = true, DefaultPageSize = 10)]
  public IEnumerable<Author> GetAuthors(
    [Service] BlogDbContext database
  ) => database.Authors;
}

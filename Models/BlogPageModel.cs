using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
  [Node]
  public class BlogPage
  {
    [ID]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    [GraphQLIgnore]
    public int AuthorId { get; set; }

    public BlogPage(int id, string title, string content, int authorId)
    {
      if (string.IsNullOrEmpty(title))
        throw new ArgumentException("Title is required", nameof(title));

      if (string.IsNullOrEmpty(content))
        throw new ArgumentException("Content is required", nameof(content));

      Id = id;
      Title = title;
      Content = content;
      AuthorId = authorId;
    }

    public async Task<Author?> GetAuthorAsync(
    [Service] BlogDbContext database) => await database.Authors.FindAsync(AuthorId);

    public static async Task<BlogPage?> GetBlogPageAsync(
      int id,
      BlogPageBatchDataLoader dataLoader
    )
    {
      return await dataLoader.LoadAsync(id);
    }
  }

  public class BlogPageBatchDataLoader : BatchDataLoader<int, BlogPage>
  {
    private readonly BlogDbContext _database;

    public BlogPageBatchDataLoader(
      [Service] BlogDbContext database,
      IBatchScheduler batchScheduler,
      DataLoaderOptions? options = null)
      : base(batchScheduler, options)
    {
      _database = database;
    }

    protected override async Task<IReadOnlyDictionary<int, BlogPage>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
      Console.WriteLine($"Loading {keys.Count} blog pages from the database.");
      return await _database.BlogPages.Where(x => keys.Contains(x.Id)).ToDictionaryAsync(i => i.Id);
    }
  }

  [Node]
  public class Author
  {
    [ID]
    public int Id { get; set; }
    public string Name { get; set; }

    public Author(int id, string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentException("Name is required", nameof(name));

      Id = id;
      Name = name;
    }

    [UsePaging(MaxPageSize = 10, IncludeTotalCount = true, DefaultPageSize = 10)]
    public async Task<IEnumerable<BlogPage>> GetBlogPagesAsync(
      [Service] BlogDbContext database) => await database.BlogPages.Where(x => x.AuthorId == Id).ToListAsync();
  }

  public class BlogDbContext : DbContext
  {
    public BlogDbContext(DbContextOptions options) : base(options) { }
    public DbSet<BlogPage> BlogPages { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
  }
}

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

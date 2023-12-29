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

    public Author GetAuthor() => new Author(1, "John Doe");
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
  }

  public class BlogDbContext : DbContext
  {
    public BlogDbContext(DbContextOptions options) : base(options) { }
    public DbSet<BlogPage> BlogPages { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
  }
}

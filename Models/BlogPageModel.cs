using HotChocolate.Types.Relay;
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

    public BlogPage(int id, string title, string content)
    {
      if (string.IsNullOrEmpty(title))
        throw new ArgumentException("Title is required", nameof(title));

      if (string.IsNullOrEmpty(content))
        throw new ArgumentException("Content is required", nameof(content));

      Id = id;
      Title = title;
      Content = content;
    }
  }

  class BlogPageDb : DbContext
  {
    public BlogPageDb(DbContextOptions options) : base(options) { }
    public DbSet<BlogPage> BlogPages { get; set; } = null!;
  }
}
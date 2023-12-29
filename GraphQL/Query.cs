using Blog.Models;

public class Query
{
  public BlogPage? GetBlogPage([ID] int id)
  {
    return new BlogPage(
      id: id,
      title: "C# in depth.",
      content: "C# in depth is a book about the C# language and the core .NET platform."
      );
  }

  public IList<BlogPage?>? GetBlogPages([ID] int[] ids)
  {
    return ids
      .Select(id => new BlogPage(
        id: id,
        title: "C# in depth.",
        content: "C# in depth is a book about the C# language and the core .NET platform."
      ))
      .ToList();
  }
}
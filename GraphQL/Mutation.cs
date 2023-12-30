using Blog.Models;
using Microsoft.EntityFrameworkCore;

public class Mutation
{
  // Could be outsourced into a separate file, or even a nuget package for reuse
  public class NoViewerException : Exception
  {
    public NoViewerException()
      : base($"Viewer not authenticated")
    {
    }
  }

  [Error(typeof(NoViewerException))]
  public async Task<BlogPage?> LikeBlogPageAsync(
    [ID] int id,
    [Service] IHttpContextAccessor httpContextAccessor,
    [Service] BlogDbContext database
  )
  {
    // How would it look like to make this reusable in .NET?
    var headers = httpContextAccessor.HttpContext?.Request.Headers;
    int.TryParse(headers?["User"].ToString(), out int userId);

    if (userId == 0)
      throw new NoViewerException();

    var user = database.Users
      .Include(x => x.LikedBlogPages)
      .FirstOrDefault(x => x.Id == userId);
    var blogPage = database.BlogPages
      .Include(x => x.LikedByUsers)
      .FirstOrDefault(x => x.Id == id);

    if (blogPage?.LikedByUsers == null || user?.LikedBlogPages == null)
    {
      // Probably should throw an error here 🤷‍♂️
      return null;
    }

    user.LikedBlogPages.Add(blogPage);
    blogPage.LikedByUsers.Add(user);

    // Save changes to the database
    await database.SaveChangesAsync();

    return blogPage;
  }

  [Error(typeof(NoViewerException))]
  public async Task<BlogPage?> UnlikeBlogPageAsync(
    [ID] int id,
    [Service] IHttpContextAccessor httpContextAccessor,
    [Service] BlogDbContext database
  )
  {
    // How would it look like to make this reusable in .NET?
    var headers = httpContextAccessor.HttpContext?.Request.Headers;
    int.TryParse(headers?["User"].ToString(), out int userId);

    if (userId == 0)
      throw new NoViewerException();

    var user = database.Users
      .Include(x => x.LikedBlogPages)
      .FirstOrDefault(x => x.Id == userId);
    var blogPage = database.BlogPages
      .Include(x => x.LikedByUsers)
      .FirstOrDefault(x => x.Id == id);

    if (blogPage?.LikedByUsers == null || user?.LikedBlogPages == null)
    {
      // Probably should throw an error here 🤷‍♂️
      return null;
    }

    user.LikedBlogPages.Remove(blogPage);
    blogPage.LikedByUsers.Remove(user);

    // Save changes to the database
    await database.SaveChangesAsync();

    return blogPage;
  }
}

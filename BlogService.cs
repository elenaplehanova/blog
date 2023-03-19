using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

internal class BlogService
{
    internal static object NumberOfCommentsPerUser(MyDbContext context) =>
       context.BlogComments.GroupBy(b => b.UserName).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();


    internal static object NumberOfLastCommentsLeftByUser(MyDbContext context) =>
        context.BlogPosts.Select(b => new
        {
            LastComment = b.Comments.OrderBy(c => c.CreatedDate).Select(x => new { x.UserName }).Last()
        }).GroupBy(l => l.LastComment.UserName).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();

    internal static object PostsOrderedByLastCommentDate(MyDbContext context) =>
        context.BlogPosts.Select(b => new
        {
            b.Title,
            LastComment = b.Comments.OrderBy(c => c.CreatedDate).Select(x => new { Date = x.CreatedDate.ToString("yyyy-MM-dd"), x.Text }).Last()
        }).ToList();
}

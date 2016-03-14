﻿using MoM.Blog.Models;
using MoM.Module.Interfaces;
using System.Collections.Generic;

namespace MoM.Blog.Interfaces
{
    public interface IPostRepository : IDataRepository
    {
        IEnumerable<Post> All();
        IEnumerable<Post> Posts(int pageNo, int pageSize);
        IEnumerable<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
        IEnumerable<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        IEnumerable<Post> PostsForSearch(string search, int pageNo, int pageSize);
        int TotalPosts(bool checkIsPublished = true);
        int TotalPostsForCategory(string categorySlug);
        int TotalPostsForTag(string tagSlug);
        int TotalPostsForSearch(string search);
        IEnumerable<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
        Post Post(int year, int month, string titleSlug);
        Post Post(int id);
        Post AddPost(Post post);
        Post EditPost(Post post);
        void DeletePost(int id);  
    }
}

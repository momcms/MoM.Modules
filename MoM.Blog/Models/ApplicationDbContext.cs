﻿using Microsoft.EntityFrameworkCore;

namespace MoM.Blog.Models
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ModelBuilderFactory.BuildModels(modelBuilder);
        }
    }
}

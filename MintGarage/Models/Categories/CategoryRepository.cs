﻿using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private MintGarageContext context;
        public CategoryRepository(MintGarageContext ctx)
        {
            context = ctx;
            context.Category.Add(new Category() {Name = "test" }) ;
            context.SaveChanges();
        }
        public IQueryable<Category> Categories => context.Category;
    }
}

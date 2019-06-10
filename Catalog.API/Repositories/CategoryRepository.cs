using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.ViewModels.CategoryViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListCategoryViewModel> Get()
        {
            return _context
                .Categories
                .Select(x => new ListCategoryViewModel
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .AsNoTracking()
                .ToList();
        }

        public Category Get(Guid id)
        {
            return _context.Categories.Find(id);
        }

        public void Save(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.Remove(category);
            _context.SaveChanges();
        }
    }
}
using Catalog.API.Data;
using Catalog.API.Models;
using Catalog.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace Catalog.API.Test.Repositories
{
    public class CatalogRepositoryTest
    {
        [Fact]
        public void Add_writes_to_database()
        {
            var options = new DbContextOptionsBuilder<StoreDataContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            using (var context = new StoreDataContext(options))
            {
                var repository = new CategoryRepository(context);

                var category = new Category()
                {
                    Title = "Nova Categoria",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                repository.Save(category);
            }

            using (var context = new StoreDataContext(options))
            {
                var repository = new CategoryRepository(context);
                var categories = repository.Get();

                Assert.Single(categories);
                Assert.Equal("Nova Categoria", context.Categories.AsNoTracking().FirstOrDefault().Title);
            }
        }

        [Fact]
        public void Find_by_id()
        {
            var options = new DbContextOptionsBuilder<StoreDataContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            var category = new Category()
            {
                Title = "Nova Categoria",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            using (var context = new StoreDataContext(options))
            {
                var repository = new CategoryRepository(context);

                repository.Save(category);
            }

            using (var context = new StoreDataContext(options))
            {
                var repository = new CategoryRepository(context);
                var newCategory = repository.Get(category.Id);

                Assert.Equal(category.Id, newCategory.Id);
            }
        }

        [Fact]
        public void Delete_by_id()
        {
            var options = new DbContextOptionsBuilder<StoreDataContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            var category = new Category()
            {
                Title = "Nova Categoria",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            using (var context = new StoreDataContext(options))
            {
                var repository = new CategoryRepository(context);

                repository.Save(category);
            }

            using (var context = new StoreDataContext(options))
            {
                var repository = new CategoryRepository(context);

                repository.Delete(category);

                var deletedCategory = repository.Get(category.Id);

                Assert.Null(deletedCategory);
            }
        }
    }
}
using Catalog.API.Controllers;
using Catalog.API.Data;
using Catalog.API.Repositories;
using Catalog.API.ViewModels.CategoryViewModel;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Catalog.API.Test.Controllers
{
    public class CategoryControllerTest
    {
        private readonly CategoryRepository _repository;
        private readonly CategoryController _controller;

        public CategoryControllerTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreDataContext>()
                .UseInMemoryDatabase("category_database_test");

            var context = new StoreDataContext(optionsBuilder.Options);

            _repository = new CategoryRepository(context);
            _controller = new CategoryController(_repository);
        }

        [Fact]
        public void Categories_Post_Get_ReturnsOkResponse()
        {
            var category = new EditCategoryViewModel()
            {
                Title = "Categoria Teste"
            };

            var responsePost = _controller.Post(category);
            var response = _controller.Get();

            Assert.True(responsePost.Success);
            Assert.Single(response);
        }

        [Fact]
        public void Categories_GetById_CategoriesReturnsOkResponse()
        {
            var response = _controller.Get(Guid.NewGuid());
            response.Should().BeNull();
        }
    }
}
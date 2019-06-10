using Catalog.API.Models;
using Catalog.API.Repositories;
using Catalog.API.ViewModels;
using Catalog.API.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Catalog.API.Controllers
{
    [Route("api")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/categories")]
        [HttpGet]
        [ResponseCache(Duration = 1)]
        public IEnumerable<ListCategoryViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/categories/{id:guid}")]
        [HttpGet]
        public Category Get(Guid id)
        {
            return _repository.Get(id);
        }

        [Route("v1/categories")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditCategoryViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar a categoria",
                    Data = model.Notifications
                };

            var category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.Now
            };

            _repository.Save(category);

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria cadastrada com sucesso",
                Data = category
            };
        }

        [Route("v1/categories")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditCategoryViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar a categoria",
                    Data = model.Notifications
                };

            var category = _repository.Get(model.Id);

            if (category == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Categoria não encontrada",
                    Data = category
                };
            }

            category.Title = model.Title;
            category.UpdatedAt = DateTime.Now;

            _repository.Update(category);

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria alterada com sucesso",
                Data = category
            };
        }

        [Route("v1/categories/{id:guid}")]
        [HttpDelete]
        public ResultViewModel Delete(Guid id)
        {
            var category = _repository.Get(id);

            if (category == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Categoria não encontrada",
                    Data = category
                };
            }

            _repository.Delete(category);

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria excluída com sucesso",
                Data = category
            };
        }
    }
}
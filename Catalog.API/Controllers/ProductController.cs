using Catalog.API.Models;
using Catalog.API.Repositories;
using Catalog.API.ViewModels;
using Catalog.API.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Catalog.API.Controllers
{
    [Route("api")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public ProductController(ProductRepository productRepository, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [Route("v1/products")]
        [HttpGet]
        [ResponseCache(Duration = 1)]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _productRepository.Get();
        }

        [Route("v1/products/{id:guid}")]
        [HttpGet]
        public Product Get(Guid id)
        {
            return _productRepository.Get(id);
        }

        [Route("v1/products")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = model.Notifications
                };

            var category = _categoryRepository.Get(model.CategoryId);

            if (category == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Categoria não encontrada",
                    Data = category
                };
            }

            var product = new Product
            {
                Title = model.Title,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now,
                Description = model.Description,
                UpdatedAt = DateTime.Now,
                Price = model.Price,
                Quantity = model.Quantity
            };

            _productRepository.Save(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso",
                Data = product
            };
        }

        [Route("v1/products")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar o produto",
                    Data = model.Notifications
                };

            var product = _productRepository.Get(model.Id);

            if (product == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Produto não encontrado",
                    Data = product
                };
            }

            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.UpdatedAt = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _productRepository.Update(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso",
                Data = product
            };
        }

        [Route("v1/products/{id:guid}")]
        [HttpDelete]
        public ResultViewModel Delete(Guid id)
        {
            var product = _productRepository.Get(id);

            if (product == null)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Produto não encontrado",
                    Data = product
                };
            }

            _productRepository.Delete(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto excluído com sucesso",
                Data = product
            };
        }
    }
}
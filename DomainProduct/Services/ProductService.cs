using AutoMapper;
using Domain.Utils.HttpStatusExceptionCustom;
using DomainProduct.Interfaces.IRepositorys;
using DomainProduct.Interfaces.IServices;
using DomainProduct.Views.ProductViews;
using EntitieProduct;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainProduct.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepositorie;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepositorie, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepositorie = productRepositorie;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductView> Add(ProductAddView produto)
        {
            const string situacao = "Ativo";

            if (produto.DataFabricacao >= produto.DataValidade)
            {
                throw new HttpStatusExceptionCustom(StatusCodeEnum.NotAcceptable, "A data de fabricação não pode ser maior ou igual à data de validade.");
            }
            if (produto.DataValidade <= DateTime.Now)
            {
                throw new HttpStatusExceptionCustom(StatusCodeEnum.NotAcceptable, "A data de validade não pode ser maior ou igual à data atual.");
            }

            var novoProduto = _mapper.Map<Product>(produto);

            novoProduto.Situacao = situacao;

            await _productRepositorie.Add(novoProduto);

            var mapReturnProduct = _mapper.Map<ProductView>(novoProduto);

            return mapReturnProduct;
        }

        public async Task<ProductView> Update(ProductUpdateView produto)
        {

            if (produto.DataFabricacao >= produto.DataValidade)
            {
                throw new HttpStatusExceptionCustom(StatusCodeEnum.NotAcceptable, "A data de fabricação não pode ser maior ou igual à data de validade.");
            }
            if (produto.DataValidade <= DateTime.Now)
            {
                throw new HttpStatusExceptionCustom(StatusCodeEnum.NotAcceptable, "A data de validade não pode ser maior ou igual à data atual.");
            }

            var mapProduct = _mapper.Map<Product>(produto);

            var getProduct = await GetByCode(produto.Codigo);

            if (getProduct == null)
            {
                throw new HttpStatusExceptionCustom(StatusCodeEnum.Conflict, "Falha ao Atualizar o Produto (Produto não encontrado).");
            }

            mapProduct.Situacao = getProduct.Situacao;

            await _productRepositorie.Update(mapProduct);

            var mapReturnProduct = _mapper.Map<ProductView>(mapProduct);

            return mapReturnProduct;
        }

        public Task<string> Delete(int code)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteProduct(int code)
        {
            var returnResult = await _productRepositorie.DeleteProduct(code);

            if (returnResult.Situacao == "Inativo")
            {
                return $"Produto {returnResult.Situacao}";
            }
            return $"Falha ao Inativar Produto {returnResult.Codigo}";
        }

        public async Task<List<ProductView>> ListAllProductByPage(int pageNumber, int pageSize)
        {
            var returnListProduct = await _productRepositorie.ListAllProductByPage(pageNumber, pageSize);
            var productMap = _mapper.Map<List<ProductView>>(returnListProduct);
            return productMap;
        }

        public async Task<List<ProductView>> ListProductByDateManufactureWhitPagination(DateTime startDate, DateTime endDate, int pageNumber, int pageSize)
        {
            var returnListProduct = await _productRepositorie.ListProductByDateManufactureWhitPagination(startDate, endDate, pageNumber, pageSize);
            var productMap = _mapper.Map<List<ProductView>>(returnListProduct);
            return productMap;
        }

        public async Task<ProductView> GetByCode(int code)
        {
            var returnProduct = await _productRepositorie.GetByCode(code);

            var mapProduct = _mapper.Map<ProductView>(returnProduct);

            return mapProduct;

        }

        public async Task<List<ProductView>> GetAll()
        {
            var product = await _productRepositorie.GetAll();

            var returnMapProduct = _mapper.Map<List<ProductView>>(product);

            return returnMapProduct;
        }

        public async Task<ProductView> GetById(int id)
        {
            var product = await _productRepositorie.GetById(id);

            var returnMapProduct = _mapper.Map<ProductView>(product);

            return returnMapProduct;
        }

    }
}

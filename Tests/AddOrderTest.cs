using AutoMapper;
using Domain.Utils.HttpStatusExceptionCustom;
using DomainProduct.Interfaces.IRepositorys;
using DomainProduct.Services;
using DomainProduct.Views.ProductViews;
using EntitieProduct;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Domain.Test.OrderServicesTests
{
    public class AddOrderTest
    {
        [TestFixture]
        public class OrderServicesTestes
        {
            private ProductService _productServices;
            private Mock<IProductRepository> _repositoryMock;
            private Mock<IMapper> _mapperMock;
            private Mock<ILogger<ProductService>> _loggerMock;

            [SetUp]
            public void Setup()
            {
                _repositoryMock = new Mock<IProductRepository>();
                _mapperMock = new Mock<IMapper>();
                _loggerMock = new Mock<ILogger<ProductService>>();

                _productServices = new ProductService(_repositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
            }

            [Test]
            public async Task AddNewProduct()
            {
                // Arrange
                var objeto = new ProductAddView {
                CodigoFornecedor = "1234567",
                CNPJFornecedor = "1234567890",
                DataFabricacao = DateTime.Now.AddDays(-10),
                DataValidade = DateTime.Now.AddDays(20),
                Descricao = "TesteMock",
                DescricaoFornecedor = "TesteMock"
                };

                var order = new Product {};
                var productView = new ProductView {};

                _mapperMock.Setup(m => m.Map<Product>(objeto)).Returns(order);
                _repositoryMock.Setup(r => r.Add(order)).Returns(Task.CompletedTask);
                _mapperMock.Setup(m => m.Map<ProductView>(order)).Returns(productView);

                // Act
                var resultado = await _productServices.Add(objeto);

                // Assert
                Assert.That(resultado, Is.EqualTo(productView));
            }

            [Test]
            public void AddNewProduct_DeveLancarHttpStatusExceptionCustom_Quando_DataFabricaçãoNaoSerMaiorIgualDataValidade()
            {
                // Arrange
                var objeto = new ProductAddView
                {
                    CodigoFornecedor = "1234567",
                    CNPJFornecedor = "1234567890",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now,
                    Descricao = "TesteMock",
                    DescricaoFornecedor = "TesteMock"
                };

                // Act & Assert
                Assert.ThrowsAsync<HttpStatusExceptionCustom>(async () => await _productServices.Add(objeto));
            }

            [Test]
            public void AddNewProduct_DeveLancarHttpStatusExceptionCustom_QuandoDataValidadeIgualOuMenorDataAtual()
            {
                // Arrange
                var objeto = new ProductAddView
                {
                    CodigoFornecedor = "1234567",
                    CNPJFornecedor = "1234567890",
                    DataFabricacao = DateTime.Now.AddDays(-10),
                    DataValidade = DateTime.Now,
                    Descricao = "TesteMock",
                    DescricaoFornecedor = "TesteMock"
                };

                // Act & Assert
                Assert.ThrowsAsync<HttpStatusExceptionCustom>(async () => await _productServices.Add(objeto));
            }
        }
    }
}

using System;
using _Project.Scripts.MVP.Model;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Systems
{
    public class PresenterFactory : IDisposable
    {
        private readonly Product _product;
        private readonly CurrencyRepository _repository;
        private readonly ProductBuyer _productBuyer;
        private ProductPresenter _productPresenter;

        [Inject]
        public PresenterFactory(Product product, CurrencyRepository repository, ProductBuyer productBuyer = default)
        {
            _product = product;
            _repository = repository;
            _productBuyer = productBuyer;
        }

        public ProductPresenter Create()
        {
            _productPresenter = new ProductPresenter(_product, _repository, _productBuyer);
            return _productPresenter;
        }

        public void Dispose()
        {
            _productPresenter.Dispose();
        }
    }
}
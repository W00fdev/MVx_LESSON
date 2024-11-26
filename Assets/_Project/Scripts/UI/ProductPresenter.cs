using System;
using System.Collections.Generic;
using _Project.Scripts.MVP.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using static _Project.Scripts.UI.IProductPresenter;

namespace _Project.Scripts.UI
{
    [Serializable]
    public class ProductPresenter : IProductPresenter, IInitializable, IDisposable
    {
        [ShowInInspector] private Product _product;

        private readonly CurrencyRepository _repository;
        private readonly ProductBuyer _productBuyer;
        private readonly List<PriceElement> _priceElements = new();

        private bool _isButtonEnabled;

        public Action OnStateChanged { get; set; }
        public Action<bool> OnBuyButtonEnabled { get; set; }

        public IReadOnlyList<IPriceElement> PriceElements => _priceElements;

        [ShowInInspector] public string ProductName => _product ? _product.Name : string.Empty;

        [ShowInInspector] public string ProductDescription => _product ? _product.Description : string.Empty;

        [ShowInInspector] public string Price => _product ? _product.Price[0].Amount.ToString() : string.Empty;

        [ShowInInspector] public Sprite Icon => _product ? _product.IconSprite : null;

        [ShowInInspector] public bool IsButtonEnabled => _isButtonEnabled;

        private bool CanBuy()
        {
            return _product && _productBuyer.CanBuy(_product);
        }

        [Inject]
        public ProductPresenter(Product product, CurrencyRepository repository, ProductBuyer productBuyer = default)
        {
            _repository = repository;
            _productBuyer = productBuyer;

            ChangeProduct(product);
        }

        public void OnBuyClicked()
        {
            _productBuyer.Buy(_product);
        }

        public void Initialize()
        {
            _isButtonEnabled = CanBuy();
            _repository.OnStateChanged += OnMoneyChanged;
        }

        [Button]
        public void ChangeProduct(Product product)
        {
            if (product == null)
                return;

            if (_product != product)
            {
                _product = product;
                _isButtonEnabled = CanBuy();
                OnBuyButtonEnabled?.Invoke(IsButtonEnabled);
                UpdatePrice();
                OnStateChanged?.Invoke();
            }
        }

        private void UpdatePrice()
        {
            _priceElements.Clear();

            if (_product == null)
                return;

            int count = _product.Price.Count;
            for (var i = 0; i < count; i++)
                _priceElements.Add(new PriceElement(this, i));
        }

        private void OnMoneyChanged()
        {
            bool canBuy = CanBuy();
            if (canBuy != _isButtonEnabled)
            {
                _isButtonEnabled = canBuy;
                OnBuyButtonEnabled?.Invoke(IsButtonEnabled);
            }
        }

        public void Dispose()
        {
            _repository.OnStateChanged -= OnMoneyChanged;
        }

        private sealed class PriceElement : IPriceElement
        {
            private readonly ProductPresenter _parent;
            private readonly int _index;

            public Sprite Icon => _parent._repository.GetStorage(GetPrice().Type).Icon;
            public int Price => GetPrice().Amount;

            public PriceElement(ProductPresenter parent, int index)
            {
                _parent = parent;
                _index = index;
            }

            private CurrencyData GetPrice()
            {
                return _parent._product.Price[_index];
            }
        }
    }
}
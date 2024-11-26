using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace _Project.Scripts.MVP.Model
{
    public class CurrencyRepository : IInitializable, IDisposable
    {
        private readonly Dictionary<CurrencyType, CurrencyStorage> _repository;

        public event Action OnStateChanged;

        [Inject]
        public CurrencyRepository(CurrencyStorage[] storages)
        {
            _repository = storages.ToDictionary(it => it.Type);
        }

        public CurrencyStorage GetStorage(CurrencyType type)
        {
            return _repository[type];
        }

        public IEnumerator<CurrencyStorage> GetEnumerator()
        {
            return _repository.Values.GetEnumerator();
        }

        public bool IsEnough(Product product)
        {
            foreach (CurrencyData currencyProduct in product.Price)
            {
                int currentCurrencyAmount = GetStorage(currencyProduct.Type).Currency;
                if (currentCurrencyAmount < currencyProduct.Amount)
                    return false;
            }

            return true;
        }

        public void Spend(IReadOnlyList<CurrencyData> productPrice)
        {
            foreach (CurrencyData currencyProduct in productPrice)
            {
                CurrencyStorage storage = GetStorage(currencyProduct.Type);
                storage.SpendCurrency(currencyProduct.Amount);
            }
        }

        public void Initialize()
        {
            foreach (var pair in _repository)
                pair.Value.OnStateChanged += StateChanged;
        }

        public void Dispose()
        {
            foreach (var pair in _repository)
                pair.Value.OnStateChanged -= StateChanged;
        }

        private void StateChanged()
        {
            OnStateChanged?.Invoke();
        }
    }
}
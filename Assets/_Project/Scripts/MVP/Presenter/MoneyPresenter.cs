using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.MVP.Presenter
{
    public sealed class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly CurrencyView _currencyView;

        [Inject]
        public MoneyPresenter(MoneyStorage moneyStorage, CurrencyView currencyView)
        {
            _moneyStorage = moneyStorage;
            _currencyView = currencyView;

            Func<int, string> format = i => $"{i}$";
            _currencyView.SetupCurrency(_moneyStorage.Money, format);
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneyEarned += OnMoneyEarned;
            _moneyStorage.OnMoneySpent += OnMoneySpent;
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyEarned -= OnMoneyEarned;
            _moneyStorage.OnMoneySpent -= OnMoneySpent;
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyEarned(int newValue, int range)
        {
            _currencyView.EarnCurrency(newValue - range, range);
        }

        private void OnMoneySpent(int newValue, int range)
        {
            _currencyView.SpendCurrency(newValue + range, -range);
        }

        private void OnMoneyChanged(int previousValue, int newValue)
        {
            _currencyView.ChangeCurrency(previousValue, newValue - previousValue);
        }
    }
}
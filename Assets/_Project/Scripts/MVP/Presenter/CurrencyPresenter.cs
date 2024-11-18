using System;
using _Project.Scripts.MVP.Model;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.MVP.Presenter
{
    public class CurrencyPresenter
    {
        private readonly CurrencyStorage _currencyStorage;
        private readonly CurrencyView _currencyView;

        public CurrencyView View => _currencyView;
        public CurrencyStorage Storage => _currencyStorage;

        public CurrencyPresenter(CurrencyStorage currencyStorage, CurrencyView currencyView)
        {
            _currencyStorage = currencyStorage;
            _currencyView = currencyView;

            Func<int, string> format = i => $"{i}";
            _currencyView.SetupCurrency(_currencyStorage.Currency, _currencyStorage.Icon, format);

            Subscribe();
        }

        private void Subscribe()
        {
            _currencyStorage.OnCurrencyEarned += OnCurrencyEarned;
            _currencyStorage.OnCurrencySpent += OnCurrencySpent;
            _currencyStorage.OnCurrencyChanged += OnCurrencyChanged;
        }

        public void Dispose()
        {
            _currencyStorage.OnCurrencyEarned -= OnCurrencyEarned;
            _currencyStorage.OnCurrencySpent -= OnCurrencySpent;
            _currencyStorage.OnCurrencyChanged -= OnCurrencyChanged;
        }

        private void OnCurrencyEarned(int newValue, int range)
        {
            _currencyView.EarnCurrency(newValue - range, range);
        }

        private void OnCurrencySpent(int newValue, int range)
        {
            _currencyView.SpendCurrency(newValue + range, -range);
        }

        private void OnCurrencyChanged(int previousValue, int newValue)
        {
            _currencyView.ChangeCurrency(previousValue, newValue - previousValue);
        }
    }
}
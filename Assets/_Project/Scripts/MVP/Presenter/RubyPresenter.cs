using System;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.MVP.Presenter
{
    public sealed class RubyPresenter : IInitializable, IDisposable
    {
        private readonly RubyStorage _rubyStorage;
        private readonly CurrencyView _currencyView;

        [Inject]
        public RubyPresenter(RubyStorage rubyStorage, CurrencyView currencyView)
        {
            _rubyStorage = rubyStorage;
            _currencyView = currencyView;

            Func<int, string> format = i => $"{i}";
            _currencyView.SetupCurrency(_rubyStorage.Ruby, format);
        }

        public void Initialize()
        {
            _rubyStorage.OnRubyEarned += OnRubyEarned;
            _rubyStorage.OnRubySpent += OnRubySpent;
            _rubyStorage.OnRubyChanged += OnRubyChanged;
        }

        public void Dispose()
        {
            _rubyStorage.OnRubyEarned -= OnRubyEarned;
            _rubyStorage.OnRubySpent -= OnRubySpent;
            _rubyStorage.OnRubyChanged -= OnRubyChanged;
        }

        private void OnRubyEarned(int newValue, int range)
        {
            _currencyView.EarnCurrency(newValue - range, range);
        }

        private void OnRubySpent(int newValue, int range)
        {
            _currencyView.SpendCurrency(newValue + range, -range);
        }

        private void OnRubyChanged(int previousValue, int newValue)
        {
            _currencyView.ChangeCurrency(previousValue, newValue - previousValue);
        }
    }
}
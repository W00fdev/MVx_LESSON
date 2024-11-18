using _Project.Scripts.MVP.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using TriInspector;

namespace _Project.Scripts.CompositionRoot
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private CurrencyView _moneyView;
        [SerializeField] private CurrencyView _rubyView;

        [SerializeField] private int _startMoney = 100;
        [SerializeField] private int _startRuby = 10;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MoneyStorage>(Lifetime.Scoped).WithParameter(_startMoney);
            builder.Register<RubyStorage>(Lifetime.Scoped).WithParameter(_startRuby);

            builder.RegisterEntryPoint<MoneyPresenter>().WithParameter(_moneyView);
            builder.RegisterEntryPoint<RubyPresenter>().WithParameter(_rubyView);
        }
    }
}
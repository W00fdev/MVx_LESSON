using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.CompositionRoot
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private int _startMoney = 100;   
        [SerializeField] private int _startRuby = 10;   
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MoneyStorage>(Lifetime.Scoped).WithParameter(_startMoney);
            builder.Register<RubyStorage>(Lifetime.Scoped).WithParameter(_startRuby);
        }
    }
}
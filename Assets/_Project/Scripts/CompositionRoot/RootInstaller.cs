using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.CompositionRoot
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private int _startMoney = 100;   
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MoneyStorage>(Lifetime.Singleton).WithParameter(_startMoney);

        }
    }
}
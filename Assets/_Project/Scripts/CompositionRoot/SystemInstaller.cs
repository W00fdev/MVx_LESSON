using _Project.Scripts.MVP;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.CompositionRoot
{
    public class SystemInstaller : LifetimeScope
    {
        [SerializeField] private int _money = 100;
        
        protected override void Configure(IContainerBuilder builder)
        {
            //builder.Register<MoneyStorage>(Lifetime.Singleton);
        }
    }
}
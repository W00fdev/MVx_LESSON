using _Project.Scripts.MVP.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.CompositionRoot
{
    public class SystemInstaller : MonoInstaller
    {
        [SerializeField] [ShowInInspector] private CurrencyStorage[] _storages;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CurrencyRepository>()
                .AsSingle()
                .WithArguments(_storages)
                .NonLazy();

            Container.Bind<ProductBuyer>().AsSingle();
        }
    }
}
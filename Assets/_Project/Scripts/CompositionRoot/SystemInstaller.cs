using _Project.Scripts.MVP.Model;
using _Project.Scripts.Systems;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.CompositionRoot
{
    public class SystemInstaller : MonoInstaller
    {
        [SerializeField] [ShowInInspector] private CurrencyStorage[] _storages;
        [SerializeField] [ShowInInspector] private Lootbox _lootbox;


        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CurrencyRepository>()
                .AsSingle()
                .WithArguments(_storages)
                .NonLazy();

            Container.Bind<ProductBuyer>().AsSingle();

            Container.BindInterfacesAndSelfTo<Lootbox>()
                .FromInstance(_lootbox)
                .AsSingle()
                .NonLazy();

            Container.Bind<LootboxConsumer>()
                .AsSingle()
                .NonLazy();
        }
    }
}
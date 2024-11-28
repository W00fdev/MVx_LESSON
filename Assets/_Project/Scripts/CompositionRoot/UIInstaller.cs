using _Project.Scripts.Lootboxes;
using _Project.Scripts.MVP.Presenter;
using _Project.Scripts.MVP.View;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.CompositionRoot
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private CurrencyListView _listView;
        [SerializeField] private LootboxView _lootboxView;

        [SerializeField] private Product _initialProduct;
        [SerializeField] private ProductPopup _productPopup;

        public override void InstallBindings()
        {
            Container.Bind<ProductPopup>().FromInstance(_productPopup)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<CurrencyRepositoryPresenter>()
                .AsCached()
                .WithArguments(_listView)
                .NonLazy();

            Container.BindInterfacesAndSelfTo<ProductPresenter>()
                .AsSingle()
                .WithArguments(_initialProduct)
                .NonLazy();

            Container.BindInterfacesTo<LootboxPresenter>()
                .AsSingle()
                .WithArguments(_lootboxView)
                .NonLazy();
        }
    }
}
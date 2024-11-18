using _Project.Scripts.MVP.Model;
using _Project.Scripts.MVP.Presenter;
using _Project.Scripts.MVP.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using TriInspector;

namespace _Project.Scripts.CompositionRoot
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private CurrencyStorage[] _storages;
        [SerializeField] private CurrencyListView _listView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CurrencyRepository>(Lifetime.Scoped).WithParameter(_storages);
            builder.RegisterEntryPoint<CurrencyRepositoryPresenter>().WithParameter(_listView);
        }
    }
}
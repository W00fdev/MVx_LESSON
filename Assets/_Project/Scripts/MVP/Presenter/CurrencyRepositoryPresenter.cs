using System;
using System.Collections.Generic;
using _Project.Scripts.MVP.Model;
using _Project.Scripts.MVP.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.MVP.Presenter
{
    public class CurrencyRepositoryPresenter : IInitializable, IDisposable
    {
        private readonly CurrencyRepository _currencyRepository;
        private readonly CurrencyListView _currencyListView;

        private readonly List<CurrencyPresenter> _presenters;

        [Inject]
        public CurrencyRepositoryPresenter(CurrencyRepository currencyRepository, CurrencyListView currencyListView)
        {
            _currencyListView = currencyListView;
            _currencyRepository = currencyRepository;

            _presenters = new List<CurrencyPresenter>();
        }

        public void Initialize()
        {
            foreach (CurrencyStorage currencyStorage in _currencyRepository)
            {
                CurrencyView view = _currencyListView.SpawnView();
                var presenter = new CurrencyPresenter(currencyStorage, view);
                _presenters.Add(presenter);
            }
        }

        public void Dispose()
        {
            foreach (CurrencyPresenter presenter in _presenters)
            {
                presenter.Dispose();
                _currencyListView.UnspawnView(presenter.View);
            }
        }
    }
}
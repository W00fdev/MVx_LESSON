using System;
using _Project.Scripts.MVP.Model;
using _Project.Scripts.Systems;
using Zenject;

namespace _Project.Scripts.Lootboxes
{
    public class LootboxPresenter : IInitializable, IDisposable
    {
        private Lootbox _lootbox; // model
        private LootboxConsumer _lootboxConsumer; // model
        private CurrencyRepository _repository;
        private LootboxView _view; // passive-view

        [Inject]
        public LootboxPresenter(Lootbox model, LootboxView view, LootboxConsumer lootboxConsumer,
            CurrencyRepository repository)
        {
            _lootbox = model;
            _view = view;
            _lootboxConsumer = lootboxConsumer;
            _repository = repository;
        }

        public void Initialize()
        {
            _view.SetIcon(_lootbox.Icon);
            UpdateRemainingTime(_lootbox.RemainingTime);
            UpdateStateReady(_lootbox.IsReady);

            UpdateResourceElements();

            _view.OnCollectClicked += OnCollectClicked;
            _lootbox.OnReady += UpdateStateReady;
            _lootbox.OnTimeChanged += UpdateRemainingTime;
        }

        private void UpdateResourceElements()
        {
            var resources = _lootbox.CurrencyReward;
            for (var i = 0; i < resources.Length; i++)
            {
                CurrencyData resource = resources[i];
                AmountView spawnResourceElement = _view.SpawnResourceElement();
                spawnResourceElement.SetAmount(resource.Amount);
                spawnResourceElement.SetIcon(_repository.GetStorage(resource.Type).Icon);
            }
        }

        private void UpdateStateReady(bool ready)
        {
            _view.SetReady(ready);
        }

        private void OnTimeChanged(float time)
        {
            UpdateRemainingTime(time);
        }

        private void UpdateRemainingTime(float time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            _view.SetRemainingTime($"{timeSpan.Minutes}:{timeSpan.Seconds}");
        }

        private void OnReady(bool ready)
        {
            UpdateStateReady(ready);
        }

        public void Dispose()
        {
            _view.OnCollectClicked -= OnCollectClicked;
            _lootbox.OnReady += UpdateStateReady;
            _lootbox.OnTimeChanged += UpdateRemainingTime;

            _view.ClearResources();
        }

        private void OnCollectClicked()
        {
            _repository.Add(_lootbox.CurrencyReward);
            _lootbox.Consume();
        }
    }
}
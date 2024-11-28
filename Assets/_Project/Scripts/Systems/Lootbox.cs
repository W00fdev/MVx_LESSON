using System;
using _Project.Scripts.MVP.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Systems
{
    [Serializable]
    public class Lootbox : IFixedTickable
    {
        public event Action<bool> OnReady;
        public event Action<float> OnTimeChanged;

        [SerializeField] private CurrencyRepository _reward;

        [SerializeField] private float _remainingTime;
        [SerializeField] private float _duration;
        [SerializeField] private bool _isReady;

        [SerializeField] [PreviewField] private Sprite _icon;

        [SerializeField] private CurrencyData[] _currencyReward;

        public CurrencyData[] CurrencyReward => _currencyReward;
        public Sprite Icon => _icon;
        public float Duration => _duration;
        public float RemainingTime => _remainingTime;
        public bool IsReady => _isReady;

        public bool Consume()
        {
            if (_isReady)
                return false;

            _isReady = false;
            _remainingTime = _duration;
            OnReady?.Invoke(false);
            return true;
        }

        public void FixedTick()
        {
            if (_isReady)
                return;

            _remainingTime = Mathf.Max(0f, _remainingTime - Time.fixedDeltaTime);
            OnTimeChanged?.Invoke(_remainingTime);

            if (_remainingTime <= 0f)
                SetReady();
        }

        private void SetReady()
        {
            _isReady = true;
            OnReady?.Invoke(true);
        }
    }
}
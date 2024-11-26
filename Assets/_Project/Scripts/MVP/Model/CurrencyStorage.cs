using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.MVP.Model
{
    [Serializable]
    public class CurrencyStorage
    {
        public delegate void AddedDelegate(int newValue, int range);

        public delegate void RemovedDelegate(int newValue, int range);

        public delegate void ChangedDelegate(int prevValue, int newValue);

        [SerializeField] private CurrencyType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _currency;

        public CurrencyType Type => _type;
        public Sprite Icon => _icon;
        public int Currency => _currency;

        public event AddedDelegate OnCurrencyEarned;
        public event RemovedDelegate OnCurrencySpent;
        public event ChangedDelegate OnCurrencyChanged;

        public event Action OnStateChanged;

        public CurrencyStorage(CurrencyType type, int currency)
        {
            _currency = currency;
        }

        public void SetupCurrency(int currency)
        {
            _currency = currency;
        }

        [Button("Add Currency")]
        public void AddCurrency(int range)
        {
            _currency += range;
            OnCurrencyEarned?.Invoke(_currency, range);
            OnStateChanged?.Invoke();
        }

        [Button("Spend Currency")]
        public void SpendCurrency(int range)
        {
            _currency -= range;
            OnCurrencySpent?.Invoke(_currency, range);
            OnStateChanged?.Invoke();
        }

        [Button("Change Currency")]
        public void ChangeCurrency(int range)
        {
            int prevValue = _currency;
            _currency += range;
            OnCurrencyChanged?.Invoke(prevValue, _currency);
            OnStateChanged?.Invoke();
        }
    }
}
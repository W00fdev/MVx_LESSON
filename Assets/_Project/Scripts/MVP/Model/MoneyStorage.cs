using System;
using TriInspector;
using UnityEngine;
using VContainer;

[Serializable]
public class MoneyStorage
{
    public delegate void AddedDelegate (int newValue, int range);
    public delegate void RemovedDelegate (int newValue, int range);
    public delegate void ChangedDelegate (int prevValue, int newValue);
    
    [SerializeField] private int _money;
    
    public int Money => _money;

    public event AddedDelegate OnMoneyEarned;
    public event RemovedDelegate OnMoneySpent;
    public event ChangedDelegate OnMoneyChanged;

    [Inject]
    public MoneyStorage(int money)
    {
        _money = money;
        Debug.Log($"Money injected with {money}");
    }

    public void SetupMoney(int money)
        => _money = money;
    
    [Button( "Add Money")]
    public void AddMoney(int range)
    {
        _money += range;
        OnMoneyEarned?.Invoke(_money, range);
    }
    
    [Button("Spend Money")]
    public void SpendMoney(int range)
    {
        _money -= range;
        OnMoneySpent?.Invoke(_money, range);
    }

    [Button("Change Money")]
    public void ChangeMoney(int range)
    {
        int prevValue = _money;
        _money += range;
        OnMoneyChanged?.Invoke(prevValue, _money);
    }
}
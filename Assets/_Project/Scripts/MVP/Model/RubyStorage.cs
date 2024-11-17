using System;
using TriInspector;
using UnityEngine;
using VContainer;

[Serializable]
public class RubyStorage
{
    public delegate void AddedDelegate (int newValue, int range);
    public delegate void RemovedDelegate (int newValue, int range);
    public delegate void ChangedDelegate (int prevValue, int newValue);
    
    [SerializeField] private int _ruby;
    
    public int Ruby => _ruby;

    public event AddedDelegate OnRubyEarned;
    public event RemovedDelegate OnRubySpent;
    public event ChangedDelegate OnRubyChanged;

    [Inject]
    public RubyStorage(int ruby)
    {
        _ruby = ruby;
        Debug.Log($"Ruby injected with {ruby}");
    }

    public void SetupMoney(int money)
        => _ruby = money;
    
    [Button( "Add Ruby")]
    public void AddRuby(int range)
    {
        _ruby += range;
        OnRubyEarned?.Invoke(_ruby, range);
    }
    
    [Button("Spend Ruby")]
    public void SpendRuby(int range)
    {
        _ruby -= range;
        OnRubySpent?.Invoke(_ruby, range);
    }

    [Button("Change Ruby")]
    public void ChangeRuby(int range)
    {
        int prevValue = _ruby;
        _ruby += range;
        OnRubyChanged?.Invoke(prevValue, _ruby);
    }
}
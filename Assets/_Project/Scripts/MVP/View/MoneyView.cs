using System;
using System.Collections;
using TMPro;
using TriInspector;
using UnityEngine;
using VContainer;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _earnedColor;
    [SerializeField] private Color _spentColor;
    [SerializeField] private float _animationDuration;

    [Inject] private MoneyStorage _moneyStorage;
    
    private Coroutine _animation;
    
    private int _viewedCount;
    private int _actualCount;
    private bool _isEarning;
    
    private void Start()
    {
        _moneyText.color = _defaultColor;
        _moneyStorage.OnMoneyEarned += MoneyEarned;
        _moneyStorage.OnMoneySpent += MoneySpent;
        _moneyStorage.OnMoneyChanged += MoneyChanged;

        _viewedCount = _moneyStorage.Money;
        _moneyText.text = _moneyStorage.Money + "$";
    }

    private void OnDestroy()
    {
        _moneyStorage.OnMoneyEarned -= MoneyEarned;
        _moneyStorage.OnMoneySpent -= MoneySpent;
        _moneyStorage.OnMoneyChanged -= MoneyChanged;
    }

    private void MoneyEarned(int newValue, int range)
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _isEarning = true;
        _actualCount = newValue;
        _animation = StartCoroutine(ChangingMoney());
    }

    private void MoneySpent(int newValue, int range)
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _isEarning = false;
        _actualCount = newValue;
        _animation = StartCoroutine(ChangingMoney());
    }

    private void MoneyChanged(int prevValue, int newValue)
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _isEarning = prevValue < newValue;
        _actualCount = newValue;
        _animation = StartCoroutine(ChangingMoney());
    }
    
    IEnumerator ChangingMoney()
    {
        float targetTime = _animationDuration;
        float timer = 0f;
        int prevCount = _viewedCount;
        while (_viewedCount != _actualCount && timer < targetTime)
        {
            yield return null;
            timer += Time.deltaTime;
            ChangeText((int)Mathf.Lerp(prevCount, _actualCount, Mathf.Clamp(timer / targetTime, 0f, 1f)));
        }

        _moneyText.color = _defaultColor;
        _animation = null;
    }

    private void ChangeText(int count)
    {
        _viewedCount = count;
        _moneyText.text = $"{count}$";
        _moneyText.color = (_isEarning) ? _earnedColor : _spentColor;
    }
}

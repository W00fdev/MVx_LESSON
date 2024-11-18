using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _earnedColor;
    [SerializeField] private Color _spentColor;
    [SerializeField] private float _animationDuration;

    private Func<int, string> _format;
    private Coroutine _animation;
    private bool _isEarning;

    public void SetupCurrency(int value, Func<int, string> format)
    {
        _format = format;
        _moneyText.text = _format?.Invoke(value);
        _moneyText.color = _defaultColor;
    }

    public void EarnCurrency(int previous, int range)
    {
        StopAnimation();

        _isEarning = true;
        _animation = StartCoroutine(ChangingCurrency(previous, range));
    }

    public void SpendCurrency(int previous, int range)
    {
        StopAnimation();

        _isEarning = false;
        _animation = StartCoroutine(ChangingCurrency(previous, range));
    }

    public void ChangeCurrency(int previous, int range)
    {
        StopAnimation();

        _isEarning = range > 0;
        _animation = StartCoroutine(ChangingCurrency(previous, range));
    }

    private IEnumerator ChangingCurrency(int startValue, int range)
    {
        var progression = 0f;
        while (progression < 1f)
        {
            yield return null;
            progression += Time.deltaTime / _animationDuration;
            progression = Mathf.Clamp(progression, 0f, 1f);
            ChangeText(startValue + (int)(range * progression));
        }

        ChangeText(startValue + range);
        _moneyText.color = _defaultColor;
        _animation = null;
    }

    private void StopAnimation()
    {
        if (_animation != null)
            StopCoroutine(_animation);
    }

    private void ChangeText(int count)
    {
        _moneyText.text = _format?.Invoke(count);
        _moneyText.color = _isEarning ? _earnedColor : _spentColor;
    }
}
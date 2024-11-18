using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _earnedColor;
    [SerializeField] private Color _spentColor;
    [SerializeField] private float _animationDuration;

    private Func<int, string> _format;
    private Coroutine _animation;
    private bool _isEarning;

    public void SetupCurrency(int value, Sprite icon, Func<int, string> format)
    {
        _format = format;
        _iconImage.sprite = icon;
        _currencyText.text = _format?.Invoke(value);
        _currencyText.color = _defaultColor;
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
        _currencyText.color = _defaultColor;
        _animation = null;
    }

    private void StopAnimation()
    {
        if (_animation != null)
            StopCoroutine(_animation);
    }

    private void ChangeText(int count)
    {
        _currencyText.text = _format?.Invoke(count);
        _currencyText.color = _isEarning ? _earnedColor : _spentColor;
    }
}
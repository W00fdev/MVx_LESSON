using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

public class RubyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rubyText;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _earnedColor;
    [SerializeField] private Color _spentColor;
    [SerializeField] private float _animationDuration;

    private RubyStorage _rubyStorage;
    private Coroutine _animation;
    
    private int _viewedCount;
    private int _actualCount;
    private bool _isEarning;
    
    [Inject]
    public void Construct(RubyStorage moneyStorage)
    {
        _rubyStorage = moneyStorage;
    }

    private void OnEnable()
    {
        _rubyStorage.OnRubyEarned += RubyEarned;
        _rubyStorage.OnRubySpent += RubySpent;
        _rubyStorage.OnRubyChanged += RubyChanged;

        ChangeText(_rubyStorage.Ruby);
        _rubyText.color = _defaultColor;
    }

    private void OnDestroy()
    {
        _rubyStorage.OnRubyEarned -= RubyEarned;
        _rubyStorage.OnRubySpent -= RubySpent;
        _rubyStorage.OnRubyChanged -= RubyChanged;
    }

    private void RubyEarned(int newValue, int range)
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _isEarning = true;
        _actualCount = newValue;
        _animation = StartCoroutine(ChangingRuby());
    }

    private void RubySpent(int newValue, int range)
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _isEarning = false;
        _actualCount = newValue;
        _animation = StartCoroutine(ChangingRuby());
    }

    private void RubyChanged(int prevValue, int newValue)
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _isEarning = prevValue < newValue;
        _actualCount = newValue;
        _animation = StartCoroutine(ChangingRuby());
    }
    
    IEnumerator ChangingRuby()
    {
        float targetTime = _animationDuration;
        float timer = 0f;
        int prevCount = _viewedCount;
        while (_viewedCount != _actualCount && timer < targetTime)
        {
            yield return null;
            timer += Time.deltaTime;
            float lerpT = Mathf.Clamp(timer / targetTime, 0f, 1f);
            ChangeText((int)Mathf.Lerp(prevCount, _actualCount, lerpT));
        }

        _rubyText.color = _defaultColor;
        _animation = null;
    }

    private void ChangeText(int count)
    {
        _viewedCount = count;
        _rubyText.text = $"{count}$";
        _rubyText.color = (_isEarning) ? _earnedColor : _spentColor;
    }
}
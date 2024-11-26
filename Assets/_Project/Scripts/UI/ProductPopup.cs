using _Project.Scripts.MVP.Model;
using _Project.Scripts.Systems;
using _Project.Scripts.UI;
using Lean.Gui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProductPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _productName;
    [SerializeField] private TextMeshProUGUI _productDescription;

    //[SerializeField] private CurrencyRepository _currencyRepository;
    [SerializeField] private PriceListView _priceListView;

    [SerializeField] private LeanButton _button;
    [SerializeField] private Image _image;


    private IProductPresenter _productPresenter;

    [Inject]
    public void Construct(IProductPresenter productPresenter)
    {
        _productPresenter = productPresenter;
    }

    [Sirenix.OdinInspector.Button]
    public void Show()
    {
        OnStateChanged();

        _productPresenter.OnStateChanged += OnStateChanged;
        _productPresenter.OnBuyButtonEnabled += OnBuyButtonEnabled;
        _button.OnClick.AddListener(_productPresenter.OnBuyClicked);

        ShowPrice();
        gameObject.SetActive(true);
    }

    private void ShowPrice()
    {
        _priceListView.Clear();

        var priceElements = _productPresenter.PriceElements;
        for (var i = 0; i < priceElements.Count; i++)
        {
            IProductPresenter.IPriceElement priceElement = priceElements[i];
            PriceView priceView = _priceListView.SpawnView();

            priceView.SetIcon(priceElement.Icon);
            priceView.SetPrice(priceElement.Price);
        }
    }

    private void OnStateChanged()
    {
        _productName.text = _productPresenter.ProductName;
        _productDescription.text = _productPresenter.ProductDescription;
        _image.sprite = _productPresenter.Icon;

        ShowPrice();
    }

    private void OnBuyButtonEnabled(bool enabled)
    {
        _button.interactable = enabled;
    }

    [Sirenix.OdinInspector.Button]
    public void Hide()
    {
        _productPresenter.OnStateChanged -= OnStateChanged;
        _productPresenter.OnBuyButtonEnabled -= OnBuyButtonEnabled;
        _button.OnClick.RemoveListener(_productPresenter.OnBuyClicked);
        gameObject.SetActive(false);
    }
}
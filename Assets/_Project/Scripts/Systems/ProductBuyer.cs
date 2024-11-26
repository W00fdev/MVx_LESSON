using System;
using _Project.Scripts.MVP.Model;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class ProductBuyer
{
    public event Action<Product> OnProductBought;
    public event Action<bool> OnProductCanBuy;

    private readonly CurrencyRepository _currencyRepository;

    [Inject]
    public ProductBuyer(CurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    [Button]
    public bool CanBuy(Product product)
    {
        return product == null
            ? throw new ArgumentNullException(nameof(product))
            : _currencyRepository.IsEnough(product);
    }

    [Button]
    public bool Buy(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (_currencyRepository.IsEnough(product) == false)
        {
            Debug.LogWarning($"<color=red>Not enough money for product {product}</color>");
            return false;
        }

        _currencyRepository.Spend(product.Price);
        OnProductBought?.Invoke(product);
        Debug.Log($"<color=green>Product bought {product}</color>");

        return true;
    }
}
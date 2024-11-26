using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "Data/Product")]
public class Product : ScriptableObject
{
    public string Name => _name;
    public string Description => _description;
    public Sprite IconSprite => _iconSprite;

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _iconSprite;

    [SerializeField] private List<CurrencyData> _price;

    public IReadOnlyList<CurrencyData> Price => _price;
}
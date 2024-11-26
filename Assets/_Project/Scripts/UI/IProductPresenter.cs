using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public interface IProductPresenter
    {
        Action OnStateChanged { get; set; }
        Action<bool> OnBuyButtonEnabled { get; set; }

        IReadOnlyList<IPriceElement> PriceElements { get; }

        string ProductName { get; }
        string ProductDescription { get; }
        string Price { get; }
        Sprite Icon { get; }
        bool IsButtonEnabled { get; }
        void OnBuyClicked();

        public interface IPriceElement
        {
            Sprite Icon { get; }
            int Price { get; }
        }
    }
}
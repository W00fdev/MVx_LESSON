using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Systems
{
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Image _icon;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetPrice(int price)
        {
            _price.text = price.ToString();
        }
    }
}
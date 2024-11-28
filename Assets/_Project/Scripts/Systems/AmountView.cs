using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Systems
{
    public class AmountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Image _icon;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetAmount(int price)
        {
            _price.text = price.ToString();
        }
    }
}
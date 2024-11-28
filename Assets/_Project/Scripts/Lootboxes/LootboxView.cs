using System;
using _Project.Scripts.Systems;
using Lean.Gui;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Project.Scripts.Lootboxes
{
    // Passive-view
    public class LootboxView : MonoBehaviour
    {
        public event UnityAction OnCollectClicked
        {
            add => _collectButton.OnClick.AddListener(value);
            remove => _collectButton.OnClick.RemoveListener(value);
        }

        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _remainingTimeText;
        [SerializeField] private LeanButton _collectButton;
        [SerializeField] private GameObject _timerContainer;

        [SerializeField] private AmountListView _resources;

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }

        // Осторожно, форматирование здесь использовать не нужно.
        public void SetRemainingTime(string time)
        {
            _remainingTimeText.text = time;
        }

        public void SetReady(bool isReady)
        {
            _collectButton.gameObject.SetActive(isReady);
            _timerContainer.gameObject.SetActive(!isReady);
        }

        public AmountView SpawnResourceElement()
        {
            return _resources.SpawnView();
        }

        public void ClearResources()
        {
            _resources.Clear();
        }
    }
}
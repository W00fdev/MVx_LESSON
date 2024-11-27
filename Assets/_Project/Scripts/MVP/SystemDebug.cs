using _Project.Scripts.MVP.Model;
using _Project.Scripts.Systems;
using _Project.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.MVP
{
    public class SystemDebug : MonoBehaviour
    {
        [Inject] [ShowInInspector] public ProductPresenter _presenter;

        [Inject] [ShowInInspector] public Lootbox LootboxDebug;
        [Inject] [ShowInInspector] public LootboxConsumer LootboxConsumerDebug;

        [Button]
        public void Consume()
        {
            LootboxConsumerDebug.Consume(LootboxDebug);
        }
    }
}
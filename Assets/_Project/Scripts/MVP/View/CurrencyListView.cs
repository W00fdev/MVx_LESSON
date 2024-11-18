using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.MVP.View
{
    public class CurrencyListView : MonoBehaviour
    {
        [SerializeField] private CurrencyView _prefab;
        [SerializeField] private Transform _container;

        public CurrencyView SpawnView()
        {
            return Instantiate(_prefab, _container);
        }

        public void UnspawnView(CurrencyView view)
        {
            if (view)
                Destroy(view.gameObject);
        }
    }
}
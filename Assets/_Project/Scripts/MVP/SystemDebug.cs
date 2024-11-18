using _Project.Scripts.MVP.Model;
using TriInspector;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.MVP
{
    public class SystemDebug : MonoBehaviour
    {
        [Inject] [ShowInInspector] private CurrencyRepository _currencyRepository;
    }
}
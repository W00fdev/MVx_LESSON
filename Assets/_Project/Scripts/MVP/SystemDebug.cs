using TriInspector;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.MVP
{
    public class SystemDebug : MonoBehaviour
    {
        [Inject] [ShowInInspector]
        private MoneyStorage _moneyStorage;
        
        [Inject] [ShowInInspector]
        private RubyStorage _rubyStorage;
    }
}
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace _Project.Scripts.MVP.Model
{
    public class CurrencyRepository
    {
        private readonly Dictionary<CurrencyType, CurrencyStorage> _repository;

        [Inject]
        public CurrencyRepository(CurrencyStorage[] storages)
        {
            _repository = storages.ToDictionary(it => it.Type);
        }

        public CurrencyStorage GetStorage(CurrencyType type)
        {
            return _repository[type];
        }

        public IEnumerator<CurrencyStorage> GetEnumerator()
        {
            return _repository.Values.GetEnumerator();
        }
    }
}
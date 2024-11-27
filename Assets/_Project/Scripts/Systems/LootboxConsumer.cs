using _Project.Scripts.MVP.Model;
using Zenject;

namespace _Project.Scripts.Systems
{
    public class LootboxConsumer
    {
        private readonly CurrencyRepository _currency;

        [Inject]
        public LootboxConsumer(CurrencyRepository currency)
        {
            _currency = currency;
        }

        public bool Consume(Lootbox lootbox)
        {
            if (lootbox.Consume())
            {
                _currency.Add(lootbox.CurrencyReward);
                return true;
            }

            return false;
        }
    }
}
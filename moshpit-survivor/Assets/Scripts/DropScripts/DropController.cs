using SFX;
using UI_Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DropScripts
{
    public class DropController : MonoBehaviour
    {
        [SerializeField] private Transform coinPool;
        
        public CoinPickups coin;
        public int currentCoins;

        private IObjectResolver _resolver;
        private UIController _uiController;

        [Inject]
        public void Construct(IObjectResolver resolver,UIController uiController)
        {
            _resolver = resolver;
            _uiController = uiController;
        }

        public void DropCoin(Vector3 position, int value)
        {
            var newCoin = _resolver.Instantiate(coin, position + new Vector3(0.2f, .4f, 0f), Quaternion.identity,coinPool);
            newCoin.coinAmount = value;
            newCoin.gameObject.SetActive(true);
        }

        public void AddCoin(int coinsToAdd)
        {
            currentCoins += coinsToAdd;
            _uiController.UpdateCoins(currentCoins);
            SFXManager.instance.PlaySfxPitched(2);
            
        }

        public void SpendCoin(int coinsToSpend)
        {
            currentCoins -= coinsToSpend;
            _uiController.UpdateCoins(currentCoins);
        }
    }
}
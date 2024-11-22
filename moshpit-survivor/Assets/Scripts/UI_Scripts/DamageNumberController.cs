using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UI_Scripts
{
    public class DamageNumberController : MonoBehaviour
    {

        [SerializeField] private DamageNumber numberToSpawn;
        [SerializeField] private Transform damageNumberCanvas;

        private readonly List<DamageNumber> _numberPool = new List<DamageNumber>();
        private IObjectResolver _objectResolver;
        
        [Inject]
        public void Construct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }
        
        public void DamageToSpawn(float damageAmount, Vector3 location)
        {
            var rounded = Mathf.RoundToInt(damageAmount);

            var newDamage = GetFromPool();
            newDamage.DamageNumberSpawn(rounded);
            newDamage.gameObject.SetActive(true);
            newDamage.transform.position = location;
        }

        public void PlaceInPool(DamageNumber numberToPlace)
        {
            numberToPlace.gameObject.SetActive(false);
            _numberPool.Add(numberToPlace);
        }
        
        private DamageNumber GetFromPool()
        {
            DamageNumber numberToOutput = null;

            if (_numberPool.Count == 0)
            {
                numberToOutput = _objectResolver.Instantiate(numberToSpawn, damageNumberCanvas);
            }
            else
            {
                numberToOutput = _numberPool[0];
                _numberPool.RemoveAt(0);
            }
            
            return numberToOutput;
        }
        
        
        
    }
}
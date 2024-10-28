using TMPro;
using UI_Scripts;
using UnityEngine;
using VContainer;

public class DamageNumber : MonoBehaviour
{
    
    [SerializeField] private TMP_Text damageNumberText;
    [SerializeField] private float damageLifeTime;
    [SerializeField] private float floatSpeed; 
    
    private float _lifeTimeCounter;
    private DamageNumberController _damageNumberController;

    [Inject]
    public void Construct(DamageNumberController damageNumberController)
    {
        _damageNumberController = damageNumberController;
    }

    private void Start()
    {
        _lifeTimeCounter = damageLifeTime;
    }

    private void Update()
    {
        DamageNumberDespawn();
        SlidingEffectNumber();
    }

    public void DamageNumberSpawn(int damageNumber)
    {
        _lifeTimeCounter = damageLifeTime;
        
        damageNumberText.text = damageNumber.ToString();
    }
    
    private void DamageNumberDespawn()
    {
        if (_lifeTimeCounter > 0)
        {
            _lifeTimeCounter -= Time.deltaTime;

            if (_lifeTimeCounter <= 0)
            {
                _damageNumberController.PlaceInPool(this);
            }    
        }
    }

    private void SlidingEffectNumber()
    {
        transform.position += Vector3.up * (floatSpeed * Time.deltaTime);
    }
}

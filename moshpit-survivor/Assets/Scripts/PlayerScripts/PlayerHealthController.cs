using System;
using System.Threading.Tasks;
using Datas;
using Interfaces;
using SFX;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PlayerScripts
{
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        
         public static PlayerHealthController Instance;
         private void Awake()
         {
             Instance = this;
         }

        [SerializeField] private Slider healthSlider;
        [SerializeField] private GameObject deathEffect;
        public float currentHealth;

        private Animator _animator;
        private PlayerData _playerData;

        
        [Inject]
        public void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            currentHealth = _playerData.health;
        }

        private void Update()
        {
            healthSlider.maxValue = _playerData.health;
            healthSlider.value = currentHealth;
        }

        private async Task DieAfterAnimation()
        {
            var animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            await Task.Delay(TimeSpan.FromSeconds(animationLength));
            
            gameObject.SetActive(false);
        }
        
        public void TakeDamage(float damageToTake)
        {
            currentHealth -= damageToTake;
            
            
            if (currentHealth <= 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                _animator.SetTrigger("isDie");
                _ = DieAfterAnimation();
                
                SFXManager.instance.PlaySfxPitched(3);
                LevelManager.Instance.EndGame();
            }
            
            healthSlider.value = currentHealth;
        }
    }
}
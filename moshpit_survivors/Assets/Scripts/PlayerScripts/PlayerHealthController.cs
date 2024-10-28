using System;
using System.Threading.Tasks;
using Datas;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PlayerScripts
{
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {

        [SerializeField] private float currentHealth;
        [SerializeField] private Slider healthSlider;
        
        private Animator _animator;
        private PlayerData _playerData;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        [Inject]
        public void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        private void Start()
        {
            currentHealth = _playerData.health;
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
                _animator.SetTrigger("isDie");
                _ = DieAfterAnimation();
            }
            
            healthSlider.value = currentHealth;
        }
    }
}
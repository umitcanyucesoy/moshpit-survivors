using System;
using System.Collections;
using System.Collections.Generic;
using Datas;
using UnityEngine;
using VContainer;
using Weapons;
using Random = UnityEngine.Random;


namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {

        public List<WeaponService> unassignedWeapons, assignedWeapons;
        [HideInInspector] public List<WeaponService> fullyLevelledWeapons;
        public int maxWeapons = 3;
        
        private Vector3 _moveInput = new Vector3(0f, 0f, 0f); 
        private Animator _animator;
        private PlayerData _playerData;
        
        [Inject]
        public void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        { 
            PlayerAddWeapon(Random.Range(0, unassignedWeapons.Count));
        }


        private void Update()
        {
            PlayerMovement();
        }

        private void PlayerMovement()
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
            _moveInput.Normalize();
            transform.position += _moveInput * (_playerData.moveSpeed * Time.deltaTime);
            
            _animator.SetBool("isMoving", _moveInput != Vector3.zero);
        }

        private void PlayerAddWeapon(int weaponNumber)
        {
            if (weaponNumber >= unassignedWeapons.Count) return;
            
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
                
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }

        public void PlayerAddWeapon(WeaponService weaponToAdd)
        {
            weaponToAdd.gameObject.SetActive(true);

            assignedWeapons.Add(weaponToAdd);
            unassignedWeapons.Remove(weaponToAdd);
        }
    }
}


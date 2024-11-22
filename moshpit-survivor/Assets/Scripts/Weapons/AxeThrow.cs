using System;
using System.Collections;
using System.Collections.Generic;
using Datas;
using SFX;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class AxeThrow : MonoBehaviour
{

    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Rigidbody2D theRb;
    [SerializeField] private float throwForce;
    

    private void Start()
    {
        AxeThrowForce();
    }

    private void Update()
    {
        AxeRotate();
    }

    private void AxeThrowForce()
    {
        theRb.velocity = new Vector2(Random.Range(-throwForce,throwForce), throwForce);
        SFXManager.instance.PlaySfxPitched(4);
    }

    private void AxeRotate()
    {
        transform.rotation = Quaternion.Euler(0f,0f, transform.rotation.eulerAngles.z + 
                                                     (weaponData.rotateSpeed * 360f * Time.deltaTime * Mathf.Sign(theRb.velocity.x)));
    }
    
}

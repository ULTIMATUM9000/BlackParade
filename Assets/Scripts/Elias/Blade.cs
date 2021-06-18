using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;

public class Blade : MonoBehaviour
{ 
    [SerializeField] LayerMask _LayerMask;
    [SerializeField] float _Radius;

    Collider2D[] _Collider;

    //ZBDslime _Slime;

    public int Damage { get; set; }

    void OnEnable()
    {
        foreach(Collider2D col in Physics2D.OverlapCircleAll(transform.position, _Radius, _LayerMask))
        {
            //_Slime = col.GetComponent<ZBDslime>();

            //_Slime?.SlimeTakesDamage(Damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _Radius);
    }
}

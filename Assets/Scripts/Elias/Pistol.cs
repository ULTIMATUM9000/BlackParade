using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YergoScripts;
using YergoScripts.ObjectPool;

public class Pistol : Ability
{
    [SerializeField] GameObject _BulletPrefab;
    [SerializeField] int _Damage;

    ObjectPool _BulletPool = new ObjectPool();

    void Start()
    {
        _BulletPool.Instantiate(_BulletPrefab, 20);
    }

    protected override void AbilityStart(GameObject player, float angle)
    {
        Bullet bullet = _BulletPool.GetObject().GetComponent<Bullet>();

        bullet.Damage = _Damage;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        AbilityEnd();
    }

    protected override void AbilityUpdate(GameObject player, float angle){}

    protected override void AbilityInactiveUpdate()
    {
        _BulletPool.ReturnObject();
    }
}
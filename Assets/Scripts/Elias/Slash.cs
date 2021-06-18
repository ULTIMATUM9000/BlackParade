using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Ability
{
    [SerializeField] GameObject _SlashChild;
    [SerializeField] int _Damage;

    Blade _Blade;

    void Start()
    {
        _SlashChild.gameObject.SetActive(false);
    }

    protected override void AbilityStart(GameObject player, float angle)
    {
        _Blade = _SlashChild.GetComponentInChildren<Blade>();

        _Blade.Damage = _Damage;
        _SlashChild.gameObject.SetActive(true);
        _SlashChild.transform.rotation = Quaternion.Euler(0, 0, angle);
        StartCoroutine(SlashTime());        
    }

    protected override void AbilityUpdate(GameObject player, float angle){}

    protected override void AbilityInactiveUpdate() {}

    IEnumerator SlashTime()
    {
        yield return new WaitForSeconds(0.1f);
        _SlashChild.gameObject.SetActive(false);
        AbilityEnd();
    }
}

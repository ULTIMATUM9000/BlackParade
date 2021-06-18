using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] float _CoolDownTime;

    float _Angle;
    float _TimeLeft = 0;

    GameObject _Player;
    
    bool _IsCoolDown = false;
    bool _IsActive = false;

    public delegate void CoolDownListener (float timeLeft, float coolDowmTime);

    public CoolDownListener CoolDownFuncs;

    public void Activate(GameObject player, float angle)
    {
        if(!_IsCoolDown)
        {
            _IsActive = true;
            _Angle = angle;
            _Player = player;

            AbilityStart(player, angle);
        }
    }

    void Update()
    {
        if (_IsActive)
            AbilityUpdate(_Player, _Angle);
        else
            AbilityInactiveUpdate();


        if (_IsCoolDown)
            CoolDown();
    }

    void CoolDown()
    {
        _TimeLeft -= Time.deltaTime;

        if (CoolDownFuncs != null)
            CoolDownFuncs(_TimeLeft, _CoolDownTime);

        if (_TimeLeft <= 0)
            _IsCoolDown = false;
    }

    protected void AbilityEnd()
    {
        _IsActive = false;
        _IsCoolDown = true;

        _TimeLeft = _CoolDownTime;
    }

    protected abstract void AbilityStart(GameObject player, float angle);
    protected abstract void AbilityUpdate(GameObject player, float angle);

    protected abstract void AbilityInactiveUpdate();

}
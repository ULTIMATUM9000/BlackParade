using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YergoScripts.AnimationManagement;

public class PlayerAnimHandler : MonoBehaviour
{
    [SerializeField] AnimationClip _IdleFront;
    [SerializeField] AnimationClip _IdleLeft;
    [SerializeField] AnimationClip _IdleRight;
    [SerializeField] AnimationClip _IdleBack;

    [SerializeField] AnimationClip _WalkFront;
    [SerializeField] AnimationClip _WalkLeft;
    [SerializeField] AnimationClip _WalkRight;
    [SerializeField] AnimationClip _WalkBack;

    [SerializeField] AnimationClip _AttackFront;
    [SerializeField] AnimationClip _AttackLeft;
    [SerializeField] AnimationClip _AttackRight;
    [SerializeField] AnimationClip _AttackBack;

    AnimationOverrideManager _AnimOverrideManager = new AnimationOverrideManager();
    Player _Player;

    void Start()
    {
        _AnimOverrideManager.Animator = GetComponent<Animator>();
        _Player = GetComponent<Player>();
    }

    void Update()
    {
        if (_Player.FaceDirection.x != 0 || _Player.FaceDirection.y != 0) // Rook directions
        {
            if (_Player.FaceDirection.y == 1)
                _AnimOverrideManager.OverrideAnimationClip("DefaultClip", _WalkBack);

            else if (_Player.FaceDirection.y == -1)
                _AnimOverrideManager.OverrideAnimationClip("DefaultClip", _WalkFront);

            else if (_Player.FaceDirection.x == 1)
                _AnimOverrideManager.OverrideAnimationClip("DefaultClip", _WalkRight);

            else if (_Player.FaceDirection.x == -1)
               _AnimOverrideManager.OverrideAnimationClip("DefaultClip", _WalkLeft);
        }
    }
}

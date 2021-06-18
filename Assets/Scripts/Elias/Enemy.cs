using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using YergoScripts;
using YergoScripts.Grid;
using YergoScripts.Pathfinding;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] protected GameObject _Target;
    [SerializeField] LayerMask _TargetLayer;
    [Space]
    [SerializeField] float _Speed;

    RaycastHit2D _Hit;
    Vector2 _Direction;
    
    protected bool _DoMove = true;
    protected Rigidbody2D _Rigidbody;

    void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(_DoMove)
        {
            _Direction = _Target.transform.position - transform.position;

            _Hit = Physics2D.Raycast(transform.position, _Direction, Vector2.Distance(transform.position, _Target.transform.position), _TargetLayer);

            if (_Hit)
                _Rigidbody.MovePosition(Vector2.MoveTowards(transform.position, _Target.transform.position, _Speed * Time.deltaTime));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] ParadeMovement _Parade;

    bool _IsHalfPoint = false;

    public static Vector2 CurrentCheckpoint { get; private set; }

    void Awake()
    {
        CurrentCheckpoint = transform.position;
    }

    void Update()
    {
        if(_Parade.percent >= 0.5f && !_IsHalfPoint)
        {
            _IsHalfPoint = true;
            transform.position = _Parade.transform.position + Vector3.down;
            CurrentCheckpoint = transform.position;
        }
    }
}

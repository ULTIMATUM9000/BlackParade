using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YergoScripts.Grid;

public class PathfindingManager : MonoBehaviour
{
    //[SerializeField] float _UpdateInterval = 1f;

    //public static UnityAction Listener;

    //public static List<UnityAction> Test = new List<UnityAction>();

    //Delegate[] _ListenerFuncs;
    //int _CurrentFunc = 0;
    //void Start()
    //{
    //    _ListenerFuncs = Listener?.GetInvocationList();

    //    StartCoroutine(UpdatePathfinding(_UpdateInterval));
    //}

    //IEnumerator UpdatePathfinding(float seconds)
    //{
    //    yield return new WaitForSeconds(seconds);

    //    // --- For new enemies that uses pathfinding --- 
    //    Delegate[] tempFuncs = Listener?.GetInvocationList(); // Get current list

    //    if (tempFuncs != _ListenerFuncs) // If not the same
    //        _ListenerFuncs = tempFuncs; // replace

    //    // --- For new enemies that uses pathfinding --- 

    //    if(_ListenerFuncs != null)
    //    {
    //        _CurrentFunc = _CurrentFunc < _ListenerFuncs.Length - 1 ? _CurrentFunc + 1 : 0; // Add or reset depending on count

    //        _ListenerFuncs[_CurrentFunc].DynamicInvoke(); // individually invoke
    //    }

    //    StartCoroutine(UpdatePathfinding(seconds)); // Repeat
    //}

    static List<UnityAction> _PathfindList = new List<UnityAction>();

    static int _CurrentPathfind = 0;

    void LateUpdate()
    {
        if(_PathfindList.Count > 0)
        {
            if (_CurrentPathfind >= _PathfindList.Count)
                _CurrentPathfind = 0;

            _PathfindList[_CurrentPathfind++]();
        }
    }

    public static bool Contains(UnityAction pathfind)
    {
        return _PathfindList.Contains(pathfind);
    }

    public static void Add(UnityAction pathfind)
    {
        _PathfindList.Add(pathfind);
    }

    public static void Remove(UnityAction pathfind)
    {
        if (!_PathfindList.Contains(pathfind))
            return;

        if(_CurrentPathfind <= _PathfindList.FindIndex((UnityAction u) => u == pathfind))
            _CurrentPathfind--;

        _PathfindList.Remove(pathfind);
    }
}

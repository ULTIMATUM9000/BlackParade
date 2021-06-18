using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YergoScripts.ObjectPool
{
    public class ObjectPool
    {
        List<GameObject> _ActivePool = new List<GameObject>();
        
        Stack<GameObject> _InactivePool = new Stack<GameObject>();
        
        GameObject _OriginalGameObject;
        
        public int Count { get => _ActivePool.Count + _InactivePool.Count; }
        public bool HasUnusedObjects { get => _InactivePool.Count > 0; } // Not yet sure if I will use this

        /// <summary>
        /// Gets an unused object in the pool. If all are in use, the function will *reset and use another object. *Reset is not yet implemented
        /// </summary>
        public GameObject GetObject() // Reset is not yet implemented
        {
            GameObject returnObj;

            if (_InactivePool.Count > 0)
            {
                returnObj = _InactivePool.Pop();
                _ActivePool.Add(returnObj);

                returnObj.SetActive(true);

                return returnObj;
            }
            // Reset GameObject
            
            return _ActivePool[_ActivePool.Count - 1];
        }

        /// <summary>
        /// Gets an unused object in the pool. If all are in use, the function will add and use another object. 
        /// </summary>
        public GameObject AdaptGetObject()
        {
            GameObject obj;

            if (_InactivePool.Count > 0)
            {
                obj = _InactivePool.Pop();

                _ActivePool.Add(obj);

                return obj;
            }

            obj = GameObject.Instantiate(_OriginalGameObject);
            obj.SetActive(true);

            _ActivePool.Add(obj);

            return obj;
        }

        /// <summary>
        /// Gets an unused object in the pool. If all are in use, the function will return null. 
        /// </summary>
        public GameObject TryGetObject()
        {
            if(_InactivePool.Count > 0)
            {
                GameObject obj = _InactivePool.Pop();

                _ActivePool.Add(obj);

                return obj;
            }

            return null;
        }

        /// <summary>
        /// Returns object found inactive and puts it back into the pool
        /// </summary>
        public void ReturnObject()
        {
            List<GameObject> inactiveObjects = new List<GameObject>();

            foreach(GameObject g in _ActivePool)
            {
                if (!g.activeSelf)
                    inactiveObjects.Add(g);
            }

            foreach(GameObject g in inactiveObjects)
            {
                _ActivePool.Remove(g);
                _InactivePool.Push(g);
            }
        }

        /// <summary>
        /// Returns object back into the pool and sets it inactive to be used somewhere else.
        /// </summary>
        public void ReturnObject(GameObject obj)
        {
            if(_ActivePool.Contains(obj))
            {
                obj.SetActive(false);

                _ActivePool.Remove(obj);

                _InactivePool.Push(obj);
            }

            else if (_InactivePool.Contains(obj))
                Debug.LogWarning("ObjectPool Warning: " + obj + " is already inactive!");

            else
                Debug.LogWarning("ObjectPool Warning: " + obj + " is not part of the pool!");
        }

        /// <summary>
        /// Instantiates the Game Objects in the Object Pool and sets it inactive for use.
        /// </summary>
        public void Instantiate(GameObject obj, uint numObjects)
        {
            _OriginalGameObject = obj;

            GameObject gameObj;

            for(int i = 0; i < numObjects; i++)
            {
                gameObj = GameObject.Instantiate(obj);
                gameObj.SetActive(false);
                
                _InactivePool.Push(gameObj);
            }
        }

        /// <summary>
        /// Destroys all Game Objects and clears the Object Pool.
        /// </summary>
        public void DestroyPool()
        {
            foreach (GameObject obj in _InactivePool)
                GameObject.Destroy(obj.gameObject);

            foreach (GameObject obj in _ActivePool)
                GameObject.Destroy(obj.gameObject);

            _InactivePool.Clear();
            _ActivePool.Clear();
        }
    }
}
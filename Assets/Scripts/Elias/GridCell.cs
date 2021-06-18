using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YergoScripts.Grid
{
    public class GridCell
    {
        #region Variables
        Vector3 _WorldPosition;
        Vector3Int _GridPosition;
        Bounds _Bounds;
        [System.Obsolete] bool _IsObstacle = false;
        GameObject[] _GameObjectArray = null;
        #endregion

        #region Constructors
        public GridCell()
        {
            _WorldPosition = Vector3.zero;

            _GridPosition = Vector3Int.zero;

            _Bounds = new Bounds();

            _IsObstacle = false;
        }

        [System.Obsolete]
        public GridCell(Vector3 worldPosition, Vector3Int gridPosition, Vector3 size, bool isObstacle)
        {
            _WorldPosition = worldPosition;

            _GridPosition = gridPosition;

            _Bounds = new Bounds(worldPosition, size);

            _IsObstacle = isObstacle;
        }

        public GridCell(Vector3 worldPosition, Vector3Int gridPosition, Vector3 size, GameObject[] gameObjects)
        {
            _WorldPosition = worldPosition;

            _GridPosition = gridPosition;

            _Bounds = new Bounds(worldPosition, size);

            _GameObjectArray = gameObjects;
        }
        #endregion

        #region Get Set
        public Vector3 WorldPosition { get => _WorldPosition; }
        public Vector3Int GridPosition { get => _GridPosition; }
        public Vector3 Size { get => _Bounds.size; }
        public Vector3 Extents { get => _Bounds.extents; }
        public bool IsObstacle { get => _IsObstacle; }
        public GameObject[] GameObjectsList { get => _GameObjectArray; }
        #endregion

        #region Functions
        [System.Obsolete("Not sure if this will be used or useful at all.")]
        public List<GameObject> GetGameObjects(LayerMask layerMask)
        {
            List<GameObject> gameObjectsList = new List<GameObject>();

            foreach(GameObject gameObject in GameObjectsList)
            {
                if (gameObject.layer == layerMask)
                    gameObjectsList.Add(gameObject);
            }

            return gameObjectsList;
        }

        [System.Obsolete("Not sure if this will be used or useful at all.")]
        public List<GameObject> GetGameObjects(string tag, LayerMask layerMask)
        {
            List<GameObject> gameObjectsList = new List<GameObject>();

            foreach (GameObject gameObject in GameObjectsList)
            {
                if (gameObject.layer == layerMask && gameObject.tag == tag)
                        gameObjectsList.Add(gameObject);
            }

            return gameObjectsList;
        }

        public List<GameObject> GetGameObjects(string tag)
        {
            List<GameObject> gameObjectsList = new List<GameObject>();

            foreach (GameObject gameObject in _GameObjectArray)
            {
                if (gameObject.tag == tag)
                    gameObjectsList.Add(gameObject);

            }

            return gameObjectsList;
        }

        [System.Obsolete("Not sure if this will be used or useful at all.")]
        public GameObject FindGameObject(string name, string tag, LayerMask layerMask)
        {
            foreach (GameObject gameObject in _GameObjectArray)
            {
                if (gameObject.layer == layerMask && gameObject.tag == tag && gameObject.name == name)
                    return gameObject;
            }

            return null;
        }

        public GameObject FindGameObject(string name, string tag)
        {
            foreach (GameObject gameObject in _GameObjectArray)
            {
                if (gameObject.tag == tag && gameObject.name == name)
                    return gameObject;
            }

            return null;
        }

        [System.Obsolete("Not sure if this will be used or useful at all.")]
        public GameObject FindGameObject(string name, LayerMask layerMask)
        {
            foreach (GameObject gameObject in _GameObjectArray)
            {
                if (gameObject.layer == layerMask && gameObject.name == name)
                    return gameObject;
            }
            
            return null;
        }

        public bool Contains(Vector3 position)
        {
            return _Bounds.Contains(position);
        }

        public bool Contains(GameObject gameObject) // Not sure if this works
        {
            if (_GameObjectArray == null)
                return false;

            foreach(GameObject gObject in _GameObjectArray)
            {
                if (gObject == gameObject)
                    return true;
            }

            return false;
        }
        #endregion
    }
}
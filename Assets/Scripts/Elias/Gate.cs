//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class Gate : Interactable
//{
//    [SerializeField] Key[] _Keys;

//    [System.Serializable]
//    public class Key
//    {
//        public GameObject KeyGameObject;
//        public bool IsPlaced = false;
//    }

//    protected override void ObjectUpdate()
//    {
//        bool allKeysPlaced = true;

//        foreach (Key key in _Keys)
//            allKeysPlaced = allKeysPlaced && key.IsPlaced;

//        if (allKeysPlaced)
//            gameObject.SetActive(false);
//    }

//    protected override void Idle()
//    {
//        _BubbleObject.transform.localScale = Vector2.one * 1;
//    }

//    protected override void Interact(GameObject player)
//    {
//        Player p = player.GetComponent<Player>();

//        _BubbleObject.transform.localScale = Vector2.one * 5;

//        _Text.fontSize = 0.5f;
//        _Text.text = "Keys needed: " + _Keys.Length + "\nKeys in Inventory: " + UserKeys(p) + "\nKeys Placed: " + KeysPlaced() + "\n\nPress E to Place Keys";

//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            foreach(GameObject key in player.GetComponent<Player>().Items)
//                PlaceKey(key);

//            foreach(Key key in _Keys)
//            {
//                if (key.IsPlaced)
//                    p.GiveFromInventory(gameObject, key.KeyGameObject);
//            }
//        }
//    }

//    protected override void IsNear(GameObject player)
//    {

//    }

//    string KeysPlaced()
//    {
//        int keysPlaced = 0;
        
//        foreach(Key k in _Keys)
//        {
//            if (k.IsPlaced)
//                keysPlaced++;
//        }

//        return keysPlaced.ToString();
//    }

//    public bool PlaceKey(GameObject key)
//    {
//        foreach(Key k in _Keys)
//        {
//            if(k.KeyGameObject == key)
//            {
//                k.IsPlaced = true;
//                return true;
//            }
//        }

//        return false;
//    }

//    public int UserKeys(Player player)
//    {
//        int count = 0;

//        foreach(Key k in _Keys)
//        {
//            foreach(GameObject key in player.Items)
//            {
//                if (k.KeyGameObject == key)
//                {
//                    count++;
//                    break;
//                }
//            }
//        }

//        return count;
//    }
//}
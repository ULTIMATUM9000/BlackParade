using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YergoScripts.GameManagement
{
    /// <summary>
    /// No Idea if this works for inheritance but yeah...
    /// Source: https://www.youtube.com/watch?v=CPKAgyp8cno
    /// </summary>
    public class Singleton : MonoBehaviour
    {
        static Singleton Instance { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            else
                Destroy(this);
        }
    }
}
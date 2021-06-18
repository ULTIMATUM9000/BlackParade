using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager
{
    // This exists to fix 2 or more interactable from activating
    public static List<GameObject> NearInteractables { get; private set; }

    static InteractableManager()
    {
        NearInteractables = new List<GameObject>();
    }

    // Problem: What if there are 2 objects with the same distance as with the interacter
    public static bool IsClosest(GameObject interactable, GameObject interacter)
    {
        float closestDistance = Vector2.Distance(interactable.transform.position, interacter.transform.position);

        foreach(GameObject g in NearInteractables)
        {
            if (Vector2.Distance(g.transform.position, interacter.transform.position) < closestDistance)
                return false;
        }

        return true;
    }
}

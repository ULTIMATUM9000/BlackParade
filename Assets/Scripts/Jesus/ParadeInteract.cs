using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadeInteract : Interactable
{
    [SerializeField] Vector2 _BubblePosOffsetAfter;
    [SerializeField] float _FontSizeAfter;
    [SerializeField] [Multiline] string _Info;

    // [SerializeField] ScoreManager scoreManager;
    // [SerializeField] float scoreValue;

    Vector2 _BubblePosOffsetBefore;

    void Start()
    {
        _BubblePosOffsetBefore = _BubbleObject.transform.localPosition;
        _BubbleObject.transform.localPosition = _BubblePosOffsetBefore;
        // scoreManager = GameObject.FindGameObjectWithTag("UI").GetComponent<ScoreManager>();
    }

    protected override void ObjectUpdate() // Update
    {} 

    protected override void IsNear(GameObject player) // When in range
    {
        _Text.text = _Info;
        _Text.fontSize = _FontSizeAfter;
    } 

    protected override void InteractStart(GameObject interacter)
    {
        interacter.GetComponent<Player>().SpawnToParade(gameObject);
        // scoreManager.AddScore(scoreValue);
    }

    protected override void InteractUpdate(GameObject interacter) // When pressed interact
    {} 

    protected override void Idle() // When not in range
    {} 
}

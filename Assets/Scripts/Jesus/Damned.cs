using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Damned : Interactable
{
	[SerializeField] Vector2 _BubblePosOffsetAfter;
    [SerializeField] float _FontSizeAfter;
    [SerializeField] [Multiline] string _Info;

	Vector2 _BubblePosOffsetBefore;

	void Start()
	{
		_BubblePosOffsetBefore = _BubbleObject.transform.localPosition;
        _BubbleObject.transform.localPosition = _BubblePosOffsetBefore;
	}

	protected override void ObjectUpdate()
    {

    }

    protected override void IsNear(GameObject player)
    {
        _Text.text = _Info;
        _Text.fontSize = _FontSizeAfter;
        
    }

    protected override void InteractStart(GameObject interacter)
    {
        interacter.GetComponent<Player>().CarryPerson(gameObject);
    }

    protected override void InteractUpdate(GameObject interacter)
    {
        
    }

    protected override void Idle()
    {

    }

}

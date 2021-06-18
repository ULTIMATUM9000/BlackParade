using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class InfoGiver : Interactable
{
    [SerializeField] Vector2 _BubblePosOffsetAfter;
    [Space]
    [SerializeField] float _FontSizeAfter;
    [SerializeField] [Multiline] string _Info;


    Vector2 _BubblePosOffsetBefore;

    //float _Radius = 1f;
    //RaycastHit2D _Hit;

    void Start()
    {
        _BubblePosOffsetBefore = _BubbleObject.transform.localPosition;

        _BubbleObject.transform.localPosition = _BubblePosOffsetBefore;
    }

    protected override void ObjectUpdate()
    {

    }

    protected override void InteractStart(GameObject interacter){}

    protected override void InteractUpdate(GameObject player)
    {
        _BubbleObject.transform.localPosition = _BubblePosOffsetAfter;
        _BubbleObject.transform.localScale = Vector2.one * 5;

        _Text.text = _Info;
        _Text.fontSize = _FontSizeAfter;
    }

    protected override void Idle()
    {
        _BubbleObject.transform.localPosition = _BubblePosOffsetBefore;
        _BubbleObject.transform.localScale = Vector2.one * 1;
    }

    protected override void IsNear(GameObject player)
    {
        _BubbleObject.SetActive(true);
    }
    //void Update()
    //{
    //    _Hit = Physics2D.CircleCast(transform.position, _Radius, Vector2.one, 1.5f, _PlayerMask);

    //    if(_Hit)
    //    {
    //        _BubbleObject.SetActive(true);

    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            _BubbleObject.transform.localPosition = _BubblePosOffsetAfter;
    //            _BubbleObject.transform.localScale = Vector2.one * 5;

    //            _Text.text = _Info;
    //            _Text.fontSize = _FontSizeAfter;
    //        }
    //    }

    //    else
    //    {
    //        _BubbleObject.transform.localPosition = _BubblePosOffsetBefore;
    //        _BubbleObject.transform.localScale = Vector2.one * 1;

    //        _Text.text = "Press E to Interact";
    //        _Text.fontSize = 1.5f;

    //        _BubbleObject.SetActive(false);
    //    }
    //}

}

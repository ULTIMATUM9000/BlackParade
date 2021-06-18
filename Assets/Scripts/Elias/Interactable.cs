using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected float _Radius;
    [SerializeField] protected GameObject _BubbleObject;
    [SerializeField] protected TextMeshPro _Text;
    [SerializeField] LayerMask _InteractLayer;

    protected bool _Interacted = false;
    protected bool _Interactable = true;  //Should be false when tha damned is in the parade already

    RaycastHit2D _Hit;

    float _OrigFontSize;
    string _OrigText;

    void Awake()
    {
        _OrigFontSize = _Text.fontSize;
        _OrigText = _Text.text;

        _BubbleObject.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        ObjectUpdate();

        _Hit = Physics2D.CircleCast(transform.position, _Radius, Vector2.one, _Radius, _InteractLayer);

        if (_Hit && _Interactable)
        {
            if (_Interacted)
                InteractUpdate(_Hit.collider.gameObject);

            else
            {
                _BubbleObject.SetActive(true);

                _Text.text = _OrigText;
                _Text.fontSize = _OrigFontSize;

                IsNear(_Hit.collider.gameObject);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _Interacted = true;
                    InteractStart(_Hit.collider.gameObject);
                }
            }
        }

        else
        {
            _BubbleObject.SetActive(false);
            _Interacted = false;

            Idle();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _Radius);
    }

    protected abstract void ObjectUpdate(); // Update

    protected abstract void IsNear(GameObject player); // When in range

    protected abstract void InteractStart(GameObject interacter);

    protected abstract void InteractUpdate(GameObject interacter); // When pressed interact

    protected abstract void Idle(); // When not in range
}

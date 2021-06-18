using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YergoScripts;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask _LayerMask;
    [SerializeField] int _Speed;

    public int Damage { get; set; }
    
    Vector2 _Size;
    Vector2 _Direction;
    Collider2D _Collider;

    void Start() => _Size = GetComponent<SpriteRenderer>().bounds.size;

    void OnEnable()
    {
        StartCoroutine(Timer());
    }

    void Update()
    {
        _Collider = Physics2D.OverlapBox(transform.position, _Size, transform.eulerAngles.z, _LayerMask);

        if (_Collider)
        {
            //ZBDslime slime = _Collider.GetComponent<ZBDslime>();

            //if (slime)
                //slime.SlimeTakesDamage(Damage);

            StopCoroutine(Timer());
            gameObject.SetActive(false);
        }

        transform.Translate(transform.up * _Speed * Time.deltaTime);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
    }
}

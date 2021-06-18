using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class zbdEnemy : MonoBehaviour
{
    private Player player;

    [SerializeField] int enemyHealth = 10;
    [SerializeField] int enemyDamage = 5;
    [SerializeField] GameObject _Target;
    [SerializeField] LayerMask _TargetLayer;
    [SerializeField] float enemyRadius = 0.25f;
    [SerializeField] LayerMask layerMask;
    bool Attacking = false;
    [SerializeField] bool isImmuneToMelee = false; //change this in inspector if the enemy prefab is immune or not


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

        Collider2D col = Physics2D.OverlapCircle(transform.position, enemyRadius, layerMask);
        if (col.GetComponent<Player>())
        {
            if (Attacking == false)
            {
                StartCoroutine("attack");
            }
        }


        if (enemyHealth <= 0) //Enemy Dies here
        {
            enemyDead();
        }
    }



    //public void movement()
    //{

    //}

    public void EnemyTakesDamage(bool isMelee, int damage) //If attack is melee make bool true, if attack is not make bool false
    {
        if (isMelee == true && isImmuneToMelee == false) //depends on the enemy prefab if it is immune or not
        {
            enemyHealth -= damage; // enemy takes damage
            if (enemyHealth <= 0) //Enemy Dies here
            {
                enemyDead();
            }
        }
        if (isMelee == false && isImmuneToMelee == true) 
        {
            enemyHealth -= damage; // enemy takes damage
            if (enemyHealth <= 0) //Enemy Dies here
            {
                enemyDead();
            }
        }

    }

    IEnumerator attack()
    {
        Attacking = true; //so that corroutine doesnt make multiple versions
        yield return new WaitForSeconds(2.0f); //ammount of time before it attacks
        Collider2D col = Physics2D.OverlapCircle(transform.position, enemyRadius, layerMask);
        if (col.GetComponent<Player>())
        {
            player.TakeDamage(enemyDamage);
        }
        yield return new WaitForSeconds(1.0f); //ammount of time he waits to attack again total of 3 seconds MIGHT REMOVE THIS

        Attacking = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyRadius);
    }

    private void enemyDead()
    {
        this.gameObject.SetActive(false);
    }
}

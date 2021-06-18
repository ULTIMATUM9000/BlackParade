using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using YergoScripts;
using YergoScripts.ObjectPool;

public class Player : MonoBehaviour
{
    [SerializeField] int _MaxHealth;
    [SerializeField] float _WalkSpeed;
    [SerializeField] float _RunSpeed;
    [SerializeField] float _SpeedTime = 1;
    [SerializeField] Ability _Ability1;
    [SerializeField] Ability _Ability2;

    Vector2 _Velocity;

    Vector2 _FaceDir = Vector2.up;

    Rigidbody2D _Rigidbody;
    
    public Vector2 Velocity { get => _Rigidbody.velocity; }
    public Vector2 FaceDirection { get => _FaceDir; }
    public bool IsDamaged { get; private set; }
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get => _MaxHealth; }
    public bool IsAlive { get => CurrentHealth > 0; }
    public GameObject PersonCarrying { get; private set; }
    public Ability Ability1 { get => _Ability1; private set => _Ability1 = value; }
    public Ability Ability2 { get => _Ability2; private set => _Ability2 = value; }

    float ParadeSpeed;
    ParadeMovement parade;

    [SerializeField] ScoreManager scoreManager;
    float scoreValue = 1;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();

        // Checkpoint will be in front of the parade

        parade = GameObject.FindGameObjectWithTag("Parade").GetComponent<ParadeMovement>();
    }

    void Update()
    {
        Camera.main.transform.position = transform.position - Vector3.forward * 10;

        Move();
        Abilities();

        if (!IsAlive)
            KillPlayer();
    }

    void Move()
    {
        _Velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (_Velocity != Vector2.zero)
            _FaceDir = _Velocity.normalized;

        _Velocity = _Velocity * (Input.GetKey(KeyCode.J) ? _RunSpeed : _WalkSpeed) * 0.1f;
        _Rigidbody.velocity = _Velocity;
    }

    void Abilities()
    {
        //MathY.Vector2ToDegree(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition))
        if (Input.GetKeyDown(KeyCode.Mouse0))
            //_Ability1.Activate(gameObject, MathY.Vector2ToDegree(_FaceDir) - 90f);
            _Ability1.Activate(gameObject, MathY.Vector2ToDegree(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)) + 90f);

        if (Input.GetKeyDown(KeyCode.Mouse1))
            //_Ability2.Activate(gameObject, MathY.Vector2ToDegree(_FaceDir) / 2 - 45f);
            _Ability2.Activate(gameObject, MathY.Vector2ToDegree(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)) / 2 - 135f);
    }

    void KillPlayer()
    {
        DropPerson(PersonCarrying);

        transform.position = Checkpoint.CurrentCheckpoint;
        CurrentHealth = MaxHealth;
    }

    IEnumerator Damaged()
    {
        IsDamaged = true;

        yield return new WaitForSeconds(0.1f);

        IsDamaged = false;
    }

    public void TakeDamage(int damage)
    {
        damage = CurrentHealth - damage;

        CurrentHealth = damage > 0 ? damage : 0;

        StartCoroutine(Damaged());
    }

    public void CarryPerson(GameObject person)
    {
        if(PersonCarrying != null)
            DropPerson(PersonCarrying);

        PersonCarrying = person;
        PersonCarrying.SetActive(false);
    }

    public void DropPerson(GameObject person)
    {
        if(PersonCarrying != null)
        {
            PersonCarrying.SetActive(true);
            PersonCarrying.transform.position = transform.position;
            PersonCarrying = null;
        }
    }

    public void SpawnToParade(GameObject person)
    {
        //drop the damned to the parade and make them move

        if(PersonCarrying != null)
        {
            PersonCarrying.SetActive(true);
            PersonCarrying.transform.position = person.transform.position;
            Destroy(PersonCarrying); //temporary measure
            PersonCarrying = null;
            scoreManager.AddScore(scoreValue);
        }
        
    }

    public bool DropToParade()
    {
        bool isCarrying = PersonCarrying;

        if (PersonCarrying)
        {
            isCarrying = true;

            // Object pool later
            //Destroy(PersonCarrying);

            PersonCarrying.SetActive(false); // Temp
            PersonCarrying = null;
        }

        return isCarrying;
    }
}

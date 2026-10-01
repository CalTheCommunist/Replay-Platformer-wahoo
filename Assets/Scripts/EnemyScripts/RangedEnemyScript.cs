using UnityEngine;

public class RangedEnemyScript : MonoBehaviour
{
    private GameObject _target;
    public Vector3 directionToPlayer;
    private LayerMask SortingHat;
    public bool FoundPlayer;
    public enum States
    {
        Idle,
        Search,
        Attack
    }

    private float timer;
    
    public States CurrentState;

    private void Awake()
    {
        CurrentState = States.Idle;
        _target = GameObject.FindGameObjectWithTag("Player");
        SortingHat = LayerMask.GetMask("Player");
    }

    void Update()
    {
        directionToPlayer = Vector3.Normalize(_target.transform.position - transform.position);
    }
    private void FixedUpdate()
    {
        if (CurrentState == States.Idle)
        {
            Debug.Log("Enemy is Idle");
            timer = timer + Time.deltaTime;
            if(timer >= 5)
            {
                CurrentState = States.Search;
                timer = 0;
            }
        }
        if (CurrentState == States.Search) {
            Debug.Log("Enemy is Searching");
            if (FoundPlayer == true)
            {
                CurrentState = States.Attack;
                Debug.Log("Enemy is attacking");
            }
        looking();
        }
        if(FoundPlayer == false)
        {
            CurrentState = States.Idle;
        }

        

    }
    private void looking()
    {
        Ray ray = new(transform.position, directionToPlayer);
        if (Vector3.Dot(transform.TransformDirection(Vector3.forward), directionToPlayer) > 0.5f)
        {
            if(Physics.Raycast(ray, 25f, SortingHat) == true)
            {
                FoundPlayer = true;
                Debug.Log("Found the Player!");
            }
            else
            {
                FoundPlayer = false;
                Debug.Log("where the heck is the player? bubu.");
            }

        }
        else
        {
            FoundPlayer = false;
        }
    }
    private void hurlAnvil()
    {
       
    }

}

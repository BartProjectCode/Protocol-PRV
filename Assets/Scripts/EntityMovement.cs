using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityMovement : MonoBehaviour
{
    //Always useful variables
    [SerializeField] private Vector3 nextPos;
    private float _effectiveSpeed;
    private Transform _ground;
    
    //Random Movement variables
    [SerializeField] private float speed;
    [SerializeField] private float tolerance = 0.5f;
    
    //Attack Mode et Flee Mode variables
    public GameObject target = null;
    public GameObject predator = null;
    public bool attackMode;
    public bool fleeMode;

    public GameObject groundGO;
    //public bool fatigue;
    [SerializeField] private float timerAttack = 4f;
    [SerializeField] private float timerFlee = 4f;
    //[SerializeField] private float timerFatigue = 2f;
    [SerializeField] private float speedModifierAttack = 1.5f;
    [SerializeField] private float speedModifierFleeing = 1.5f;
    //[SerializeField] private float speedModifierFatigue = 1f;
    private float _t = 0;

    private void Start()
    {
        //Initialization
        attackMode = false;
        fleeMode = false;
        //fatigue = false;
        _effectiveSpeed = speed;
        groundGO = GameObject.FindGameObjectWithTag("Ground");
        _ground = groundGO.transform;
        
        //Setting a position for the object to reach
        NewDestination();
    }

    private void Update()
    {
        //Making the object face in the direction it's moving
        transform.forward = (nextPos - transform.position).normalized;
        
        //Verifying if the object is "fatigued"
        // if (fatigue)
        // {
        //     _t += Time.deltaTime;
        //
        //     if (_t <= timerFatigue)
        //     {
        //         //If it is, it can't attack nor flee, and its speed is affected.
        //         attackMode = false;
        //         fleeMode = false;
        //         _effectiveSpeed = speed * speedModifierFatigue;
        //     }
        //     else
        //     {
        //         //At the end of the "fatigued" status, its variables reinitialize.
        //         _t = 0;
        //         _effectiveSpeed = speed;
        //         fatigue = false;
        //     }
        // }

        //Verifying if no other status applies to the object
        if (!attackMode && !fleeMode)
        {
            if (Mathf.Abs(transform.position.x - nextPos.x) <= tolerance && Mathf.Abs(transform.position.z - nextPos.z) <= tolerance)
            {
                //If it already reached its destination, with a margin of "tolerance", another destination is attributed.
                NewDestination();
            }
        }
        //In case of the "attacking" status applying
        else if (attackMode) //&& !fatigue
        {
            //Too focused to flee, now!
            fleeMode = false;
            _t += Time.deltaTime;

            if (_t <= timerAttack)
            {
                //Focussing on the enemy and chasing it during a limited period.
                nextPos = target.transform.position;
                _effectiveSpeed = speed * speedModifierAttack;
            }
            else
            {
                //At the end of the "attacking" status, its variables reinitialize.
                _t = 0;
                //fatigue = true;
                _effectiveSpeed = speed;
                attackMode = false;
                NewDestination();
            }
        }
        //In case of the "fleeing" status applying
        else if (fleeMode && !attackMode) //&& !fatigue
        {
            _t += Time.deltaTime;

            if (_t <= timerFlee)
            {
                //Trying to go to the opposite direction relatively to the potential aggressor, without going OOB.
                transform.forward = (transform.position - predator.transform.position).normalized;
                nextPos = transform.position + transform.forward;
                nextPos = new Vector3(Mathf.Clamp(nextPos.x, -5 * _ground.localScale.x, 5 * _ground.localScale.x), 0f,
                    Mathf.Clamp(nextPos.z, -5 * _ground.localScale.z, 5 * _ground.localScale.z));
                _effectiveSpeed = speed * speedModifierFleeing;
            }
            else
            {
                //Again, at the end of the "fleeing" status, its variables reinitialize.
                _t = 0;
                //fatigue = true;
                _effectiveSpeed = speed;
                fleeMode = false;
                NewDestination();
            }
        }
        
        //Move the object to its destination with a speed previously defined.
        //transform.Translate((nextPos - transform.position).normalized * (Time.deltaTime * _effectiveSpeed), Space.World);
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        //Depending on the status, the Gizmos color changes.
        if (attackMode)
        {
            Gizmos.color = Color.red;
        }
        else if (fleeMode)
        {
            Gizmos.color = Color.yellow;
        }
        // else if (fatigue)
        // {
        //     Gizmos.color = Color.blue;
        // }
        else
        {
            Gizmos.color = Color.green;
        }
        
        //Indicates visually the direction faced by the object.
        Gizmos.DrawLine(transform.position, nextPos);
    }

    private void NewDestination()
    {
        //The formula that proposes a random destination for the object to reach.
        nextPos = _ground.position + new Vector3(Random.Range(-1f, 1f) * _ground.localScale.x * 5, transform.position.y, Random.Range(-1f, 1f) * _ground.localScale.z * 5);
    }
}

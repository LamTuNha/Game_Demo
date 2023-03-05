using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    Transform nextWaypoint;
    int waypointNum = 0;

    public float flightSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectonZone;    
    public List<Transform> waypoint;

    public Collider2D deathCollider;

    public bool _hasTarget = false;
    private int attackCooldown;

    public bool HasTarget { 
        get{
            return _hasTarget;
        } private set{
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        } 
    }

     public bool CanMove{
        get{
            return animator.GetBool(AnimationStrings.canMove);
        }set{
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start(){
        nextWaypoint = waypoint[waypointNum];
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectonZone.detectedColliders.Count > 0;
    }

    private void OnEnable() {
        damageable.damageableDeath.AddListener(OnDeath);
    }

    private void FixedUpdate(){
        if(damageable.IsAlive){
            if(CanMove){
                Flight();
            }else{
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight(){
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.velocity =  directionToWaypoint * flightSpeed;

        UpdateDerection();
        if(distance <= waypointReachedDistance){
            waypointNum++;
            if(waypointNum >= waypoint.Count){
                waypointNum = 0;
            }

            nextWaypoint = waypoint[waypointNum];
        }
    }
     
    private void UpdateDerection(){

        Vector3 localScale = transform.localScale;
        if(transform.localScale.x > 0){
            if(rb.velocity.x < 0){
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }else{
            if(rb.velocity.x < 0){
                transform.localScale = new Vector3(1 * localScale.x, localScale.y, localScale.z);
            }else{
                 transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
               
        }
    }

    public void OnDeath(){
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}

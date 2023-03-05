using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDerections), typeof(Damageable))] 
public class Bringer : MonoBehaviour
{
    TouchingDerections touchingDerections;
    Damageable damageable;
    public float walkAcceleration = 2f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f; 

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    Animator animator;

    public enum WalkableDirection {Right,Left};
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection{
        get{
            return _walkDirection;
        }
        set{
            if(_walkDirection != value){
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right){
                    walkDirectionVector = Vector2.right;
                }else if(value == WalkableDirection.Left){
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;

        }
    }

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

    public float AttackCooldown { 
        get{
            return animator.GetFloat(AnimationStrings.attackCooldown);
        } private set{
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        } 
    }

    // Start is called before the first frame update
    void Awake() {
        rb = GetComponent<Rigidbody2D>();      
        touchingDerections = GetComponent<TouchingDerections>(); 
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update(){
        HasTarget = attackZone.detectedColliders.Count > 0;

        if(attackCooldown >= 0) {
            AttackCooldown  -= Time.deltaTime;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(touchingDerections.IsGrounded && touchingDerections.IsOnWall ){
            FlipDirection();
        }
        if(!damageable.LockVelocity){
            if(CanMove && touchingDerections.IsGrounded){
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed)
                    , rb.velocity.y);
            }else{
                //animator.GetBool(AnimationStrings.canMove);
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate ), rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right){
            WalkDirection = WalkableDirection.Left;
        }else if(WalkDirection == WalkableDirection.Left){
            WalkDirection = WalkableDirection.Right;
        }else{
            Debug.LogError("Current walkable direction is not set  to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockBack){
        rb.velocity = new Vector2(knockBack.x,rb.velocity.y + knockBack.y);
    }

    public void OnCliffDetected(){
        if(touchingDerections.IsGrounded){
            FlipDirection();
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {    
        if(target.tag == "Destroy"){
            Destroy(gameObject);
        }
    }
}

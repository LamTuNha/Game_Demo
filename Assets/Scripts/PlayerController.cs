using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDerections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    TouchingDerections touchingDerections;
    Damageable damageable;

    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;
    public float jumpImpulse = 5f;
    public float jump = 4f;
    public float airWalkSpeed = 3f;    
    public float walkStopRate = 0.05f;  
    
    Rigidbody2D rb;
    Animator animator;
    Vector2 moveInput;

    [SerializeField]
    private bool _isMoving = true;   

    [SerializeField]
    private bool _isRunning = false;    

    [SerializeField]
    private bool _isFacingRight = true;    


    public float CurrentRunSpeed{
        get{
            if(CanMove){
                if(IsMoving && !touchingDerections.IsOnWall){
                    if( touchingDerections.IsGrounded){
                        if(IsRunning){
                            return RunSpeed;
                        }else{
                            return WalkSpeed;
                        }
                    }else{
                        return airWalkSpeed;
                    }                
                }else{
                    return 0;
                }

            }else{
                return 0;
            }
            
        }set{

        }
    }

    public float CurrentJump{
        get{
            if(IsRunning){
                return jumpImpulse;
            }else{
                return jump;
            }
        }set{

        }
    }       
    
    public bool IsMoving{ 
        get {
            return _isMoving;
        } 
        set {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsRunning{ 
        get {
            return _isRunning;
        }set {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
   
    public bool IsFacingRight{
        get{
            return _isFacingRight;
        }private set{
            if(_isFacingRight){
                transform.localScale *= new Vector2(-1,1);
            }else{
                transform.localScale *= new Vector2(-1,1);
            }
            _isFacingRight = value;
        }
    }

    public bool IsAlive{
        get{
            return animator.GetBool(AnimationStrings.isAlive);
        }set{

        }
    }

    public bool CanMove{
        get{
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDerections = GetComponent<TouchingDerections>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame

    private void FixedUpdate() {
        if(!damageable.LockVelocity){
            rb.velocity = new Vector2(moveInput.x * CurrentRunSpeed, rb.velocity.y);
        }

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    private void SetFacingDirection(Vector2 moveInput){
            if(moveInput.x > 0 && !IsFacingRight){
                IsFacingRight = true;

            }else if(moveInput.x < 0 && IsFacingRight){
                IsFacingRight = false;
            }
    }


    public void OnMove(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();
        if(IsAlive){
            IsMoving = moveInput !=Vector2.zero;
            SetFacingDirection(moveInput);
        }else{
            IsMoving = false;
        }
        
        
    }    

    public void OnRun(InputAction.CallbackContext context){
        if(context.started){
            IsRunning = true;
        }else if(context.canceled){
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context){
        if(IsAlive){
            if(context.started && touchingDerections.IsGrounded && CanMove){
            animator.SetTrigger(AnimationStrings.Jump);
            rb.velocity = new Vector2(rb.velocity.x, CurrentJump);
            }else if(context.canceled){
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context){
        if(context.started){
            animator.SetTrigger(AnimationStrings.AttackTrigger);
        }else if( context.canceled){
        }
    }
    public void OnRangedAttack(InputAction.CallbackContext context){
        if(context.started){
            animator.SetTrigger(AnimationStrings.rangedAttack);
            
        }else if( context.canceled){
        }
    }

     public void OnHit(int damage, Vector2 knockBack){
        rb.velocity = new Vector2(knockBack.x,rb.velocity.y + knockBack.y);
     }

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D target)
    {    
        if(!IsAlive ||target.tag == "Destroy"){
            Application.LoadLevel("GamePlayScene");
        }

    }
}

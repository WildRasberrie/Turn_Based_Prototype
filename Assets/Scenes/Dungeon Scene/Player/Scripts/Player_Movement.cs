using UnityEngine;
public class Player_Movement : MonoBehaviour
{
    PlayerInputActions input;
    Vector2 move;
    [SerializeField] float speed;
    DPadMovement dpad;
    Animator animator;
    Rigidbody rb;


    private void Awake()
    {
        dpad = GameObject.FindWithTag("DPad").GetComponent<DPadMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input = new PlayerInputActions();
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

    void FixedUpdate()
    {
        PlayerController();

        //print(move);
    }

    void PlayerController() {
        move = input.Player.Move.ReadValue<Vector2>();
    
        //play idle anim when not moving 
        if (move == Vector2.zero && 
            (!dpad.upPressed &&
             !dpad.downPressed &&
             !dpad.rightPressed && 
             !dpad.leftPressed)) {
            //set vel to 0 when not walking 
            rb.linearVelocity = Vector2.zero;
            animator.Play("Idle");

        }

      

        //Arrow Key Movements
            //if moving up on y, set animation to walking up 
            if (move.y > 0 || dpad.upPressed)
            {
                //set animation to walk up
                animator.Play("Forward_Walk");
            }
            //if moving down on y, set animation to walking down
            else if (move.y < 0 || dpad.downPressed)
            {
                animator.Play("Back_Walk");
            }

            if (move.x > 0 || dpad.rightPressed)
            {
                animator.Play("Right_Walk");
            }
            else if (move.x < 0 || dpad.leftPressed)
            {
                animator.Play("Left_Walk");
            }


        //DPad Movement 
        if (dpad.upPressed)
        {
            move.y++;
        }
        else if (dpad.downPressed)
        {
            move.y--;
        }

        if (dpad.rightPressed)
        {
            move.x++;
        }
        else if (dpad.leftPressed)
        {
            move.x--;
        }

        if (move != Vector2.zero)
        {
            rb.linearVelocity = move * speed;
        }

    }

}


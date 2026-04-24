using UnityEngine;
//use input actions system 
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    PlayerInputActions input;
    Vector2 move;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    }

    void PlayerController() {
        move = input.Player.Move.ReadValue<Vector2>();
        var animator = GetComponent<Animator>();
        var rb = GetComponent<Rigidbody>();
        //play idle anim when not moving 
        if (move == Vector2.zero) {
            //set vel to 0 when not walking 
            rb.linearVelocity = Vector2.zero;
            animator.Play("Idle");

        }

        if (move != Vector2.zero) { 
            rb.linearVelocity = move * speed;
            //if moving up on y, set animation to walking up 
            if (move.y > 0)
            {
                //set animation to walk up
                animator.Play("Forward_Walk");
            }
            //if moving down on y, set animation to walking down
            else if (move.y < 0)
            {
                animator.Play("Back_Walk");
            }

            if (move.x > 0)
            {
                animator.Play("Right_Walk");
            }
            else if (move.x < 0)
            {
                animator.Play("Left_Walk");
            }
        }
    }

}


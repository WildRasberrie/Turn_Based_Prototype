using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player_Movement : MonoBehaviour
{
    PlayerInputActions input;
    Vector2 move;
    Vector3 mousePos;
    [SerializeField] float speed;
    public Animator animator;
    Rigidbody rb;
    public Camera cam;
    //public GameObject Waypoint;
    public Vector3 offset;
    Vector3 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
       /// Waypoint.SetActive(false);
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
        var scene = SceneManager.GetActiveScene().name;

        if (scene == "Dungeon_lvl1") this.gameObject.SetActive(true);
        if (scene != "Dungeon_lvl1") this.gameObject.SetActive(false);



        PlayerController();

        //print(mousePos);

        //print(move);

       // print (direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(mousePos, transform.forward);
    }

    void PlayerController()
    {
        
        var clicked = Mouse.current.leftButton.isPressed;

        if (clicked)
        {
            mousePos = //convert to world position
                  cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (Vector3.Distance(transform.position, mousePos) > 0.5f)
            {
                //Waypoint.SetActive(true);
                //Waypoint.transform.position = mousePos;
                direction = (mousePos - transform.position).normalized;
                rb.linearVelocity = new Vector3(direction.x, direction.y, 0) * speed;
            }
            //walking anims

            if (direction.y > 0)
            {
                //set animation to walk up
                animator.Play("Forward_Walk");
            }
       
            if (direction.y < 0) { 
                animator.Play("Back_Walk");
            }

      
            if (direction.x > 0)
            {

                animator.Play("Right_Walk");
            }
          
            if (direction.x < 0)
            {
                animator.Play("Left_Walk");
            }
        }

        if (transform.position == mousePos)
        {
            rb.linearVelocity = Vector3.zero;
            ///Waypoint.SetActive(false);
            animator.Play("Idle");
        }
        
        
    }

    Vector3 TrackAngle() {
       return new Vector3( Vector3.Dot(transform.position,transform.right), Vector3.Dot(transform.position, transform.forward)).normalized;
    }


}
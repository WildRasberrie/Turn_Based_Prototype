using UnityEngine;
using UnityEngine.AI;
public class Player_Movement : MonoBehaviour
{
    PlayerInputActions input;
    Vector2 move;
    Vector3 mousePos;
    [SerializeField] float speed;
    Animator animator;
    Rigidbody rb;
    public Camera cam;
    public GameObject Waypoint;
    public NavMeshAgent player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Waypoint.SetActive(false);
    }

    private void OnEnable()
    {
        input = new PlayerInputActions();
        input.Enable();
    }
    

    void FixedUpdate()
    {
        PlayerController();

        //print(mousePos);

        //print(move);

        print (TrackAngle());
    }

    void PlayerController()
    {
        
        var clicked = Input.GetMouseButtonUp(0);

        if (clicked)
        {
            mousePos = cam.ScreenPointToRay(Input.mousePosition).origin;
            
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
               player.SetDestination(hit.point);

                Waypoint.transform.position = hit.point;
            }
            //walking anims
    
            if (rb.linearVelocity.y > 0)
            {
                //set animation to walk up
                animator.Play("Forward_Walk");
            }
       
            if (rb.linearVelocity.y < 0) { 
                animator.Play("Back_Walk");
            }

      
            if (rb.linearVelocity.x > 0)
            {

                animator.Play("Right_Walk");
            }
          
            if (rb.linearVelocity.x < 0)
            {
                animator.Play("Left_Walk");
            }
        }

        if (transform.position == mousePos)
        {
            rb.linearVelocity = Vector3.zero;
            Waypoint.SetActive(false);
            animator.Play("Idle");
        }



    }

    Vector3 TrackAngle() {
       return new Vector3( Vector3.Dot(transform.right, mousePos), Vector3.Dot(transform.up, mousePos)).normalized;
    }


}
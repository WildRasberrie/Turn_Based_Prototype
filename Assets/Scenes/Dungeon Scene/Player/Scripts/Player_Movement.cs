using UnityEngine;
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

        if (clicked && Vector3.Distance(transform.position, mousePos) > 0.01f)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePos - transform.position).normalized;
            Waypoint.SetActive(true);

            if (rb.linearVelocity != Vector3.zero)
            {
                var current_target = new Vector3(mousePos.x, mousePos.y, Waypoint.transform.position.z);

                Waypoint.transform.position = current_target;
            }
            //Arrow Key Movements
            //if moving up on y, set animation to walking up 
            if (direction.y > 0.8)
            {
                rb.linearVelocity = new Vector3(0, speed);
            }
            if (rb.linearVelocity.y > 0)
            {
                //set animation to walk up
                animator.Play("Forward_Walk");
            }
            //if moving down on y, set animation to walking down
            else if (direction.y < -0.8)
            {
                rb.linearVelocity = new Vector3(0, -speed);

            }
            if (rb.linearVelocity.y < 0) { 
                animator.Play("Back_Walk");
            }

            if (direction.x > 0.8)
            {
                rb.linearVelocity = new Vector3(speed, 0, 0);
            }
            if (rb.linearVelocity.x > 0)
            {

                animator.Play("Right_Walk");
            }
            else if (direction.x < -0.8)
            {
                rb.linearVelocity = new Vector3(-speed, 0, 0);
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
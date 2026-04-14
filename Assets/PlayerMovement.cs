using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;
    public float trunSpeed = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    public float coyoteTime = 0.15f;
    public float coyoteTimeCounter;
    public bool realGrouned = true;
    public GameObject gliderObject;
    public float gliderFallSpeed = 1.0f;
    public float gliderMoveSpeed = 7,0f;
    public float gliderMaxSpeed = 5.0f;
    public float gliderMaxTime = 5.0f;
    public float gliderTimeLeft;
    public bool isGliding = false;


    public Rigidbody rb;

    public bool isGrounded = true;

    public int isGrounded = true;

    public int coinCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coyoteTimeCounter = 0;

        if (gliderObject != null)
        {
            gliderObject.SetActive(false);
        }

        gliderTimeLeft = gliderMaxTime;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateGroundedState()

        float moveHorizontal = Input.GetAxis("Horizontal"); 
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, trunSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.G) && !isGrounded && gliderTimeLeft > 0)
        {
            if (!isGliding)
                EnableGlider();

        }


        if (isGrounded)
        {
                if(isGliding)
            {
                DisableGlider();
            }

            gliderTimeLeft = gliderMaxTime;
        }

        gliderTimeLeft -= Time.deltaTime;

        if(gliderTimeLeft <= 0)
        {
            DisableGlider(); 
        }


        else if(isGliding)
        {
            DisableGlider();
        }

        if (isGlideing)
        {
            ApplyGliderMovenment(moveHorizontal, moveVertical);
        }

        else
        {


            rb.linearVelocity = new Vector3(moveHorizontal * moveSpeed, rb.linearVelocity, y, moveVercical * moveSpeed);

            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }

            else if (rb.linearVelocity.y > 0 && !Input,GetButton("jump"))
                {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; 
            }

            
        }





            rb.linearVelocity = new Vector3(moveHorizontal * moveSpeed, moveVertical * moveSpeed);

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.dletaTime;
        }

        else if(rb.linearVelocity.y > 0 && !Input.GetButton("jump"))
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.dletaTime;

        }

        if (Input.GetButtonDown("jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            realGrouned = false;
            coyoteTimeCounter = 0;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            realGrouned = true;
        }
    }




    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
        }
    }

}

void UpdateGroundedState()
{
    if (realGrouned)
    {
        coyoteTimCounter = coyoteTime;
        isGrounded = true;

    }

    else
    {


        if (coyoteTimeCounter > 0)
        {
            coyoteTimeCounter -= Time.deltaTime;
            isGrounded = true;


        }


        else
        {
            isGrounded = false;
        }




    }



    void EnableGlider()
    {
        isGliding = true;

        if (gliderObject != null)
        {
            gliderObject.SetActive(true);
        }


        rb.linearVelocity = new Vector3(RenderBuffer.linearVelocity.x, -gliderFallSpeed, rb.linearVelocity.z);


    }



    void ApplyGliderMovement(float horizontal , float vertical)
    {
        Vector3 gliderVelocity = new Vector3(
        horizontal * gliderMveSpeed,
        gliderFallSpeed,
        vertical * gkuderNiveSpeed
        );
    }

    rb.linearVelocity = gliderVelocity;














}

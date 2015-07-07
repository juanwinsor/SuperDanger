using UnityEngine;
using System.Collections;

public class PlayerCharacterController : MonoBehaviour {

    public Animator anim;

    private bool m_CurrentHitGround = true;
    private bool m_PreviousHitGround = true;

    float m_WalkSpeed = 100.0f;
    float m_MaxWalkSpeed = 2.0f;

    public Transform flashLight;

    public enum MovementState
    {
        MOVE_IDLE,
        MOVE_RIGHT,
        MOVE_LEFT,
        MOVE_JUMP,
        MOVE_FALL,
		MOVE_ATTACK_FRONT
    }
    MovementState m_MovementState = MovementState.MOVE_IDLE;

	// Use this for initialization
	void Start () 
    {
        //anim = transform.GetComponent<Animator>();
        anim.SetFloat( "velocity", 1.0f );
	}
	
	// Update is called once per frame
	void Update ()
    {
		CheckAttack();

        CheckJump();

		CheckInput();
	}

    void FixedUpdate()
    {
		CheckGroundCollision();
		
		ApplyMovement();       
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider != null)
        {
            if (coll.gameObject.tag == "Ground")
            {
                
                int i = 0;
            }
        }
    }

    void CheckInput()
    {
        //-- apply x velocity
        if (Input.GetKey(KeyCode.D) == true)
        {
            m_MovementState = MovementState.MOVE_RIGHT;
        }
        else if (Input.GetKey(KeyCode.A) == true)
        {
            m_MovementState = MovementState.MOVE_LEFT;
        }
        else if ((Input.GetKey(KeyCode.D) == false) && (Input.GetKey(KeyCode.A) == false))
        {
            m_MovementState = MovementState.MOVE_IDLE;
        }
    }

     Vector2 xAxisPlayerPos = Vector3.zero;
     Vector2 xAxisCollisionCheck = Vector3.zero;
    bool CheckCollisionInDirection()
    {
        bool result = false;

        float collisionDirection = 0;

        if( m_MovementState == MovementState.MOVE_LEFT )
        {
            collisionDirection = -1.0f;
        }
        else if (m_MovementState == MovementState.MOVE_RIGHT)
        {
            collisionDirection = 1.0f;
        }
        else
        {
            return false;
        }

        float yOffset = 0.0f;
		float xDistance = this.GetComponent<Collider2D>().bounds.extents.x + 0.2f; //(this.GetComponent<Collider2D>().bounds.extents.x) + (this.GetComponent<Rigidbody2D>().velocity.x * Time.fixedDeltaTime);


        //if (isOnGround() == true)
        {
            xAxisPlayerPos = new Vector2(transform.position.x, transform.GetComponent<Collider2D>().bounds.center.y + yOffset);
            xAxisCollisionCheck = new Vector2(xAxisPlayerPos.x + (collisionDirection * xDistance), transform.GetComponent<Collider2D>().bounds.center.y + yOffset);            
        }
        //else
        //{
        //    xAxisPlayerPos = new Vector2(transform.position.x, transform.position.y + yOffset);
		//	xAxisCollisionCheck = new Vector2(xAxisPlayerPos.x + (collisionDirection * xDistance), transform.position.y + yOffset);
        //}

        //-- check for collision in direction to know if to stop applying force on the x axis        
        RaycastHit2D raycastHit = Physics2D.Linecast(xAxisPlayerPos, xAxisCollisionCheck, 1 << LayerMask.NameToLayer("Ground"));
        if (raycastHit.collider != null)
        {
            result = true;
        }

        return result;
    }

    void OnGUI()
    {
        Debug.DrawLine(xAxisPlayerPos, xAxisCollisionCheck, Color.red);
    }

    void ApplyMovement()
    {
        Vector2 direction = Vector2.zero;

        if( m_MovementState == MovementState.MOVE_LEFT )
        {
            direction.x = -1.0f;            
        }
        else if ( m_MovementState == MovementState.MOVE_RIGHT )
        {
            direction.x = 1.0f;
        }
        else if( m_MovementState == MovementState.MOVE_IDLE )
        {     
            direction.x = 0;
        }

        //-- update scale to face make sprite face direction
        if( direction.x != 0 )
        {
            this.transform.localScale = new Vector3( direction.x, 1.0f, 1.0f );
            //-- update the animation
            anim.SetFloat( "velocity", 1.0f );
        }
        else
        {
            //-- update the animation
            anim.SetFloat( "velocity", 0 );
        }


        if( direction.x != 0 )
        {
          if (flashLight != null )
          {
            flashLight.rotation = Quaternion.AngleAxis(90.0f * direction.x, Vector3.up);
          }
        }


        if( CheckCollisionInDirection() == false )
        {
            //-- update the physics forces
            //m_WalkSpeed = 400.0f;
            if( this.GetComponent<Rigidbody2D>().velocity.x > m_MaxWalkSpeed )
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2( m_MaxWalkSpeed, this.GetComponent<Rigidbody2D>().velocity.y );
                //this.GetComponent<Collider2D>().sharedMaterial.friction = 1.0f;
            }
            else if (this.GetComponent<Rigidbody2D>().velocity.x < -m_MaxWalkSpeed)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2( -m_MaxWalkSpeed, this.GetComponent<Rigidbody2D>().velocity.y );
                //this.GetComponent<Collider2D>().sharedMaterial.friction = 1.0f;
            }
            else
            {
                if( m_MovementState != MovementState.MOVE_LEFT && m_MovementState != MovementState.MOVE_RIGHT )
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2( 0, this.GetComponent<Rigidbody2D>().velocity.y);
                    //this.collider2D.sharedMaterial.friction = 1.0f;
                }
                else
                {
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * m_WalkSpeed * Time.fixedDeltaTime, this.GetComponent<Rigidbody2D>().velocity.y);
                    //this.rigidbody2D.AddForce(new Vector2(direction.x * m_WalkSpeed * Time.fixedDeltaTime, 0), ForceMode2D.Force);
                    //this.collider2D.sharedMaterial.friction = 1.0f;
                }
            }
            
            //this.rigidbody2D.velocity = new Vector2(direction.x * m_WalkSpeed * Time.fixedDeltaTime, this.rigidbody2D.velocity.y);
        }

    }

    void CheckGroundCollision()
    {
		Vector2 playerPos = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.1f );
        Vector2 groundCheckPos = new Vector2(playerPos.x, playerPos.y - 0.4f);
        RaycastHit2D raycastHit = Physics2D.Linecast(playerPos, groundCheckPos, 1 << LayerMask.NameToLayer("Ground"));
        if (raycastHit.collider != null)
        {
            m_CurrentHitGround = true;
        }
        else
        {
            m_CurrentHitGround = false;
        }

        if (m_PreviousHitGround == false && m_CurrentHitGround == true)
        {
            m_MovementState = MovementState.MOVE_IDLE;
            //anim.SetTrigger("hitGround");
        }

        anim.SetBool("previousHitGround", m_PreviousHitGround);
        anim.SetBool("currentHitGround", m_CurrentHitGround);

        m_PreviousHitGround = m_CurrentHitGround;
    }

	void CheckAttack()
	{
		if( Input.GetKeyDown(KeyCode.F) == true )
		{
			anim.SetTrigger("FrontAttack");
		}
	}

    void CheckJump()
    {
        //-- check for jump input
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            if ( isOnGround() == true )
            {
                m_MovementState = MovementState.MOVE_JUMP;

                //this.rigidbody2D.AddForce(new Vector2(0, 150.0f));
                Vector2 jumpVelocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, this.GetComponent<Rigidbody2D>().velocity.y);
                jumpVelocity.y = 200.0f * Time.fixedDeltaTime;
                this.GetComponent<Rigidbody2D>().velocity = jumpVelocity;
                anim.SetTrigger("jump");
            }
        }

        anim.SetFloat("velocityY", this.GetComponent<Rigidbody2D>().velocity.y);
        if (this.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            m_MovementState = MovementState.MOVE_FALL;
        }
    }

    bool isOnGround()
    {
        return ( m_PreviousHitGround && m_CurrentHitGround );
    }
}

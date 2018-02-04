using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // --------------------------------------------------------------

    // The character's running speed
    [SerializeField]
    float m_RunSpeed = 5.0f;

    // The gravity strength
    [SerializeField]
    float m_Gravity = 60.0f;

    // The maximum speed the character can fall
    [SerializeField]
    float m_MaxFallSpeed = 20.0f;

    // The character's jump height
    [SerializeField]
    float m_JumpHeight = 4.0f;

    // The character's ability to double jump
    public bool jumpPowerUp = false;
    bool canDoubleJump;
    

    // Identifier for Input
    [SerializeField]
    string m_PlayerInputString = "_P1";

    // The character's hands
    Transform m_rightFist;
    Transform m_leftFist;
    Transform m_rightFistPos;
    Transform m_leftFistPos;

    // The character's punch range
    [SerializeField]
    float m_PunchRange = 1.0f;


    // --------------------------------------------------------------

    // The charactercontroller of the player
    CharacterController m_CharacterController;

    // The current movement direction in x & z.
    Vector3 m_MovementDirection = Vector3.zero;

    // The current movement speed
    float m_MovementSpeed = 0.0f;

    // The current vertical / falling speed
    float m_VerticalSpeed = 0.0f;

    // The current movement offset
    Vector3 m_CurrentMovementOffset = Vector3.zero;

    // The starting position of the player
    Vector3 m_SpawningPosition = Vector3.zero;

    // Whether the player is alive or not
    bool m_IsAlive = true;

    // The time it takes to respawn
    const float MAX_RESPAWN_TIME = 1.0f;
    float m_RespawnTime = MAX_RESPAWN_TIME;

    // The time it takes for a powerUp to wear off
    const float MAX_POWERUP_TIME = 15.0f;
    float m_PowerUpTime = MAX_POWERUP_TIME;

    // The character's current punching status
    bool m_punching = false;

    // --------------------------------------------------------------


    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
       
    }

    // Use this for initialization
    void Start()
    {
        m_SpawningPosition = transform.position;
        m_rightFist = m_CharacterController.transform.Find("RightFist");
        m_leftFist = m_CharacterController.transform.Find("LeftFist");
        m_rightFistPos = m_CharacterController.transform.Find("RightFistOrgPos");
        m_leftFistPos = m_CharacterController.transform.Find("LeftFistOrgPos");
    }

    void Jump()
    {
        m_VerticalSpeed = Mathf.Sqrt(m_JumpHeight * m_Gravity);
    }

    void ApplyGravity()
    {
        // Apply gravity
        m_VerticalSpeed -= m_Gravity * Time.deltaTime;

        // Make sure we don't fall any faster than m_MaxFallSpeed.
        m_VerticalSpeed = Mathf.Max(m_VerticalSpeed, -m_MaxFallSpeed);
        m_VerticalSpeed = Mathf.Min(m_VerticalSpeed, m_MaxFallSpeed);
    }

    void UpdateMovementState()
    {
        // Get Player's movement input and determine direction and set run speed
        float horizontalInput = Input.GetAxisRaw("Horizontal" + m_PlayerInputString);
        float verticalInput = Input.GetAxisRaw("Vertical" + m_PlayerInputString);

        m_MovementDirection = new Vector3(horizontalInput, 0, verticalInput);
        m_MovementSpeed = m_RunSpeed;
    }

    void UpdateJumpState()
    {
        // Character can jump when standing on the ground
        if (Input.GetButtonDown("Jump" + m_PlayerInputString))
        {
            if(m_CharacterController.isGrounded)
            {
                Jump();
                if (jumpPowerUp)
                {
                    Debug.Log(jumpPowerUp + "JumpPowerUp");
                    
                    canDoubleJump = true;

                    Debug.Log(canDoubleJump + "can double jump");
                }
            }
            else
            {
                if (canDoubleJump)
                {
                    Debug.Log(canDoubleJump + "canDoubleJump part 2");
                    canDoubleJump = false;
                    Jump();
                }
            }
        }




    }

    // Update is called once per frame
    void Update()
    {
        // If the player is dead update the respawn timer and exit update loop
        if(!m_IsAlive)
        {
            UpdateRespawnTime();
            return;
        }

        // Update movement input
        UpdateMovementState();

        // Update jumping input and apply gravity
        UpdateJumpState();
        ApplyGravity();

        //Update Punch Status
        UpdatePunchState();
        // Calculate actual motion
        m_CurrentMovementOffset = (m_MovementDirection * m_MovementSpeed + new Vector3(0, m_VerticalSpeed, 0)) * Time.deltaTime;

        // Move character
        m_CharacterController.Move(m_CurrentMovementOffset);

        // Rotate the character in movement direction
        if(m_MovementDirection != Vector3.zero)
        {
            RotateCharacter(m_MovementDirection);
        }
    }

    void RotateCharacter(Vector3 movementDirection)
    {
        Quaternion lookRotation = Quaternion.LookRotation(movementDirection);
        if (transform.rotation != lookRotation)
        {
            transform.rotation = lookRotation;
        }
    }

    public int GetPlayerNum()
    {
        if(m_PlayerInputString == "_P1")
        {
            return 1;
        }
        else if (m_PlayerInputString == "_P2")
        {
            return 2;
        }

        return 0;
    }

    public void Die()
    {
        m_IsAlive = false;
        m_RespawnTime = MAX_RESPAWN_TIME;
    }

    void UpdateRespawnTime()
    {
        m_RespawnTime -= Time.deltaTime;
        if (m_RespawnTime < 0.0f)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        m_IsAlive = true;
        transform.position = m_SpawningPosition;
        transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }

    IEnumerator Punch(float time, float distance, Vector3 direction, int side)
    {
        if (side == 0)
        {
            m_punching = true;
            float timer = 0.0f;
            direction.Normalize();
            while (timer <= time)
            {
                m_rightFist.transform.position = m_rightFistPos.transform.position + (Mathf.Sin(timer / time * Mathf.PI) + m_PunchRange) * direction;
                yield return null;
                timer += Time.deltaTime;
            }
            m_rightFist.transform.position = m_rightFistPos.transform.position;
            m_punching = false;
        }
        else
        {
            m_punching = true;
            float timer = 0.0f;
            direction.Normalize();
            while (timer <= time)
            {
                m_leftFist.transform.position = m_leftFistPos.transform.position + (Mathf.Sin(timer / time * Mathf.PI) + m_PunchRange) * direction;
                yield return null;
                timer += Time.deltaTime;
            }
            m_leftFist.transform.position = m_leftFistPos.transform.position;
            m_punching = false;
        }
    }

    void UpdatePunchState()
    {
        // Character can punch
        if (Input.GetButtonDown("Punch" + m_PlayerInputString) && !m_punching)
        {
            
            int side = Random.Range(0, 2);
            StartCoroutine(Punch(0.5f, 1.25f, transform.forward, side));
        }
    }

    public void IncreaseJumps()
    {
        jumpPowerUp = true;
        Invoke("ReduceJumps", m_PowerUpTime);
    }

    public void ReduceJumps()
    {
        jumpPowerUp = false;
    }

    public void IncreaseSpeed()
    {
        m_RunSpeed = 10f;
        Invoke("ReduceSpeed", m_PowerUpTime);
    }

    public void ReduceSpeed()
    {
        m_RunSpeed = 5f;
    }

    public void IncreaseRange()
    {
        m_PunchRange = 2.0f;
        Invoke("ReduceRange", m_PowerUpTime);
    }

    public void ReduceRange()
    {
        m_PunchRange = 1.0f;
    }

    public bool isPunching()
    {
        return m_punching;
    }
}

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float m_MovingTurnSpeed = 360;
    [SerializeField]
    float m_StationaryTurnSpeed = 180;
    [SerializeField]
    float m_JumpPower = 6.1f;
    [Range(1f, 20f)]
    [SerializeField]
    float m_Gravity = 20f;
    // [SerializeField]
    // float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField]
    float m_MoveSpeedMultiplier = 1f;
    [SerializeField]
    float m_GroundCheckDistance = 0.5f;
    [SerializeField]
    float m_turningRadius = 2.5f;
    [SerializeField]
    float m_SlideAngle = 45f;
    
    public static FMOD.Studio.EventInstance soundJump;
    public static FMOD.Studio.EventInstance soundland;
    public static FMOD.Studio.ParameterInstance soundLandParam;

    private particletest[] jumpParticles;
    CharacterController m_CharCtrl;
    Animator m_Animator;
    private float idleSpecial = 0;
    public bool m_IsGrounded;
    const float k_Half = 0.5f;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    Vector3 m_CollisionNormal;
    int layerMask;
    private float ySpeed = -5f;
    void Awake()
    {
        jumpParticles = GetComponentsInChildren<particletest>();
        soundJump = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Boots_Jump");
        soundland = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Jump");
        soundland.getParameter("Parameter 1", out soundLandParam);
        soundLandParam.setValue(0.21f);

        if (StateController.savedPosition)
        {
            transform.rotation = StateController.playerRot;
            transform.position = StateController.playerPos;
        }
    }
    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Player");
        layerMask = ~layerMask;
        m_Animator = GetComponent<Animator>();
        m_CharCtrl = GetComponent<CharacterController>();

    }

    public bool get_grounded()
    {
        return m_IsGrounded;
    }

    private bool wasGrounded = false;
    public void Move(Vector3 move, bool jump)
    {
        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);

        //lower movespeed in ramps
        CheckGroundStatus();
        move = move * (1f - Vector3.Angle(m_GroundNormal, Vector3.up) / 90f);

        // we don't want negative zero
        if (move.x == 0)
            move.x = 0;
        if (move.z == 0)
            move.z = 0;
        m_TurnAmount = Mathf.Atan2(move.x, move.z) * m_turningRadius;
        m_ForwardAmount = move.z;

        ApplyExtraTurnRotation();

        wasGrounded = m_IsGrounded;
        m_IsGrounded = m_CharCtrl.isGrounded;

        if (wasGrounded == false && m_IsGrounded) //landed
        {
            soundland.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            soundland.start();
        }
        if (m_IsGrounded)
        {
            ySpeed = -5;
            // to step, slide down
            float groundAngle = Vector3.Angle(m_GroundNormal, Vector3.up);

            //prevent getting stuck on "walls"
            if (m_IsGrounded && m_GroundNormal == Vector3.up && Mathf.Abs(m_CollisionNormal.y) < 0.5f && (Mathf.Abs(m_CollisionNormal.x) > 0.8f || Mathf.Abs(m_CollisionNormal.z) > 0.8f))
            {
                move = Vector3.ProjectOnPlane(m_CollisionNormal, transform.up) * 5;
                m_IsGrounded = false;
            }
            else if (m_IsGrounded && groundAngle > m_SlideAngle)
            {
                //slide more if higher angle
                move = Vector3.Lerp(transform.forward * m_ForwardAmount * m_MoveSpeedMultiplier, Vector3.ProjectOnPlane(m_GroundNormal, transform.up) * 2, (groundAngle - m_SlideAngle) / m_SlideAngle);
                if (jump)
                {
                    Jump();
                }
            }
            //ordinary move
            else
            {
                if (jump)
                {
                    Jump();
                }
                move = transform.forward * m_ForwardAmount * m_MoveSpeedMultiplier;
            }
            if (ySpeed > -5)
                ySpeed -= m_Gravity * Time.deltaTime;
            else ySpeed = -5;
        }
        else
        {
            move = transform.forward * m_ForwardAmount * m_MoveSpeedMultiplier;
            if (ySpeed > -20)
                ySpeed -= m_Gravity * Time.deltaTime;
            else ySpeed = -20;
        }


        // Debug.Log(ySpeed);
        move.y = ySpeed;
        m_CharCtrl.Move(move * Time.deltaTime);
           
        // send input and other state parameters to the animator
        UpdateAnimator();
    }

    void Jump()
    {
        for(int i = 0; i < jumpParticles.Length; i++)
        {
            jumpParticles[i].PlayParticles();
        }
        
        soundJump.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        soundJump.start();
        ySpeed = m_JumpPower;
    }

    void UpdateAnimator()
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        idleSpecial += Time.deltaTime * Random.Range(0f, 0.5f);

        if (idleSpecial > 1.0f)
        {
            idleSpecial = 0;
            m_Animator.SetBool("IdleSpecial", true);
        }
    

        else
        {
            m_Animator.SetBool("IdleSpecial", false);
        }

        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_CharCtrl.velocity.y);
        }
        else
        {
            m_Animator.SetFloat("Jump", 0);
        }
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);

        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        m_CollisionNormal = hit.normal;
    }



    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance, layerMask))
        {
            m_GroundNormal = hitInfo.normal;
        }
        else
        {
            m_GroundNormal = Vector3.up;
        }
    }

    public void playBark(float delay)
    {

    }
}


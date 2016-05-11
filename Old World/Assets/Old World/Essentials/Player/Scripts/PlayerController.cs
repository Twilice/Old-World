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
    float m_Gravity = 10f;
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

    CharacterController m_CharCtrl;
    Animator m_Animator;
    public bool m_IsGrounded;
    const float k_Half = 0.5f;
    float m_TurnAmount;
    float m_ForwardAmount;
    float velocity;
    public float air_accelerate = 3;
    public float max_speed = 0.1f;
    Vector3 m_GroundNormal;
    Vector3 m_CollisionNormal;
    int layerMask;
    private float ySpeed = -5f;
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

        m_IsGrounded = m_CharCtrl.isGrounded;

        if (m_IsGrounded)
        {
            ySpeed = -5;
            // to step, slide down
            float groundAngle = Vector3.Angle(m_GroundNormal, Vector3.up);
          
            //prevent getting stuck on "walls"
            if (m_IsGrounded && m_GroundNormal == Vector3.up && Mathf.Abs(m_CollisionNormal.y) < 0.5f && (Mathf.Abs(m_CollisionNormal.x) > 0.8f || Mathf.Abs(m_CollisionNormal.z) > 0.8f))
            {
                move = Vector3.ProjectOnPlane(m_CollisionNormal, transform.up)*5;
                m_IsGrounded = false;
            }
            else if (m_IsGrounded && groundAngle > m_SlideAngle)
            {
                //slide more if higher angle
                move = Vector3.Lerp(transform.forward * m_ForwardAmount * m_MoveSpeedMultiplier, Vector3.ProjectOnPlane(m_GroundNormal, transform.up) * 2, (groundAngle - m_SlideAngle) / m_SlideAngle);
                if (jump)
                    ySpeed = m_JumpPower;
            }
            //ordinary move
            else
            {
                if (jump)
                {
                    ySpeed = m_JumpPower;
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
            if (ySpeed > -10)
                ySpeed -= m_Gravity * Time.deltaTime;
            else ySpeed = -10;
        }


        // Debug.Log(ySpeed);
        move.y = ySpeed;
        m_CharCtrl.Move(move * Time.deltaTime);

        // send input and other state parameters to the animator
        UpdateAnimator();
    }


    void UpdateAnimator()
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("OnGround", m_IsGrounded);
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
}


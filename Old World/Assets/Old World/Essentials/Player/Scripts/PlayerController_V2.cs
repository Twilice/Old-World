using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerController_V2 : MonoBehaviour
{
    [SerializeField]
    float m_MovingTurnSpeed = 360;
    [SerializeField]
    float m_StationaryTurnSpeed = 180;
    [SerializeField]
    float m_JumpPower = 6.1f;
    [Range(1f, 4f)]
    [SerializeField]
    float m_GravityMultiplier = 2f;
   // [SerializeField]
   // float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField]
    float m_MoveSpeedMultiplier = 1f;
    [SerializeField]
    float m_AnimSpeedMultiplier = 1f;
    [SerializeField]
    float m_GroundCheckDistance = 0.5f;
    [SerializeField]
    float m_turningRadius = 2.5f;
    [SerializeField]
    float m_SlideAngle = 45f;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    public bool m_IsGrounded;
    float m_OrigGroundCheckDistance;
    const float k_Half = 0.5f;
    float m_TurnAmount;
    float m_ForwardAmount;
	float velocity;
	public float air_accelerate = 3;
	public float max_speed = 0.1f;
	Vector3 m_GroundNormal;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;
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

        CheckGroundStatus();
        if (Vector3.Angle(m_GroundNormal, Vector3.up) > m_SlideAngle)
        {
            //slide
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        }
        else
        {
            move = Vector3.ProjectOnPlane(move, Vector3.up);
        }
        // we don't want negative zero
        if (move.x == 0)
            move.x = 0;
        if (move.z == 0)
            move.z = 0;
        //move.z =  -0f; // Force the error each time
        m_TurnAmount = Mathf.Atan2(move.x, move.z) * m_turningRadius;
        m_ForwardAmount = move.z;

		ApplyExtraTurnRotation();

        // control and velocity handling is different when grounded and airborne:
        if (m_IsGrounded)
        {
            HandleGroundedMovement(move, jump);
        }
        else
        {
            HandleAirborneMovement(move);
        }

        // send input and other state parameters to the animator
        UpdateAnimator(move);
    }


    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        /*float runCycle =
            Mathf.Repeat(
                m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        if (m_IsGrounded)
        {
            m_Animator.SetFloat("JumpLeg", jumpLeg);
        }*/

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }
    }


    void HandleAirborneMovement(Vector3 move)
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce);
		m_Rigidbody.velocity = new Vector3(-velocity * Mathf.Cos((m_Rigidbody.transform.forward.x + 1) * (0.5f * Mathf.PI)), m_Rigidbody.velocity.y, -velocity * Mathf.Cos((m_Rigidbody.transform.forward.z + 1) * (0.5f * Mathf.PI)));
		m_Rigidbody.velocity += new Vector3((-1 * Mathf.Cos((m_Rigidbody.transform.forward.x + 1) * (0.5f * Mathf.PI)) * move.z * air_accelerate), 0 , (-1 * Mathf.Cos((m_Rigidbody.transform.forward.z + 1) * (0.5f * Mathf.PI)) * move.z * air_accelerate));
		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
    }


    void HandleGroundedMovement(Vector3 move, bool jump)
    {

		//added gravity to groundmovement too so you dont fly over every small incline
		//m_Rigidbody.AddForce(new Vector3(0,-20,0));
       
        // m_Rigidbody.MovePosition(transform.position + transform.forward * m_Animator.GetFloat("Forward") * Time.deltaTime * m_MoveSpeedMultiplier);

        // check whether conditions are right to allow a jump:
        if (jump && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
			// jump!
			//velocity = Mathf.Sqrt(m_Rigidbody.velocity.x * m_Rigidbody.velocity.x + m_Rigidbody.velocity.z * m_Rigidbody.velocity.z);
			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
            m_IsGrounded = false;
            m_Animator.applyRootMotion = false;
            m_GroundCheckDistance = 0.25f;
        }
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);

        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }


    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (m_IsGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            v.y = m_Rigidbody.velocity.y;
            m_Rigidbody.velocity = v;
        }
    }


    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            m_Animator.applyRootMotion = false;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            m_Animator.applyRootMotion = false;
        }
    }
}

using System;
using UnityEngine;

[Serializable]
public class CameraLookAt
{
	//Singleton holder for the mouseLook
	private CameraLookAt() { }
	private static CameraLookAt instance;
	public static CameraLookAt GetMouseLook()
	{
        if (instance == null)
        {
            instance = new CameraLookAt();
        }
        return instance;
	}
    private float XSensitivity = 2f;
    private float YSensitivity = 2f;
    private bool clampVerticalRotation = true;
	private float MinimumX = -80F;
	private float MaximumX = 80F;
	private bool smooth = false;
	private float smoothTime = 5f;

    private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;

	public void Init(Transform character, Transform camera)
	{
        m_CharacterTargetRot = character.localRotation;

        //We don't need the camera rotation, since it should always be straight
        //m_CameraTargetRot = Quaternion.identity;
        m_CameraTargetRot = camera.localRotation;
    }


	public void LookRotation(Transform character, Transform camera)
	{
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
		m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

		if (clampVerticalRotation)
			m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

		if (smooth)
		{
			character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot, smoothTime * Time.deltaTime);
			camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
		}
		else
		{
			character.localRotation = m_CharacterTargetRot;
			camera.transform.localRotation = m_CameraTargetRot;
		}
	}

	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

		angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
        
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}

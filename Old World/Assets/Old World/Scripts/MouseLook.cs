using System;
using UnityEngine;

[Serializable]
public class MouseLook
{
	//Singleton holder for the mouseLook
	private MouseLook() { }
	private static MouseLook instance;
	public static MouseLook GetMouseLook()
	{
		if (instance == null)
			instance = new MouseLook();
		return instance;
	}
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	private bool smooth = false;
	public float smoothTime = 5f;
	public bool lockCursor = true;


	private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;
	private bool m_cursorIsLocked = true;

	public void Init(Transform character)
	{
		m_CharacterTargetRot = character.localRotation;

		//We don't need the camera rotation, since it should always be straight
		m_CameraTargetRot = Quaternion.identity;
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

	public void UpdateCursorLock()
	{
		//if the user set "lockCursor" we check & properly lock the cursos
		if (lockCursor)
			InternalLockUpdate();
	}

	private void InternalLockUpdate()
	{
		if (Input.GetKeyUp(KeyCode.F5))
		{
			GameObject.Find("/MainCamera").GetComponent<MouseOrbitImproved>().enabled = false;
			m_cursorIsLocked = false;
		}
		else if (Input.GetKeyUp(KeyCode.F6))
		{
			GameObject.Find("/MainCamera").GetComponent<MouseOrbitImproved>().enabled = true;
			m_cursorIsLocked = true;
		}

		if (m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (!m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
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

using UnityEngine;
using System.Collections;

public class MovementGRAVITY : MonoBehaviour
{
	public float mouseSensitivityX = 250f;
	public float mouseSensitivityY = 250f;
	public float walkSpeed = 8f;
	
	Transform cameraT;
	float verticalLookRotation;
	
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	
	void Start ()
	{
		cameraT = Camera.main.transform;
	}
	
	void Update ()
	{
		transform.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * Time.deltaTime * mouseSensitivityX);
		verticalLookRotation += Input.GetAxis ("Mouse Y") * Time.deltaTime * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp (verticalLookRotation, -60, 60);
		cameraT.localEulerAngles = Vector3.left * verticalLookRotation;
		
		Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp (moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.18f);
	}
	
	void FixedUpdate()
	{
		GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + transform.TransformDirection (moveAmount) * Time.fixedDeltaTime);
	}
}
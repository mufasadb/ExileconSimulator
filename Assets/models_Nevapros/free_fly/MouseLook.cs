using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

//	GameObject BM;
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;
	float rotationX = 0F;
	
	float mouseX = 0F;
	float mouseY = 0F;
	
	float Old1mouseX = 0F;
	float Old1mouseY = 0F;
	
	float Old2mouseX = 0F;
	float Old2mouseY = 0F;
	
	float Old3mouseX = 0F;
	float Old3mouseY = 0F;

	void Update ()
	{
		if (Input.GetMouseButton(0))
		{
			/*
			 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			 RaycastHit hit;
				if (Physics.Raycast(ray, out hit,100.0F, 1<<8))
				BM.SendMessage("Select", hit.collider.gameObject);
				else
				BM.SendMessage("Deselect");
			*/
		}
		//if (Input.GetMouseButton(1))
		//	{
			if (axes == RotationAxes.MouseXAndY)
				{
					mouseX = Input.GetAxis("Mouse X");
					mouseY = Input.GetAxis("Mouse Y");
					
					mouseX = Mathf.Lerp(Mathf.Lerp(Mathf.Lerp(Old3mouseX,Old2mouseX,0.05F), Old1mouseX, 0.05F), mouseX, 0.05F);
					mouseY = Mathf.Lerp(Mathf.Lerp(Mathf.Lerp(Old3mouseY,Old2mouseY,0.05F), Old1mouseY, 0.05F), mouseY, 0.05F);
					
					rotationX = transform.localEulerAngles.y + mouseX * sensitivityX;
					
					rotationY += mouseY * sensitivityY;
					rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
					

			
					transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			
					Old3mouseX = Old2mouseX;
					Old3mouseY = Old2mouseY;
			
					Old2mouseX = Old1mouseX;
					Old2mouseY = Old1mouseY;
				
					Old1mouseX = mouseX;
					Old1mouseY = mouseY;
				}
				else if (axes == RotationAxes.MouseX)
				{
					transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
				}
				else
				{
					rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
					rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
					
					transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
				}
		//}
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		//BM = GameObject.Find("_BM");
	}
}
using UnityEngine;
using System.Collections;

public class hovermodel_free_fly : MonoBehaviour {
	public Rigidbody Rig;
	public GameObject Cam;
	
	
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	//float rotationY = 0F;
	//float rotationX = 0F;
	
//	float interpolateParam = 1.0F;
	
	// Use this for initialization
	void Start () {
        //Rig = gameObject.GetComponent<Rigidbody>();
        Cursor.visible = false;
	}

	
	
	// Update is called once per frame
	void Update () {

		//Rig.AddForce(new Vector3(0.0f,10.0f,0.0f));
		/*
		if (Input.GetKey("w")) Rig.velocity = Cam.transform.forward.normalized*20.0f;
		if (Input.GetKey("s")) Rig.velocity = Cam.transform.forward.normalized*-20.0f;
		if (Input.GetKey("a")) Rig.velocity = Cam.transform.right.normalized*-15.0f;
		if (Input.GetKey("d")) Rig.velocity = Cam.transform.right.normalized*15.0f;
		
		if (Input.GetKey("o")) transform.RotateAroundLocal(new Vector3(0.0f,5.0f,0.0f), 0.02f);
		if (Input.GetKey("p")) Rig.angularVelocity = new Vector3(0.0f,1.0f,0.0f);

		*/
		
		if (Input.GetKey("w")) 				Rig.AddForce(Cam.transform.forward.normalized*130.0f);
		if (Input.GetKey("s"))				Rig.AddForce(Cam.transform.forward.normalized*-120.0f);
		if (Input.GetKey("a")) 				Rig.AddForce(Cam.transform.right.normalized*-135.0f);
		if (Input.GetKey("d")) 				Rig.AddForce(Cam.transform.right.normalized*135.0f);	
		if (Input.GetKey(KeyCode.E)) 	Rig.AddRelativeForce(Cam.transform.up.normalized*130.0f);	
		if (Input.GetKey(KeyCode.Q)) 	Rig.AddRelativeForce(Cam.transform.up.normalized*-130.0f);	
		

		
		
				
			
	}
}

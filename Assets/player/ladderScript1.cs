using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderScript1 : MonoBehaviour 

{

	public Transform chController;
	bool inside = false;
	public float speedUpDown = 3.2f;
	public CharacterController FPSInput;

    public Collider Collider;
    public FPSController CharMovementScript;

void Start()
{
	FPSInput = GetComponent<CharacterController>();
	inside = false;
    print("script start");
    FPSInput.enabled = true;
    
}

void OnCollisionEnter(Collision col)
{
    if (col.gameObject.tag == "Ladder")
    {
        inside = true;
    }
}

void OnCollisionExit(Collision col)
{
    if (col.gameObject.tag == "Ladder")
    {
        inside = false;
    }
}

		
void Update()
{
	if(inside == true && Input.GetKey("w"))
	{
			chController.transform.position += Vector3.up / speedUpDown;
	}
	
	if(inside == true && Input.GetKey("s"))
	{
			chController.transform.position += Vector3.down / speedUpDown;
	}
}

}
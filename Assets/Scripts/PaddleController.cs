using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour
{
	public KeyCode key;
	private float initialVelocity;

	public AudioClip up;
	public AudioClip down;

	// Use this for initialization
	void Start()
	{
		initialVelocity = hingeJoint.motor.targetVelocity;
	}
	
	// Update is called once per frame
	void Update()
	{
		CheckPaddleInput();
	}

	void CheckPaddleInput()
	{
		// initialVelocity is "down" for the paddle
		if(Input.GetKeyDown(key))
		{
			// if "down", change to "up"
			if(hingeJoint.motor.targetVelocity == initialVelocity)
			{
				InvertMotorVelocity();
			}
			AudioSource.PlayClipAtPoint(up, new Vector3(-1.0f, 15.5f, -25.0f), 4.0f);
		}
		else if(Input.GetKeyUp(key))
		{
			// if "up", change to "down"
			if(hingeJoint.motor.targetVelocity != initialVelocity)
			{
				InvertMotorVelocity();
			}
			AudioSource.PlayClipAtPoint(down, new Vector3(-1.0f, 15.5f, -25.0f), 4.0f);
		}
	}

	void InvertMotorVelocity()
	{
		JointMotor motor = hingeJoint.motor;
		motor.targetVelocity *= -1;
		hingeJoint.motor = motor;
	}
}

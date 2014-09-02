using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour
{
	public KeyCode key;
	private float initialVelocity;

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
		}
		else if(Input.GetKeyUp(key))
		{
			// if "up", change to "down"
			if(hingeJoint.motor.targetVelocity != initialVelocity)
			{
				InvertMotorVelocity();
			}
		}
	}

	void InvertMotorVelocity()
	{
		JointMotor motor = hingeJoint.motor;
		motor.targetVelocity *= -1;
		hingeJoint.motor = motor;
	}
}

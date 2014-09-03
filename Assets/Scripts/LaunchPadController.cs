using UnityEngine;
using System.Collections;

public class LaunchPadController : MonoBehaviour
{
	public KeyCode key;
	public float maxDistance;
	public float translationConstant;
	public float forceConstant;
	public GameObject ball;

	private float translatedDistance;
	private SpringJoint spring;

	private enum LaunchPadState
	{
		MIN,
		CHARGING,
		MAX,
		RETURNING
	};
	private LaunchPadState state;

	void Start()
	{
		state = LaunchPadState.MIN;
		translatedDistance = 0.0f;
		spring = gameObject.GetComponent<SpringJoint>();
		SpringJoint.Destroy(gameObject.GetComponent<SpringJoint>());
	}

	void Update()
	{
		CheckLaunchPadInput();
	}

	void CheckLaunchPadInput()
	{
		if(Input.GetKey(key))
		{
			switch(state)
			{
				case LaunchPadState.MIN:
					ChargeLaunchPad();
					state = LaunchPadState.CHARGING;
					break;
				case LaunchPadState.CHARGING:
					ChargeLaunchPad();
					if(translatedDistance >= maxDistance)
					{
						state = LaunchPadState.MAX;
					}
					break;
				case LaunchPadState.MAX:
					// nothing
					break;
			}
		}
		else if(Input.GetKeyUp(key))
		{
			if(translatedDistance != 0.0f)
			{
				ball.rigidbody.AddForce(0, 0, forceConstant * translatedDistance);
				ReturnLaunchPad();
				state = LaunchPadState.RETURNING;
			}
		}
		else if(state == LaunchPadState.RETURNING)
		{
			ReturnLaunchPad();
			if(translatedDistance == 0.0f)
			{
				state = LaunchPadState.MIN;
			}
		}
	}

	void ChargeLaunchPad()
	{
		float dist = translationConstant * Time.deltaTime;
		translatedDistance += dist;
		if(translatedDistance > maxDistance)
		{
			dist -= translatedDistance - maxDistance;
			translatedDistance = maxDistance;
		}
		transform.Translate(0, 0, dist * -1);
	}

	void ReturnLaunchPad()
	{
		float returnDist = ball.rigidbody.transform.position.z - transform.position.z - 1;
		translatedDistance -= returnDist;
		if(translatedDistance < 0.0f)
		{
			returnDist += translatedDistance;
			translatedDistance = 0.0f;
		}
		transform.Translate(0, 0, returnDist);
	}
}

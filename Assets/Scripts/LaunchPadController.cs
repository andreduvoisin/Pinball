using UnityEngine;
using System.Collections;

public class LaunchPadController : MonoBehaviour
{
	public KeyCode key;
	public float maxDistance;
	public float translationConstant;
	public float forceConstant;
	public GameObject ball;

	public bool isReverseLaunchPad;

	private float translatedDistance;

	public AudioClip plungerAudio;

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
				AudioSource.PlayClipAtPoint(plungerAudio, new Vector3(-1.0f, 15.5f, -25.0f), 4.0f);
				if(Mathf.Round(transform.position.x) == Mathf.Round(ball.transform.position.x)
				   && Mathf.Abs(transform.position.z - ball.transform.position.z) <= maxDistance + 1)
				{
					if(!isReverseLaunchPad)
					{
						ball.rigidbody.AddForce(0, 0, forceConstant * translatedDistance);
					}
					else
					{
						ball.rigidbody.AddForce(0, 0, forceConstant * translatedDistance * -1);
					}
				}
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
		float returnDist;
		if(Mathf.Round(transform.position.x) == Mathf.Round(ball.transform.position.x))
		{
			returnDist = Mathf.Abs(ball.transform.position.z - transform.position.z) - 1.0f;
		}
		else
		{
			returnDist = 1.0f;
		}

		translatedDistance -= returnDist;
		if(translatedDistance < 0.0f)
		{
			returnDist += translatedDistance;
			translatedDistance = 0.0f;
		}
		transform.Translate(0, 0, returnDist);
	}
}

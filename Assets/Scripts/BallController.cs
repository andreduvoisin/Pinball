using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	private Vector3 startPos;

	// Use this for initialization
	void Start()
	{
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "FrontTrigger")
		{
			ResetBall();
		}
	}

	void ResetBall()
	{
		transform.position = startPos;
		transform.rigidbody.velocity = Vector3.zero;
		transform.rigidbody.angularVelocity = Vector3.zero;
	}
}

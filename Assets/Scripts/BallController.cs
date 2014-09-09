using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	public GameObject projectile;
	public float projectileSpeed;

	public Material blueMaterial;
	public Material greenMaterial;
	public Material orangeMaterial;
	public Material redMaterial;

	public bool isReverseBall;

	private Vector3 startPos;
	private GameController gameController;

	//private GameController.CurrentColor myColor;

	public AudioClip collisionSound;

	private bool isDone;

	// Use this for initialization
	void Start()
	{
		//myColor = GameController.CurrentColor.NONE;

		isDone = false;

		startPos = transform.position;

		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		ProcessInput();
	}

	void FixedUpdate()
	{
		if(isReverseBall)
		{
			rigidbody.AddForce(Physics.gravity * -1 * rigidbody.mass);
		}
	}

	void ProcessInput()
	{
		if(Input.GetKeyDown(KeyCode.X))
		{
			GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
			proj.rigidbody.AddForce(new Vector3(projectileSpeed * -1, 0, 0));
		}
		else if(Input.GetKeyDown(KeyCode.Period))
		{
			GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
			proj.rigidbody.AddForce(new Vector3(projectileSpeed, 0, 0));
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "BreakoutCube")
		{
			//BreakoutCubeController cubeScript = other.gameObject.GetComponent<BreakoutCubeController>();
			//if(myColor == cubeScript.GetMyColor())
			//{
				gameController.AddScore(500);
				other.gameObject.SetActive(false);
				gameController.ReportBlock(other.gameObject);
			AudioSource.PlayClipAtPoint(collisionSound, new Vector3(-1.0f, 15.5f, -25.0f), 0.75f);
			//}
		}
		/*
		else if(other.gameObject.tag == "BreakoutPill")
		{
			PillController pillScript = other.gameObject.GetComponent<PillController>();
			myColor = pillScript.GetMyColor();
			switch(myColor)
			{
			case GameController.CurrentColor.BLUE:
				renderer.material = blueMaterial;
				break;
			case GameController.CurrentColor.GREEN:
				renderer.material = greenMaterial;
				break;
			case GameController.CurrentColor.ORANGE:
				renderer.material = orangeMaterial;
				break;
			case GameController.CurrentColor.RED:
				renderer.material = redMaterial;
				break;
			}
		}
		*/
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "FrontTrigger")
		{
			ResetBall();
		}
	}

	public bool GetIsDone()
	{
		return isDone;
	}

	public void ResetBall()
	{
		if(gameController.GetBalls() > 0)
		{
			isDone = false;
			transform.rigidbody.velocity = Vector3.zero;
			transform.rigidbody.angularVelocity = Vector3.zero;
			transform.position = startPos;
		}
		else
		{
			isDone = true;
			transform.rigidbody.velocity = Vector3.zero;
			transform.rigidbody.angularVelocity = Vector3.zero;
			transform.position = new Vector3(100, 100, 100);
		}

		gameController.SubtractBalls(1);
	}
}

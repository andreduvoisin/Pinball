using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public float transformConstant;
	private float transformSpeed;
	private GameController gameController;

	void Start()
	{
		transformSpeed = transformConstant;

		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	void Update()
	{
		MoveEnemy();
	}

	void MoveEnemy()
	{
		transform.Translate(new Vector3(0, 0, transformSpeed * -1 * Time.deltaTime));
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name == "Ball")
		{
			gameController.AddScore(500);
			DestroyObject(gameObject);
		}
		else if(other.gameObject.tag == "Projectile")
		{
			gameController.AddScore(100);
			DestroyObject(gameObject);
		}
	}
}

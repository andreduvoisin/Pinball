using UnityEngine;
using System.Collections;

public class BumperController : MonoBehaviour
{
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	void Update()
	{

	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name == "Ball")
		{
			gameController.AddScore(scoreValue);
		}
	}
}

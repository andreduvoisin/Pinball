using UnityEngine;
using System.Collections;

public class BreakoutCubeController : MonoBehaviour
{
	private GameController.CurrentColor myColor;
	private GameController gameController;

	void Start()
	{
		if(renderer.material.name.Contains("GreenMaterial"))
		{
			myColor = GameController.CurrentColor.GREEN;
		}
		else if(renderer.material.name.Contains("RedMaterial"))
		{
			myColor = GameController.CurrentColor.RED;
		}
		else if(renderer.material.name.Contains("OrangeMaterial"))
		{
			myColor = GameController.CurrentColor.ORANGE;
		}
		else if(renderer.material.name.Contains("BlueMaterial"))
		{
			myColor = GameController.CurrentColor.BLUE;
		}

		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	void Update()
	{

	}

	public GameController.CurrentColor GetMyColor()
	{
		return myColor;
	}

	void OnCollisionEnter(Collision other)
	{

	}
}

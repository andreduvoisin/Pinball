using UnityEngine;
using System.Collections;

public class CircleController : MonoBehaviour
{
	public GameController.CurrentColor color;
	public GUIText letterText;
	private GameController gameController;
	private KeyCode myKey;

	void Start()
	{
		myKey = KeyCode.A + Random.Range(0, 25);
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}
	 
	void Update()
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(gameController.GetCurrentColor() == color)
		{
			letterText.guiText.enabled = true;
			letterText.text = "" + (char)('A' + (myKey - KeyCode.A));
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(gameController.GetCurrentColor() == color)
		{
			letterText.guiText.enabled = false;
		}
	}
}

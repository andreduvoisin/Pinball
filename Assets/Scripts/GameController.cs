using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public int score = 0;
	public int balls = 3;
	public GUIText scoreText;
	public GUIText ballsText;
	public GUIText gravityFlipTimerText;
	public GUIText winText;
	public KeyCode colorChangeKey;
	public GameObject enemyCapsule;
	
	public int spawnInterval;
	public int reduceSpawnInterval;
	private int nextSpawnTime;

	public GameObject ball;

	public float tiltAmount;

	public enum CurrentColor
	{
		NONE,
		RED,
		GREEN,
		BLUE,
		ORANGE
	}
	private CurrentColor currentColor;

	void Start()
	{
		currentColor = CurrentColor.ORANGE;
		nextSpawnTime = 0;

		if(Physics.gravity.z > 0)
		{
			Vector3 gravity = Physics.gravity;
			gravity.z *= -1;
			Physics.gravity = gravity;
		}
	}

	void Update()
	{
		ProcessInput();
		UpdateScore();
		UpdateBalls();
		UpdateCurrentColor();
		/*
		if(Mathf.RoundToInt(Time.time) == nextSpawnTime)
		{
			nextSpawnTime += spawnInterval;
			// Random enemy location.
			// x = -9 to 6.75
			// y = 1
			// z = 19
			Vector3 pos = new Vector3(Random.Range(-9f, 6.75f), 1, 19);
			Instantiate(enemyCapsule, pos, Quaternion.identity);
		}
		if(Mathf.RoundToInt(Time.time) % reduceSpawnInterval == 0)
		{
			if(spawnInterval > 1)
			{
				--spawnInterval;
			}
		}
		*/
	}

	void OnGUI()
	{
		Rect buttonRect = new Rect(Screen.width - 110, 10, 100, 20);
		if(GUI.Button(buttonRect, "Reset Game"))
		{
			Application.LoadLevel("MainGame");
		}
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	void UpdateBalls()
	{
		ballsText.text = "Balls: " + balls;
		if(balls <= 0)
		{
			winText.guiText.enabled = true;
		}
	}

	void UpdateCurrentColor()
	{
		/*
		switch(currentColor)
		{
		case CurrentColor.RED:
			currentColorText.text = "RED";
			break;
		case CurrentColor.GREEN:
			currentColorText.text = "GREEN";
			break;
		case CurrentColor.BLUE:
			currentColorText.text = "BLUE";
			break;
		case CurrentColor.ORANGE:
			currentColorText.text = "ORANGE";
			break;
		}
		*/
	}

	void ProcessInput()
	{
		if(Input.GetKeyDown(colorChangeKey))
		{
			/*
			switch(currentColor)
			{
			case CurrentColor.RED:
				currentColor = CurrentColor.GREEN;
				break;
			case CurrentColor.GREEN:
				currentColor = CurrentColor.BLUE;
				break;
			case CurrentColor.BLUE:
				currentColor = CurrentColor.ORANGE;
				break;
			case CurrentColor.ORANGE:
				currentColor = CurrentColor.RED;
				break;
			}
			*/
			Vector3 gravity = Physics.gravity;
			gravity.z *= -1;
			Physics.gravity = gravity;
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			ball.rigidbody.AddForce(new Vector3(tiltAmount * -1, 0, 0));
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			ball.rigidbody.AddForce(new Vector3(tiltAmount, 0, 0));
		}
	}

	public CurrentColor GetCurrentColor()
	{
		return currentColor;
	}

	public void AddScore(int num)
	{
		score += num;
	}
	public void SubtractScore(int num)
	{
		score -= num;
	}

	public void AddBalls(int num)
	{
		balls += num;
	}
	public void SubtractBalls(int num)
	{
		balls -= num;
	}

	public int GetBalls()
	{
		return balls;
	}
}

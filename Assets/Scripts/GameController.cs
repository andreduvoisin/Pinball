using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public int score = 0;
	public int balls = 6;
	public GUIText scoreText;
	public GUIText ballsText;
	public GUIText gravityFlipTimerText;
	public GUIText winText;
	public KeyCode colorChangeKey;

	public GameObject ball;
	public GameObject reverseBall;

	public float tiltAmount;

	private Queue blocks;

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
		blocks = new Queue();

		currentColor = CurrentColor.ORANGE;

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

	void ProcessInput()
	{
		/*if(Input.GetKeyDown(colorChangeKey))
		{
			Vector3 gravity = Physics.gravity;
			gravity.z *= -1;
			Physics.gravity = gravity;
		}
		else*/
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			ball.rigidbody.AddForce(new Vector3(tiltAmount * -1, 0, 0));
			reverseBall.rigidbody.AddForce(new Vector3(tiltAmount * -1, 0, 0));
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			ball.rigidbody.AddForce(new Vector3(tiltAmount, 0, 0));
			reverseBall.rigidbody.AddForce(new Vector3(tiltAmount, 0, 0));
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

	public void ReportBlock(GameObject other)
	{
		blocks.Enqueue(other.gameObject);
		if(blocks.Count >= 27)
		{
			while(blocks.Count > 0)
			{
				GameObject block = blocks.Dequeue() as GameObject;
				block.SetActive(true);
			}
			balls += 2;
			score += 10000;
		}
	}
}

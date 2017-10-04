using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private readonly float GAME_SPEED = 1.2f;
	private readonly int STARTING_LIFE = 3;

	[SerializeField]
	private Text scoreBoard;
	[SerializeField]
	private  Text lifePanel;
	[SerializeField]
	private  FillBar fillBar;

	private  int score;
	private  int scoreBoardScore;

	private  int life;

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3 (0, -50f, 0);
		Time.timeScale = GAME_SPEED;
		life = STARTING_LIFE;
		updateLifePanel ();
	}
	
	// Update is called once per frame
	float nextIncTime;
	float interval = 0.1f;
	void Update () {
		if (scoreBoardScore < score && Time.time >= nextIncTime) {
			scoreBoardScore += Mathf.Max(1, (score - scoreBoardScore) / 10);
			nextIncTime = Time.time + interval;
		}
		scoreBoard.text = scoreBoardScore.ToString();
	}

	public void scoreUp(int amount) {
		score += amount;	
	}

	public void checkVital() {
		if (life > 0) {
			gameOver ();
		}
	}

	public void decreaseLife() {
		life--;
		updateLifePanel ();
	}

	public void increaseLife() {
		life++;
		updateLifePanel ();
	}

	private void updateLifePanel() {
		lifePanel.text = life.ToString ();
		resetTimer ();
	}

	public void resetTimer() {
		fillBar.resetTimer ();
	}

	public void gameOver() {
		Debug.Log ("Game Over");
	}
}

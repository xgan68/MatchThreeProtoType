using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private readonly float GAME_SPEED = 1.2f;
	private readonly int STARTING_LIFE = 3;
	private readonly float GRAVITY = -50.0f;

	[SerializeField]
	private Text scoreBoard;
	[SerializeField]
	private Text lifePanel;
	[SerializeField]
	private FillBar fillBar;
	[SerializeField]
	private Canvas redCanvas;
	[SerializeField]
	private Canvas gameOverCanvas;
	[SerializeField]
	private Text finalScore;
	[SerializeField]
	private GameObject explosion;

	[SerializeField]
	public bool CRAZY_MODE_ON;

	private int difficuty;

	AudioController audioController;

	private  int score;
	private  int scoreBoardScore;

	private  int life;

	// Use this for initialization
	void Start () {
		audioController = GameObject.Find ("AudioController").GetComponent<AudioController> ();
		Physics.gravity = new Vector3 (0, GRAVITY, 0);
		Time.timeScale = GAME_SPEED;
		life = STARTING_LIFE;
		updateLifePanel ();
		gameOverCanvas.enabled = false;
		difficuty = 4;
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
		difficuty = 4 + Mathf.Min (2, score / 500);
	}

	public void checkVital() {
		if (life <= 0) {
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
		setBloodEffect (false);
	}

	public void resetTimer() {
		fillBar.resetTimer ();
	}

	public void gameOver() {
		//Debug.Log ("GameOVer");
		//returnToMainMenu ();
		BoardManager.destroyAll();
		setBloodEffect (true);
		gameOverCanvas.enabled = true;
		finalScore.text = score.ToString ();
		fillBar.coolingDown = false;
		gameOverExplosion ();
		audioController.playClockTicking (false);
	}

	public void returnToMainMenu() {
		SceneManager.LoadScene ("Main_Menu");
	}

	private void setBloodEffect(bool forceOff) {
		if (forceOff) {
			redCanvas.enabled = false;
			return;
		}
		if (life <= 1) {
			redCanvas.enabled = true;
		} else {
			redCanvas.enabled = false;
		}
	}

	private void gameOverExplosion() {
		for (int i = 0; i < explosion.transform.childCount; i++) {
			explosion.transform.GetChild(i).gameObject.GetComponent<ParticleSystem>().Emit (10);
		}
	}

	public int getDifficuty() {
		return difficuty;
	}

	public void setDifficuty(int d) {
		difficuty = d;
	}
}

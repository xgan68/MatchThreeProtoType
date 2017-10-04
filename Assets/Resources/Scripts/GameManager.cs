using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private Text scoreBoard;

	private static int score;
	private static int scoreBoardScore;
	// Use this for initialization
	void Start () {
		
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

	public static void scoreUp(int amount) {
		score += amount;	
	}
}

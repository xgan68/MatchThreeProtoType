using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private Text scoreBoard;

	private static int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreBoard.text = score.ToString();
	}

	public static void scoreUp(int amount) {
		score += amount;	
	}
}

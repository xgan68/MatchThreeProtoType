using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class FillBar : MonoBehaviour
{
	GameManager gameManager;

	public Image cooldown;
	private bool coolingDown;
	private float waitTime = 30f;
	private AudioController audioController; 

	void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		audioController = GameObject.Find ("AudioController").GetComponent<AudioController> ();
		coolingDown = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (coolingDown == true)
		{
			//Reduce fill amount over 30 seconds
			cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
			if (cooldown.fillAmount <= 0f) {
				gameManager.gameOver ();
			}

			//Clock ticking trigger
			if (cooldown.fillAmount > 0 && cooldown.fillAmount < 7.0f / waitTime) { 	
				audioController.playClockTicking (true);
			} else {
				audioController.playClockTicking (false);
			}
		}
	}

	public void resetTimer() {
		cooldown.fillAmount = 1.0f;
	}
}
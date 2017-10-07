using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class FillBar : MonoBehaviour
{
	GameManager gameManager;

	public Image cooldown;
	public bool coolingDown;
	private float startWaitTime = 30f;
	private float waitTime;
	private AudioController audioController; 

	void Start() {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		audioController = GameObject.Find ("AudioController").GetComponent<AudioController> ();
		coolingDown = true;
		waitTime = startWaitTime;
	}

	// Update is called once per frame
	void Update()
	{
		if (coolingDown == true && !gameManager.JUST_FOR_FUN)
		{
			//Reduce fill amount over 30 seconds
			cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
			if (cooldown.fillAmount <= 0f) {
				gameManager.decreaseLife ();
				gameManager.checkVital ();
				resetTimer ();
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
		waitTime = gameManager.timerWaitTime();
	}
}
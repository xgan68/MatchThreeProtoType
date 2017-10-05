using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	static private readonly int COMBO_TO_INC_LIFE = 3;
	private readonly int BOARDWIDTH = 5;
	private readonly int BOARDHEIGHT = 8;
	private static readonly float swapSpeed = 7.0f;

	public static BoardManager instance;

	enum PlayerStates {FirstGemSelected, None, Swapping, CheckMatch};
	static PlayerStates currentState;

	static Gem gem1, gem2;

	static Vector3 target1, target2;

	private Gem startingGem;
	private int dropHeight = 10;

	static List<Gem> gems = new List<Gem> ();

	static AudioController audioController;

	private static GameManager gameManager;

	private static int comboCount;
	// Use this for initialization
	void Start () {
		instance = this;
		onNewGameStart ();
		currentState = PlayerStates.CheckMatch;
		audioController = GameObject.Find ("AudioController").GetComponent<AudioController> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		comboCount = 0;
		//checkForMatch ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		if (currentState == PlayerStates.Swapping) {
			if (gem1 != null && gem2 != null 
				&& gem1.onPosition && gem2.onPosition) {
				currentState = PlayerStates.CheckMatch;
			}
			if (gem1 == null && gem2 == null && isBoardStable()) {
				currentState = PlayerStates.CheckMatch;

			}

		} else if (isBoardStable() && currentState == PlayerStates.CheckMatch) {
			currentState = PlayerStates.Swapping;
			checkForMatch ();

		} 

	}

	void checkForMatch() {
		if (gem1 && gem2)
			resetGems ();
		if (foundMatch ()) {
			destroyMatchGems ();
			if (comboCount >= COMBO_TO_INC_LIFE) {
				gameManager.increaseLife ();
			}
			gameManager.resetTimer ();
		} else {
			currentState = PlayerStates.None;
			comboCount = 0;
			gameManager.checkVital ();
		}

	}


	bool isBoardStable() {
		foreach (Gem gem in gems) {
			//Debug.Log (gem.GetComponent<Rigidbody>().velocity.y);
			if (gem == null) {
				continue;
			}
			if (Mathf.Abs(gem.GetComponent<Rigidbody>().velocity.y) > 0.4f) {
				return false;
			}
		}
		return true;
	}



	void destroyMatchGems() {
		List<Gem> destroyGroup = new List<Gem>();
		for (int i = 0; i < gems.Count; i++) {
			if (gems[i].isMatched) {
				destroyGroup.Add(gems[i]);
			}
		}

		if (destroyGroup.Count > 0) { 			//has matched one or more
			if (comboCount >= 2) {
				audioController.onCubeCombo ((float)comboCount/8);
			} else {
				audioController.onCubeExplode((float)comboCount/8);
			}
			comboCount++;
		}
		foreach (Gem gem in destroyGroup) {
			gem.explode ();
			gem.generateGem ();
			gem.transform.position = new Vector3 (
				gem.transform.position.x,
				gem.transform.position.y + dropHeight,
				gem.transform.position.z);
			gameManager.scoreUp (1 * comboCount);
		}
	}

	bool foundMatch() {
		bool result = false;
		foreach (Gem gem in gems) {
			if (gem.getNeighbor(0) != null && gem.getNeighbor(1) != null
				&& gem.getNeighbor(0).getColor() == gem.getNeighbor(1).getColor() 
				&& gem.getNeighbor(0).getColor() == gem.getColor()) {
				gem.getNeighbor (0).isMatched = true;
				gem.getNeighbor (1).isMatched = true;
				gem.isMatched = true;
				result = true;
			}
			if (gem.getNeighbor(2) != null && gem.getNeighbor(3) != null
				&& gem.getNeighbor(2).getColor() == gem.getNeighbor(3).getColor() 
				&& gem.getNeighbor(2).getColor() == gem.getColor()) {
				gem.getNeighbor (2).isMatched = true;
				gem.getNeighbor (3).isMatched = true;
				gem.isMatched = true;
				result = true;
			}
		}
		return result;
	}

	float startTime;
	void moveGem(Gem gem, Vector3 start, Vector3 target) {
		Vector3 center = (start + target) * 0.5f;
		center -= new Vector3 (0, 1, 0);
		Vector3 riseRel = start - center;
		Vector3 setRel = target - center;
		float frac = (Time.time - startTime) / 1f;
		gem.transform.position = Vector3.Slerp (riseRel, setRel, frac);
		gem.transform.position += center;
	}

	static void togglePhysics(bool enable) {
		gem1.gameObject.GetComponent<Rigidbody> ().isKinematic = enable;
		gem2.gameObject.GetComponent<Rigidbody> ().isKinematic = enable;
	}

	void onNewGameStart() {
		generateGems(BOARDWIDTH, BOARDHEIGHT);
	}


	void generateGems(int width, int height) {
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				GameObject gemGameObject = Instantiate((GameObject)Resources.Load("Prefabs/Gem"), 
														new Vector3(x - width/2 ,y , 0), 
														Quaternion.identity)as GameObject;
				gems.Add (gemGameObject.GetComponent<Gem>());
			}
		}
	}

	public static void onGemSelected(Gem gem) {
		switch (currentState) {
		case PlayerStates.None:
			toggleRotate (gem);
			gem1 = gem;
			currentState = PlayerStates.FirstGemSelected;
			break;

		case PlayerStates.FirstGemSelected:
			toggleRotate (gem1);

			if (!gem1.containsNeighbor (gem) && !gameManager.CRAZY_MODE_ON) {
				currentState = PlayerStates.None;
				gem1 = null;
				audioController.onInvailedMove ();
			} else if (gem1 != gem) {
				gem2 = gem;
				currentState = PlayerStates.Swapping;
				target2 = new Vector3(Mathf.Round(gem1.transform.position.x), 
									Mathf.Round(gem1.transform.position.y), 0);
				target1 = new Vector3(Mathf.Round(gem2.transform.position.x), 
									Mathf.Round(gem2.transform.position.y), 0);
				swapGems(gem1, gem2);
				gameManager.decreaseLife ();
			} else {
				currentState = PlayerStates.None;
				gem1 = null;
			}


			break;

		case PlayerStates.Swapping:
			Debug.Log ("Swapping");
			break;

		case PlayerStates.CheckMatch:
			Debug.Log ("Swapping");
			break;
		}
	}

	static void toggleRotate(Gem gem) {
		Rotate rotate = gem.transform.Find("Cube").GetComponent<Rotate> ();
		if (rotate.isActiveAndEnabled) {
			rotate.enabled = false;
		} else {
			rotate.enabled = true;
		}
	}

	static void swapGems(Gem gem1, Gem gem2) {
		togglePhysics (true);

		instance.StartCoroutine(moveGem(gem1, target1));
		instance.StartCoroutine(moveGem(gem2, target2));

	}

	void resetGems() {
		
		togglePhysics (false);
		gem1.onPosition = false;
		gem2.onPosition = false;
		gem1 = null;
		gem2 = null;
	}


	static IEnumerator moveGem(Gem gem, Vector3 target) {
		float remainingDistance = Vector3.Distance (gem.transform.position, target);

		while (remainingDistance > 0.01f) {
			gem.transform.position = Vector3.Lerp (gem.transform.position, target, Time.deltaTime * swapSpeed);
			remainingDistance = Vector3.Distance (gem.transform.position, target);

			yield return null;
		}
		gem.onPosition = true;;
	}

	void OnTriggerEnter(Collider other) 
	{
		if(other.tag =="Gem")
		{
			startingGem = other.gameObject.GetComponent<Gem>();
		}
	}

	public static void destroyAll() {
		foreach (Gem gem in gems) {
			if (gem != null) {
				gem.gameObject.SetActive (false);
			}
		}
		gems.Clear ();
	}
}

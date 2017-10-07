using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	enum MenuItems {Start, CrazyMode, Leave, Back};

	[SerializeField]
	private MenuItems item;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		switch (item) {
		case MenuItems.Start:
			SceneManager.LoadScene ("Level1");
			break;
		case MenuItems.CrazyMode:
			SceneManager.LoadScene ("Just_For_Fun");
			break;
		case MenuItems.Leave:
			Application.Quit ();
			break;
		case MenuItems.Back:
			SceneManager.LoadScene ("Main_menu");
			break;
		}
	}
}

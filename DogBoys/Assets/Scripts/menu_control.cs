using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_control : MonoBehaviour {

	public void loadMission() {
		SceneManager.LoadScene ("TechDemo-Tom");
	}

	public void exit() {
		Application.Quit ();
	}
		
}

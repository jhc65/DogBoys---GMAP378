using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_control : MonoBehaviour {

	public void loadMission() {
		SceneManager.LoadScene ("Level01");
	}

	public void exit() {
		Application.Quit ();
	}
		
}

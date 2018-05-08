using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants_Values : MonoBehaviour {

	#region dog lists
	private string[] _availableDogs = {"test", "test2"};
	private string[] _redDogs = {"red test"};
	private string[] _blueDogs = {"blue test"};

	public string[] getAvailable(){
		return _availableDogs;
	}
	public string[] getBlue(){
		return _blueDogs;
	}
	public string[] getRed(){
		return _redDogs;
	}
	#endregion


}

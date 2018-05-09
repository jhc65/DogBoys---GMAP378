using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants_Values : MonoBehaviour {
	
	#region dog lists and functions
	private List<Character> _availableDogs = new List<Character>();
	private List<Character> _redDogs = new List<Character>();
	private List<Character> _blueDogs = new List<Character>();

	public List<Character> getAvailable(){
		return _availableDogs;
	}
	public List<Character> getBlue(){
		return _blueDogs;
	}
	public List<Character> getRed(){
		return _redDogs;
	}

	public void moveChar(Character n, List<Character> from, List<Character> to){
		if(from.Contains(n))
			from.Remove(n);
		Debug.Log ("removed");

		to.Add (n);
		Debug.Log ("added");
	}

	void Start(){
		//test cases
		_availableDogs.Add(new Character());
		_redDogs.Add(new Character());
		_blueDogs.Add(new Character());
		Debug.Log (_availableDogs + "-" + _blueDogs + "-" + _redDogs);
	}
	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teamBuilder : MonoBehaviour {
	[SerializeField]
	private GameObject revolverDog;
	[SerializeField]
	private GameObject rifleDog;
	[SerializeField]
	private GameObject shotgunDog;
	[SerializeField]
	private GameObject fromDraft;
	[SerializeField]
	private List<Character> redTeam = new List<Character>();
	[SerializeField]
	private List<Character> blueTeam = new List<Character>();

	// Use this for initialization
	void Start () {
		//get object with team comp
		fromDraft = GameObject.Find("Game_Constants");
		//get the teams
		redTeam = fromDraft.GetComponent<Constants_Values>().getRed();
		blueTeam = fromDraft.GetComponent<Constants_Values> ().getBlue();
		//spawn teams
		deployRed();
		deployBlue ();
	}
	
	void deployRed(){
		foreach (Character n in redTeam) {
			//Instantiate (prefab1,new Vector3(3,4,3),Quaternion.identity);
		}
	}

	void deployBlue(){
		foreach (Character n in blueTeam) {
			//Instantiate (prefab2,new Vector3(7,4,3),Quaternion.identity);
		}
	}

	void createRevolver(Vector3 location){
		GameObject newDog = Instantiate (revolverDog,location,Quaternion.identity);

	}
}

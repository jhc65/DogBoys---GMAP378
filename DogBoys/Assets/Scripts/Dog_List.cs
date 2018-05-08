using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_List : MonoBehaviour {
	public GameObject constants_values;
	public GameObject dog_listable;
	private string[] available;
	private string[] blue;
	private string[] red;
	private GameObject listAvailable;
	private GameObject listBlue;
	private GameObject listRed;

	// Use this for initialization
	void Awake () {
		//get dog lists
		available = constants_values.GetComponent<Constants_Values>().getAvailable ();
		blue = constants_values.GetComponent<Constants_Values>().getBlue ();
		red = constants_values.GetComponent<Constants_Values>().getRed ();
		//get list panels
		listAvailable = GameObject.Find("Dog_Available");
		listBlue= GameObject.Find("Blue_Drafted");
		listRed= GameObject.Find("Red_Drafted");
		//populate panels
		populate (available, listAvailable);
		populate (blue, listBlue);
		populate (red, listRed);

	}
	

	void populate(string[] list, GameObject panel){
		//Debug.Log ("fill" + panel.name);
		foreach (string s in list){
			GameObject newDog = Instantiate (dog_listable,transform.position,Quaternion.identity);
			newDog.transform.parent = panel.transform;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}

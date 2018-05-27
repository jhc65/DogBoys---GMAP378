using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dog_Listable : MonoBehaviour {
	//constants_values object
	public GameObject constants_values;
	//buttons and text
	public Button left;
	public Button right;
	public Text text;
	//panels
	private GameObject listAvailable;
	private GameObject listBlue;
	private GameObject listRed;
	//character this object represents
	[SerializeField]
	private string thisDog;

	public void setType(string s){
		thisDog = s;
		Debug.Log (thisDog);
	}

	void Start () {
		constants_values = GameObject.Find ("Game_Constants");
		Button lButton = left.GetComponent<Button> ();
		Button rButton = right.GetComponent<Button>();
		Text display = text.GetComponent<Text> ();

		lButton.onClick.AddListener (MoveLeft);
		rButton.onClick.AddListener (MoveRight);
		//get panels
		listAvailable = GameObject.Find("Dog_Available");
		listBlue= GameObject.Find("Blue_Drafted");
		listRed= GameObject.Find("Red_Drafted");
	}

	void MoveLeft(){
		if (transform.parent.name == "Red_Drafted"){
			this.transform.parent = listAvailable.transform;
			constants_values.GetComponent<Constants_Values> ()
				.moveChar (thisDog, constants_values.GetComponent<Constants_Values>().getRed(), constants_values.GetComponent<Constants_Values>().getAvailable());
		}
		else if(transform.parent.name == "Dog_Available"){
			this.transform.parent = listBlue.transform;
			constants_values.GetComponent<Constants_Values> ()
				.moveChar (thisDog, constants_values.GetComponent<Constants_Values>().getAvailable(),  constants_values.GetComponent<Constants_Values>().getBlue());
		}
	}

	void MoveRight(){
		if (transform.parent.name == "Blue_Drafted"){
			this.transform.parent = listAvailable.transform;
			constants_values.GetComponent<Constants_Values> ()
				.moveChar (thisDog, constants_values.GetComponent<Constants_Values>().getBlue(), constants_values.GetComponent<Constants_Values>().getAvailable());
		}
		else if(transform.parent.name == "Dog_Available"){
			this.transform.parent = listRed.transform;
			constants_values.GetComponent<Constants_Values> ()
				.moveChar (thisDog, constants_values.GetComponent<Constants_Values>().getAvailable(),  constants_values.GetComponent<Constants_Values>().getRed());
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dog_Listable : MonoBehaviour {
	//constants_values
	public GameObject constants_values;
	//buttons and text
	public Button left;
	public Button right;
	public Text text;
	//panels
	private GameObject listAvailable;
	private GameObject listBlue;
	private GameObject listRed;


	void Start () {
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
			//script to update the lists in constants
		}
		else if(transform.parent.name == "Dog_Available"){
			this.transform.parent = listBlue.transform;
			//script to update the lists in constants
		}
	}

	void MoveRight(){
		if (transform.parent.name == "Blue_Drafted"){
			this.transform.parent = listAvailable.transform;
			//script to update the lists in constants
		}
		else if(transform.parent.name == "Dog_Available"){
			this.transform.parent = listRed.transform;
			//script to update the lists in constants
		}
	}

}

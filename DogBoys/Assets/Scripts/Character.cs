using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public int health;
	public string status;
	public string weapon; //TODO: weapon class
	private bool moved;
	private int xPos, yPos;

	// Use this for initialization
	void Start () {
		moved = false;
		health = 100;
		status = "";
		weapon = "";
	}

	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Die ();
		}
	}

	void Die(){
		Destroy (gameObject);
	}


	void Move(int x, int y){
		xPos = x;
		yPos = y;
	}
			
}

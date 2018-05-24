using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_mouse : MonoBehaviour {
	[SerializeField]
	private GameObject tileHighlight;
	[SerializeField]
	private GameObject nullHighlight;

	// Keep track of where the player clicks in the game world
	private RaycastHit hit;
	private bool highlighted;
	private Transform currentTile;
	private GameObject currentHighlight;
	private GameController gc;	//game controller

	void Start() {
		highlighted = false;
		gc = GameController.Instance;
	}

	// Update is called once per frame
	void  FixedUpdate () {
		// Shoot a raycast from the camera to where player is pointing
		Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		// If that raycast hit something...
		if (Physics.Raycast (tempRay, out hit, 10000)) {
			// Draw a line in the game that follows the ray
			Debug.DrawLine (tempRay.origin, hit.point, Color.cyan);
			//if you hit a tile
			if (hit.transform.gameObject.tag == "Tile" || hit.transform.gameObject.tag == "FullCover" || hit.transform.gameObject.tag == "NoHitCover") {
				currentTile = hit.transform.gameObject.transform;
				//current tile not highlighted
				if (highlighted == false) {
					currentTile = hit.transform.gameObject.transform;
					currentHighlight = Instantiate (tileHighlight, currentTile);
					highlighted = true;
				//current tile is highlighted
				} else {
					currentHighlight.transform.position = currentTile.position;
				}
				//-------------------------------------------------------------
				//input bs
				if (Input.GetMouseButtonDown(0)) {					//as of now, all this does is move, but other functionality should be easy to implement
                    if (hit.transform.gameObject.tag == "FullCover") {
                        gc.MoveSelectedCharacter(currentTile.position, true);
                    }
                    else if (hit.transform.gameObject.tag == "NoHitCover") {
                        gc.MoveSelectedCharacter_NoHit(currentTile.position);
                    }
                    else {
                        gc.MoveSelectedCharacter(currentTile.position, false);
                    }
				}
			}
		} else {
			if (currentHighlight != null) {
				currentHighlight.transform.position = new Vector3(currentHighlight.transform.position.x,-10,currentHighlight.transform.position.z);
			}
		}
	}
		
}

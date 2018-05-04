using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_tiles : MonoBehaviour {

	public GameObject tileHighlight;

	// Keep track of where the player clicks in the game world
	private RaycastHit hit;
	private bool highlighted;
	private Transform highlightHere;
	private GameObject currentHighlight;

	void Start() {
		highlighted = false;
	}

	// Update is called once per frame
	void Update () {
		// Shoot a raycast from the camera to where player is pointing
		Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		// If that raycast hit something...
		if (Physics.Raycast (tempRay, out hit, 10000)) {
			// Draw a line in the game that follows the ray
			Debug.DrawLine (tempRay.origin, hit.point, Color.cyan);
			if (hit.transform.gameObject.tag == "Tile") {
				highlightHere = hit.transform.gameObject.transform;
				if (highlighted == false) {
					highlightHere = hit.transform.gameObject.transform;
					currentHighlight = Instantiate (tileHighlight, highlightHere);
					highlighted = true;
				} else {
					currentHighlight.transform.position = highlightHere.position;
				}
			}
		} else {
			if (currentHighlight != null) {
				currentHighlight.transform.position = new Vector3(currentHighlight.transform.position.x,-10,currentHighlight.transform.position.z);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile_property : MonoBehaviour {

	public GameObject mouseover_effect;
	private Vector3 my_position;
	private bool effect_status;
	private GameObject my_effect;
	// Use this for initialization
	void Start () {
		my_position = this.gameObject.transform.position;
		effect_status = false;
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

	void OnMouseOver(){
		if (effect_status == false) {
			//Debug.Log ("Mouse is over" + this.name);
			my_effect = Instantiate (mouseover_effect, my_position, transform.rotation);
			effect_status = true;
		}
	}

    //private void OnMouseDown()
    //{
    //    GameController.Instance.MoveSelectedCharacter(gameObject.transform.position);
    //}

    void OnMouseExit(){
		//Debug.Log("Mouse has left" + this.name);
		effect_status = false;
		Destroy (my_effect.gameObject);
	}

}

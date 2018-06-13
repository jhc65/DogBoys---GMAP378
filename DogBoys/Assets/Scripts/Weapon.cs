using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equippable {

	protected int maxShots;
	protected int shotsRemaining;
	protected int range;
	protected int moveRange;
	private bool reloading;
	protected int damage;

	public int getRange() {
		return range;
	}
		
	public int getMoveRange() {
		return moveRange;
	}
	public int getShotsRemaining(){
		return shotsRemaining;
	}

	 public override void use(Character chara){
		//Debug.Log ("Getting to use?");
		if (!reloading) {
			GameController.Instance.currentlySelectedCharacter.GetComponent<Character> ().anim.SetTrigger ("a_isShooting");
			fire (chara);
		} else {
			Debug.Log ("Reloading");
			GameController.Instance.currentlySelectedCharacter.GetComponent<Character> ().reload ();
			reload ();
		}
	}

	public void fire(Character chara) {
		Debug.Log ("Firing for " + damage.ToString () + " damage");
		chara.hurt (damage);
		shotsRemaining--;
		Debug.Log (shotsRemaining.ToString () + " shots left");
		if (shotsRemaining <= 0) {
			reloading = true;
		}
	}

	public void reload() {
		shotsRemaining = maxShots;
		reloading = false;
	}
}	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equippable {

	protected int maxShots;
	protected int shotsRemaining;
	protected int range;
	private bool reloading;
	protected int damage;

	public int getRange() {
		return range;
	}

	public int getShotsRemaining(){
		return shotsRemaining;
	}

	 public override void use(Character chara){
		//Debug.Log ("Getting to use?");
		if (!reloading) {
			fire (chara);
		} else {
			Debug.Log ("Reloading");
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
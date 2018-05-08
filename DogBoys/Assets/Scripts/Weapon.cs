using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equippable {

	protected int maxShots;
	private int shotsRemaining;
	protected float range;
	private bool reloading;
	protected int damage;

	public void use(Character chara){
		if (!reloading) {
			fire (chara);
		} else {
			reload ();
		}
	}

	public void fire(Character chara) {
		chara.hurt (damage);
		shotsRemaining--;
		if (shotsRemaining <= 0) {
			reloading = true;
		}
	}

	public void reload() {
		shotsRemaining = maxShots;
		reloading = false;
	}
}

public class Pistol : Weapon {
	public void Start() {
		maxShots = Constants.WeaponStats.PistolStats.maxShots;
		range = Constants.WeaponStats.PistolStats.range;
		damage = Constants.WeaponStats.PistolStats.damage;
	}
}

public class Shotgun : Weapon {
	public void Start() {
		maxShots = Constants.WeaponStats.ShotgunStats.maxShots;
		range = Constants.WeaponStats.ShotgunStats.range;
		damage = Constants.WeaponStats.ShotgunStats.damage;
	}
}

public class Sniper : Weapon {
	public void Start() {
		maxShots = Constants.WeaponStats.SniperStats.maxShots;
		range = Constants.WeaponStats.SniperStats.range;
		damage = Constants.WeaponStats.SniperStats.damage;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon {
	public void Start() {
		maxShots = Constants.WeaponStats.RifleStats.maxShots;
		shotsRemaining = maxShots;
		range = Constants.WeaponStats.RifleStats.range;
		damage = Constants.WeaponStats.RifleStats.damage;
	}
}

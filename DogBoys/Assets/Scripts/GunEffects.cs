using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffects : MonoBehaviour {

	public AudioSource rifleShot;
	public AudioSource revolverShot;
	public AudioSource shotgunShot;
	public AudioSource rifleReload;
	public AudioSource revolverReload;
	public AudioSource shotgunReload;
	public AudioSource hit;

	public ParticleSystem rifleShotP;
	public ParticleSystem revolverShotP;
	public ParticleSystem shotgunShotP;
	public ParticleSystem hitP;

	void ShootRifle() {
		rifleShot.Play ();
		rifleShotP.Play ();
	}

	void ReloadRifle() {
		rifleReload.Play ();
	}

	void ShootRevolver() {
		revolverShot.Play ();
		revolverShotP.Play ();
	}

	void ReloadRevolver() {
		revolverReload.Play ();
	}

	void ShootShotgun() {
		shotgunShot.Play ();
		shotgunShotP.Play ();
	}

	void ReloadShotgun() {
		shotgunReload.Play ();
	}

	void Hit() {
		hit.Play ();
		hitP.Play ();
	}
}

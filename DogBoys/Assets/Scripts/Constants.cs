using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants {
    // Camera Values
    public static class CameraStats {
        public static float C_cameraMoveSpeed = 20.0f;
    }
		
	// Weapon Values
	public static class WeaponStats {
		public static class PistolStats {
			public static int maxShots = 6;
			public static int range = 8;
			public static int damage = 12;
		}
		public static class ShotgunStats {
			public static int maxShots = 2;
			public static int range = 5;
			public static int damage = 34;
		}
		public static class RifleStats {
			public static int maxShots = 1;
			public static int range = 10;
			public static int damage = 40;
		}
	}
}

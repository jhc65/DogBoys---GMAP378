using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    #region Variables
    [SerializeField]
    private int health;
    [SerializeField]
	private string status;
    [SerializeField]
	private string weapon; //TODO: weapon class
    private bool canMove = false;
    private bool isMoving = false;
	private int xPos, yPos;

    #region Getters and Setters
    public 
    #endregion
    #endregion

    #region Character Functions
    void Die()
    {
        Destroy(gameObject);
    }

    void Move(int x, int y)
    {
        xPos = x;
        yPos = y;
        canMove = false;
    }
    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start () {
		canMove = true;
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
    #endregion
}

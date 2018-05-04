﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    #region Variables and Declarations
    private static GameController instance = null;
    [SerializeField]
    public GameObject currentlySelectedCharacter = null;
	public List<GameObject> p1Chars = new List<GameObject>();
	public List<GameObject> p2Chars = new List<GameObject>();
	private string turn = "";

    #region Getters and Setters
    public static GameController Instance {
        get { return instance; }
    }
    #endregion
    #endregion

    #region GameController Custom Methods
    public bool HasSelectedCharacter() {
        if (currentlySelectedCharacter) {
            return true;
        }
        else {
            return false;
        }
    }

    public void SetSelectedCharacter(GameObject charIn) {
        currentlySelectedCharacter = charIn;
    }

    private void UnselectCharacter() {
        currentlySelectedCharacter.GetComponent<Character>().UnselectCharacter();
        currentlySelectedCharacter = null;
    }

    public void MoveSelectedCharacter(Vector3 position) {
        if (currentlySelectedCharacter) {
            currentlySelectedCharacter.GetComponent<Character>().Move(position);
        }
    }

	private bool p1CanMove() {
		foreach (GameObject chara in p1Chars) {
			if (chara.GetComponent<Character> ().getCanMove ()) {
				Debug.Log ("P1 can still move");
				return true;
			}
		}
		return false;
	}

	private bool p2CanMove() {
		foreach (GameObject chara in p2Chars) {
			if (chara.GetComponent<Character> ().getCanMove ()) {
				Debug.Log ("P2 can still move");
				return true;
			}
		}
		return false;
	}


	public void updateTurns(){
		if (turn == "P1" && !p1CanMove ()) {
			StartP2Turn ();
		} else if (turn == "P2" && !p2CanMove ()) {
			StartP1Turn ();
		}
	}

	private void StartP1Turn() {
		Debug.Log ("P1 turn start");
		foreach (GameObject chara in p2Chars) {
			chara.GetComponent<Character> ().setCanMove (false);
		}
		foreach (GameObject chara in p1Chars) {
			chara.GetComponent<Character> ().setCanMove (true);
		}

		turn = "P1";
	}

	private void StartP2Turn() {
		Debug.Log ("P2 turn start");
		foreach (GameObject chara in p1Chars) {
			chara.GetComponent<Character> ().setCanMove (false);
		}
		foreach (GameObject chara in p2Chars) {
			chara.GetComponent<Character> ().setCanMove (true);
		}

		turn = "P2";
	}
    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start() {
		StartP1Turn ();
    }

    private void Awake() {
        if (!instance) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlySelectedCharacter && Input.GetMouseButtonDown(1)) {
            UnselectCharacter();
        }
    }
    #endregion
}

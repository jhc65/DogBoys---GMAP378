using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    #region Variables and Declarations
    private static GameController instance = null;
    [SerializeField]
    public GameObject currentlySelectedCharacter = null;
	public List<GameObject> p1Chars = new List<GameObject>();
	public List<GameObject> p2Chars = new List<GameObject>();
	public string turn = "";
    private bool gameOver = false;
	/*
	 * Board status
	 * 0 - empty
	 * 1 - character
	 * 2 - cover
	 * 
	 * */
	private int[,] gameBoard = new int[24,32];

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

	public void setSpace(int x, int y, int status){
		gameBoard [x,y] = status;
	}

	public void printBoard() {
		string Out = "";
		for (int i = 0; i < 24; i++) {
			for (int j = 0; j < 32; j++) {
				Out += gameBoard [i, j].ToString ();
			}
			Out += '\n';
		}
		Debug.Log (Out);
	}

    public void lineOfSight() {
        GameObject whatDidIHit = null;
        if (turn == "P1") {
            //Make sure that the current player's team is active 
            foreach (GameObject friendly in p1Chars) {
                friendly.GetComponent<Character>().turnOnGameObject();
            }
            
            //Each frame reset the enemy can be seen boolean to false
            foreach (GameObject enemy in p2Chars) {
                 enemy.GetComponent<Character>().CanBeSeen = false;
            }

            //Check to see if the enemy can be seen
            foreach (GameObject friendly in p1Chars) {
                foreach(GameObject enemy in p2Chars) {
                    whatDidIHit = friendly.GetComponent<Character>().characterLineOfSight(enemy.transform.position);
                    //Debug.Log(whatDidIHit.name);
                    //Debug.Log(enemy.name);
                    if (whatDidIHit != null)
                    {
                        if (enemy.name == whatDidIHit.name)
                        {
                            if (!enemy.GetComponent<Character>().CanBeSeen)
                            {
                                enemy.GetComponent<Character>().CanBeSeen = true;
                                List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                                tempList.Add(enemy.GetComponent<Character>());
                                friendly.GetComponent<Character>().EnemySeen = tempList;
                            }
                            //Update EnemySeen list if the target is already visable to the player
                            else
                            {
                                List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                                tempList.Add(enemy.GetComponent<Character>());
                                friendly.GetComponent<Character>().EnemySeen = tempList;
                            }
                        }
                    }
                    else
                    {
                        enemy.GetComponent<Character>().turnOnGameObject();
                        enemy.GetComponent<Character>().CanBeSeen = true;
                        List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                        tempList.Add(enemy.GetComponent<Character>());
                        friendly.GetComponent<Character>().EnemySeen = tempList;
                    }
                }
            }

            //Turn on/off characters if they can be seen
            foreach (GameObject enemy in p2Chars) {
                if (enemy.GetComponent<Character>().CanBeSeen || whatDidIHit == null) {
                    enemy.GetComponent<Character>().turnOnGameObject();
                }
                else {
                    enemy.GetComponent<Character>().turnOffGameObject();
                }
            }
        }
        else {
            //Make sure that the current player's team is active 
            foreach (GameObject friendly in p2Chars) {
                friendly.GetComponent<Character>().turnOnGameObject();
            }

            //Each frame reset the enemy can be seen boolean to false
            foreach (GameObject enemy in p1Chars) {
                enemy.GetComponent<Character>().CanBeSeen = false;
            }

            //Check to see if the enemy can be seen
            foreach (GameObject friendly in p2Chars) {
                foreach (GameObject enemy in p1Chars) {
                    whatDidIHit = friendly.GetComponent<Character>().characterLineOfSight(enemy.transform.position);
                    if (whatDidIHit != null)
                    {
                        if (enemy.name == whatDidIHit.name)
                        {
                            if (!enemy.GetComponent<Character>().CanBeSeen)
                            {
                                enemy.GetComponent<Character>().CanBeSeen = true;
                                List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                                tempList.Add(enemy.GetComponent<Character>());
                                friendly.GetComponent<Character>().EnemySeen = tempList;
                            }
                            //Update EnemySeen list if the target is already visable to the player
                            else
                            {
                                List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                                tempList.Add(enemy.GetComponent<Character>());
                                friendly.GetComponent<Character>().EnemySeen = tempList;
                            }
                        }
                    }
                    else
                    {
                        enemy.GetComponent<Character>().turnOnGameObject();
                        enemy.GetComponent<Character>().CanBeSeen = true;
                        List<Character> tempList = friendly.GetComponent<Character>().EnemySeen;
                        tempList.Add(enemy.GetComponent<Character>());
                        friendly.GetComponent<Character>().EnemySeen = tempList;
                    }
                }
            }

            //Turn on/off characters if they can be seen
            foreach (GameObject enemy in p1Chars) {
                if (enemy.GetComponent<Character>().CanBeSeen || whatDidIHit == null) {
                    enemy.GetComponent<Character>().turnOnGameObject();
                }
                else {
                    enemy.GetComponent<Character>().turnOffGameObject();
                }
            }

        }
    }

    public void winGame()
    {
        if (!gameOver)
        {
            if (p1Chars.Count == 0)
            {
                Debug.Log("Player 2 Wins!");
                gameOver = true;
            }
            else if (p2Chars.Count == 0)
            {
                Debug.Log("Player 1 Wins!");
                gameOver = true;
            }
        }
    }
    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start() {
		StartP1Turn ();
        lineOfSight();
    }

    private void Awake() {
        if (!instance) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lineOfSight();
        //winGame();
        if (currentlySelectedCharacter && Input.GetMouseButtonDown(1)) {
            UnselectCharacter();
        }
    }
    #endregion
}

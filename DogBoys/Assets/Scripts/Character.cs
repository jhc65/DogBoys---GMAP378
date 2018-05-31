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
	private Weapon weapon;
    [SerializeField]
    private GameObject characterHUD;

    private bool isOnOverwatch = true;
    private List<Character> enemySeen;
    private bool canMove = false;
    private bool isMoving = false;
    private bool isSelected = false;
    private bool canBeSeen = false;
    private int xPos, yPos;
    private Vector3 newPos;

    private GameController gc;

    #region Getters and Setters
    public bool getCanMove(){
		return canMove;
	}
	public void setCanMove(bool setter){
		canMove = setter;
	}
	public int hurt(int dmg){
		health -= dmg;
		if (health <= 0)
			Die ();
		return health;
	}
    public bool CanBeSeen
    {
        get
        {
            return canBeSeen;
        }

        set
        {
            canBeSeen = value;
        }
    }
    public bool IsOnOverwatch
    {
        get
        {
            return isOnOverwatch;
        }

        set
        {
            isOnOverwatch = value;
        }
    }
    public List<Character> EnemySeen
    {
        get
        {
            return enemySeen;
        }

        set
        {
            enemySeen = value;
        }
    }

    #endregion
    #endregion

    #region Character Functions
    void Die()
    {
		if (gc.p1Chars.Contains (gameObject))
			gc.p1Chars.Remove (gameObject);
		if (gc.p2Chars.Contains (gameObject))
			gc.p2Chars.Remove (gameObject);
		gc.setSpace (Mathf.RoundToInt (gameObject.transform.position.x), Mathf.RoundToInt (gameObject.transform.position.z), 0);
        Destroy(gameObject);
        gc.winGame();
    }

    public void Move(Vector3 position)
    {
        enemySeen.Clear();
        newPos = new Vector3(position.x, gameObject.transform.position.y, position.z);
		gc.setSpace (Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z), 0);
        gameObject.transform.position = newPos;
        //Debug.Log("move");
        //if (canMove) {
        //    newPos = new Vector3(position.x, gameObject.transform.position.y, position.z);
        //    isMoving = true;
        //}

		gc.setSpace (Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z), 1);
		gc.printBoard ();

		canMove = false;
		UnselectCharacter ();

        //While moving, update LoS info
        gc.lineOfSight();

        //Overwatch attack
        if (enemySeen.Count > 0) {
            Debug.Log("Enemy sees me");
            foreach (Character enemy in enemySeen)
            {
                if (enemy.IsOnOverwatch)
                {
                    //Debug.Log("Enemy shoot me");
                    overwatchAttack(enemy);
                }
            }
        }
        enemySeen.Clear();
    }

    private void CenterOnSpace() {
        // Don't collide with Player layer

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.down) * 3f, out hit, Mathf.Infinity)) {
            Vector3 centered = new Vector3(hit.transform.position.x, gameObject.transform.position.y, hit.transform.position.z);
            gameObject.transform.position = centered;
        }
    }

    private void ToggleIsSelected() {
        if (isSelected) {
            isSelected = false;
        }
        else {
            isSelected = true;
        }
    }

    private void SetIsSelected(bool inSelection) {
        isSelected = inSelection;
    }

    private void SelectCharacter() {
        SetIsSelected(true);
        characterHUD.SetActive(true);
        gc.SetSelectedCharacter(gameObject);
    }

    public void UnselectCharacter() {
        SetIsSelected(false);
        characterHUD.SetActive(false);
		gc.SetSelectedCharacter (null);
		gc.updateTurns ();
    }

	public void Shoot(Character enemy){
		weapon.use (enemy);
	}

    public GameObject characterLineOfSight(Vector3 enemiesLocation)
    {
        RaycastHit hitMyTarget;
        Vector3 raycastFromHere = transform.position;
        enemiesLocation.y += 0.5f;
        raycastFromHere.y += 0.5f;
        if (Physics.Linecast(raycastFromHere, enemiesLocation, out hitMyTarget))
        {
            //Debug.DrawLine(raycastFromHere, enemiesLocation, Color.yellow);
            return hitMyTarget.collider.gameObject;
        }
        else
        {
            //Debug.DrawLine(raycastFromHere, enemiesLocation, Color.red);
            return null;
        }
    }

    public void overwatchAttack(Character enemy) {
        enemy.Shoot(this);
        //enemy.IsOnOverwatch = false;
    }

    public void turnOffGameObject() {
        gameObject.SetActive(false);
    }

    public void turnOnGameObject() {
        gameObject.SetActive(true);
    }
        #endregion

        #region Unity Overrides
        // Use this for initialization
        void Start () {
        gc = GameController.Instance;
        enemySeen = new List<Character>();
        canMove = true;
		health = 100;
		status = "";

        CenterOnSpace();
		gc.setSpace (Mathf.RoundToInt (gameObject.transform.position.x), Mathf.RoundToInt (gameObject.transform.position.z), 1);
    }
	
	// Update is called once per frame
	void Update () {

        //if (isMoving) {
        //    if (gameObject.transform.position != newPos) {
        //        Vector3.Lerp(gameObject.transform.position, newPos, (Time.deltaTime * 0.5f));
        //    }
        //    else {
        //        isMoving = false;
        //    }
        //}
    }

    private void OnMouseOver() {
        if (!isMoving && Input.GetMouseButtonDown(0) && canMove) {
            SelectCharacter();
        }
		if (Input.GetMouseButtonDown (0) && gc.HasSelectedCharacter() && gc.currentlySelectedCharacter != gameObject) {
			Character selected = gc.currentlySelectedCharacter.GetComponent<Character> ();

			//Attack this character and end the other character's turn
			Debug.Log("Pow");
			selected.Shoot(this);
			Debug.Log ("I have " + health.ToString() + " health left");
			selected.setCanMove (false);
			selected.UnselectCharacter ();
			gc.currentlySelectedCharacter = null;
			gc.updateTurns ();

		}
    }
    #endregion
}

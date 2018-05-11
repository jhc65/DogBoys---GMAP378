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

    private bool canMove = false;
    private bool isMoving = false;
    private bool isSelected = false;
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

		characterHUD.GetComponent<UI_Controller> ().updateCurrentHealthBar (health, 100);
		return health;
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
        Destroy(gameObject);
    }

    public void Move(Vector3 position)
    {
        newPos = new Vector3(position.x, gameObject.transform.position.y, position.z);
        gameObject.transform.position = newPos;
        //Debug.Log("move");
        //if (canMove) {
        //    newPos = new Vector3(position.x, gameObject.transform.position.y, position.z);
        //    isMoving = true;
        //}

		canMove = false;
		UnselectCharacter ();

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
    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start () {
        gc = GameController.Instance;
        canMove = true;
		health = 100;
		status = "";

        CenterOnSpace();
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

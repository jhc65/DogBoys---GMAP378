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
    #endregion
    #endregion

    #region Character Functions
    void Die()
    {
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
    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start () {
        gc = GameController.Instance;
        canMove = true;
		health = 100;
		status = "";
		weapon = "";

        CenterOnSpace();
    }
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Die ();
		}
        
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
    }
    #endregion
}

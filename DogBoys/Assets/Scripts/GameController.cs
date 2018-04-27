using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    #region Variables and Declarations
    private static GameController instance;
    [SerializeField]
    private GameObject currentlySelectedCharacter = null;

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
    #endregion

    #region Unity Overrides
    // Use this for initialization
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            UnselectCharacter();
        }
    }
    #endregion
}

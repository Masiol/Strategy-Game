using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceUIManager : MonoBehaviour
{
    public List<GameObject> buttons;

    private GameObject currentButton;
    [SerializeField] private string[] Race;

    private UnitUIManager unitUIManager;

    private void Start()
    {
        unitUIManager = FindObjectOfType<UnitUIManager>();
    }

    public void ButtonClicked(GameObject _clickedButton)
    {
        if (_clickedButton == currentButton)
        {
            ResetButtons();
        }
        else
        {
            if (currentButton != null)
            {
                currentButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
                currentButton = null;
            }
            currentButton = _clickedButton;
            SetupUIUnits(_clickedButton.name);
            currentButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

    public void ResetButtons()
    {
        if (currentButton != null)
        {
            currentButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
            currentButton = null;
        }
    }

    private void SetupUIUnits(string _name)
    {
        if (!Race.Contains(_name))
        {
            Debug.Log("Button name no match");
            return;
        }
        else
        {
            switch (_name)
            {
                case "Human":
                    unitUIManager.LoadUnits("Human");
                    break;          
            }
        }
    }
}

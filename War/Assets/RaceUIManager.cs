using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class RaceUIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons;
    [SerializeField] private string[] Race;
    [SerializeField] private RectTransform panel;
    [SerializeField] private float showPositionY;
    [SerializeField] private float hidePositionY;
    public float animationDuration = 0.5f;
    public Ease easingType = Ease.OutQuart;
    private GameObject currentButton;
    


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
            panel.DOAnchorPosY(showPositionY, animationDuration).SetEase(easingType);
            switch (_name)
            {
                case "Human":
                    unitUIManager.LoadUnits("Human");
                    break;
                case "Orcs":
                    unitUIManager.LoadUnits("Orcs");
                    break;
                case "Elves":
                    unitUIManager.LoadUnits("Evles");
                    break;
                case "Undead":
                    unitUIManager.LoadUnits("Undead");
                    break;
            }

        }
    }

    public void HideUIUnitPanel()
    {
        panel.DOAnchorPosY(hidePositionY, animationDuration).SetEase(easingType);

    }
}

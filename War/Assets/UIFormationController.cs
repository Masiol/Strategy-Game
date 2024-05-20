using UnityEngine;
using UnityEngine.UI;

public class UIFormationController : MonoBehaviour
{
    public FormationManager formationManager;
    public Button circleFormationButton;
    public Button lineFormationButton;

    void Start()
    {
        circleFormationButton.onClick.AddListener(() => formationManager.SetFormation(new CircleFormation()));
        lineFormationButton.onClick.AddListener(() => formationManager.SetFormation(new LineFormation()));
    }
}

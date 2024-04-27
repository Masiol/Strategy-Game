using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectedFormationOptions : MonoBehaviour
{
    [SerializeField] private RectTransform parent;
    [SerializeField] private Button changeFormationButton;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button moveButton;

    private void Start()
    {
        changeFormationButton.onClick.AddListener(ChangeFormation);
        removeButton.onClick.AddListener(RemoveFormation);
        moveButton.onClick.AddListener(MoveFormation);
    }
    public void ShowButtons()
    {
        parent.gameObject.SetActive(true);
        parent.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutQuad);
    }
    public void HideButtons()
    {
        parent.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            parent.gameObject.SetActive(false);
        });
    }

    public void ChangeFormation()
    {
        FindObjectOfType<FormationUIController>().ShowFormationUI();
    }
    public void RemoveFormation()
    {
        FindObjectOfType<FormationSelector>().DeselectAndDestroyFormation();

    }
    public void MoveFormation()
    {

    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private float showPositionY;
    [SerializeField] private float hidePositionY;
    public float animationDuration = 0.5f;
    public Ease easingType = Ease.OutQuart;

    [SerializeField] private RectTransform unitElement;
    [SerializeField] private RectTransform parent;

    private List<GameObject> unitsAvatar = new List<GameObject>();

    public void LoadUnits(string _raceName)
    {
        if(unitsAvatar.Count != 0)
        {
            for (int i = 0; i < unitsAvatar.Count; i++)
            {
                Destroy(unitsAvatar[i].gameObject);
            }
            unitsAvatar.Clear();
        }

        UnitBase[] units = Resources.LoadAll<UnitBase>("ScriptableObjects/Race/" + _raceName + "/Units");

        for (int i = 0; i < units.Length; i++)
        {
            var go = Instantiate(unitElement);
            go.SetParent(parent);
            //go.GetComponent<Unit>().unit = units[i];
            unitsAvatar.Add(go.gameObject);

        }
    }

    public void ShowUIUnitPanel()
    {
        //panel.DOAnchorPosY(showPositionY, animationDuration).SetEase(easingType);
    }
    public void HideUIUnitPanel()
    {
       // panel.DOAnchorPosY(hidePositionY, animationDuration + 0.5f).SetEase(easingType);

    }
}

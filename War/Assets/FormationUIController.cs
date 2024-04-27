using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationUIController : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private float showPositionX;
    [SerializeField] private float hidePositionX;
    public float animationDuration = 0.5f;
    public Ease easingType = Ease.OutQuart;

    private void Start()
    {
        panel.DOAnchorPosX(hidePositionX, 0).SetEase(easingType);

    }

    public void HideFormationUI()
    {
        panel.DOAnchorPosX(hidePositionX, animationDuration).SetEase(easingType);

    }

    public void ShowFormationUI()
    {
        panel.DOAnchorPosX(showPositionX, animationDuration).SetEase(easingType);
    }
}

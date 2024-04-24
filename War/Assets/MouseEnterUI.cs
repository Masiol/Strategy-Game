using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public enum AnimationAxis
{
    X,
    Y
}

public class MouseEnterUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform panel;
    public float animationDuration = 0.5f;
    public Ease easingType = Ease.OutQuart;
    public AnimationAxis animationAxis;
    [SerializeField] private float showPositionX;
    [SerializeField] private float hidePositionX;
    [SerializeField] private float showPositionY;
    [SerializeField] private float hidePositionY;
    

    private void Awake()
    {
        panel = GameObject.Find("UnitInfoPanel").GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animationAxis == AnimationAxis.X)
            panel.DOAnchorPosX(showPositionX, animationDuration).SetEase(easingType);
        else if (animationAxis == AnimationAxis.Y)
            panel.DOAnchorPosY(showPositionY, animationDuration).SetEase(easingType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animationAxis == AnimationAxis.X)
            panel.DOAnchorPosX(hidePositionX, animationDuration).SetEase(easingType);
        else if (animationAxis == AnimationAxis.Y)
            panel.DOAnchorPosY(hidePositionY, animationDuration).SetEase(easingType);
    }
}

using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class UnitController : MonoBehaviour
{
    [SerializeField] public float destinationReachedThreshold = 0.1f;
    [SerializeField] private UnitBase unitBase;
    [HideInInspector] public NavMeshAgent agent;
    private Animator animator;
    private bool isMovingToTarget = false;
    private Vector3 newPoint;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        CustomIdle();
    }

    public void MoveTo(Vector3 destination)
    {
        CustomRun();
        agent.SetDestination(destination);
        newPoint = destination;
        isMovingToTarget = true;
        FormationArmy formationArmy = GetComponentInParent<FormationArmy>();
       // formationArmy.GetNewPositionsForEachUnit(newPoint);
    }

    private void Update()
    {
        if (isMovingToTarget)
        {
            if (!agent.pathPending && agent.remainingDistance <= destinationReachedThreshold)
            {
                // Agent dotar³ do celu
                CustomIdle();
                FormationArmy formationArmy = GetComponentInParent<FormationArmy>();
                if (formationArmy != null && isMovingToTarget)
                {
                    transform.DORotate(Vector3.zero, 0.2f);
                    isMovingToTarget = false;
                    formationArmy.UnitReachedDestination();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(newPoint, 1);
    }

    public void CustomAttack()
    {
       // Debug.Log("dziala");
        foreach (var layer in unitBase.animSettings.animSettingsAttack.animLayers)
        {
            if (layer.layerWeight.changeSmooth)
            {
                SetLayerWeightSmooth(layer.layersIndex, layer.layerWeight.layerWeight, layer.layerWeight.speed); 
            }
            else
            {
                animator.SetLayerWeight(layer.layersIndex, layer.layerWeight.layerWeight);
            }
        }
        animator.SetFloat("Speed_f", unitBase.animSettings.animSettingsAttack.speed);
        animator.SetInteger("WeaponType_int", unitBase.animSettings.animSettingsAttack.weaponType);
        animator.SetInteger("MeleeType_int", unitBase.animSettings.animSettingsAttack.meleeTypeInt);

    }
    public void CustomIdle()
    {
        foreach (var layer in unitBase.animSettings.animSettingsIdle.animLayers)
        {
            if (layer.layerWeight.changeSmooth)
            {
                SetLayerWeightSmooth(layer.layersIndex, layer.layerWeight.layerWeight, layer.layerWeight.speed);
            }
            else
            {
                animator.SetLayerWeight(layer.layersIndex, layer.layerWeight.layerWeight);
            }
        }
        animator.SetInteger("WeaponType_int", unitBase.animSettings.animSettingsIdle.weaponType);
        animator.SetInteger("MeleeType_int", unitBase.animSettings.animSettingsIdle.meleeTypeInt);
        animator.SetFloat("Speed_f", unitBase.animSettings.animSettingsIdle.speed);
    }

    public void CustomRun()
    {
        foreach (var layer in unitBase.animSettings.animSettingsRun.animLayers)
        {
            if (layer.layerWeight.changeSmooth)
            {
                SetLayerWeightSmooth(layer.layersIndex, layer.layerWeight.layerWeight, layer.layerWeight.speed);
            }
            else
            {
                animator.SetLayerWeight(layer.layersIndex, layer.layerWeight.layerWeight);
            }
        }
        animator.SetFloat("Speed_f", unitBase.animSettings.animSettingsRun.speed);
        animator.SetInteger("WeaponType_int", unitBase.animSettings.animSettingsRun.weaponType);
        animator.SetInteger("MeleeType_int", unitBase.animSettings.animSettingsRun.meleeTypeInt);
    }
    private void SetLayerWeightSmooth(int layerIndex, float targetWeight, float duration)
    {
        DOTween.To(() => animator.GetLayerWeight(layerIndex), x => animator.SetLayerWeight(layerIndex, x), targetWeight, duration);
    }
}
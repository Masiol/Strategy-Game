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
    }

    public void MoveTo(Vector3 destination)
    {
        animator.SetFloat("Speed_f", 1.0f);
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
                animator.SetFloat("Speed_f", 0.0f);
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
}
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Unit : MonoBehaviour
{
    private State currentState;
    public NavMeshAgent agent;
    public Animator animator;
    public UnitBase unitBase;
    public string enemyTag;
    public GameObject enemyObject;

    public IdleState idleState { get; set; }
    public MoveToEnemyState moveToEnemyState { get; set; }
    public DiedState diedState { get; set; }
    public AttackState attackState { get; set; }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        moveToEnemyState = ScriptableObject.CreateInstance<MoveToEnemyState>();
        idleState = ScriptableObject.CreateInstance<IdleState>();
        diedState = ScriptableObject.CreateInstance<DiedState>();
        attackState = ScriptableObject.CreateInstance<AttackState>();

        GameEvents.StartGame += () => TransitionToState(moveToEnemyState);
        GameEvents.StartGame += FindEnemy;
    }

    private void Start()
    {
        TransitionToState(idleState);
        enemyTag = gameObject.tag == "PlayerUnit" ? "EnemyUnit" : "PlayerUnit";

    }
    private void FindEnemy()
    {
        enemyObject = FindClosestEnemy();
        StartCoroutine(FindClosestEnemyCoroutine());

    }

    private void Update()
    {
        currentState?.Execute(this);
    }

    public virtual void Attack()
    {
        // Implementacja ataku
    }

    public void TransitionToState(State newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetAnimation(string anim, bool state)
    {
        animator.SetBool(anim, state);
    }

    private IEnumerator FindClosestEnemyCoroutine()
    {
        while (true)
        {
            FindClosestEnemy();
            if (enemyObject == null)
            {
                TransitionToState(idleState);

                agent.enabled = false;
            }
            yield return new WaitForSeconds(1.0f); 
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Unit enemyUnit = enemy.GetComponent<Unit>();
            if (enemyUnit != null && enemyUnit.enabled)
            {
                float distance = Vector3.Distance(enemy.transform.position, currentPosition);
                if (distance < closestDistance)
                {
                    closest = enemy;
                    closestDistance = distance;
                }
            }
        }

        enemyObject = closest;
        return closest;
    }


    public void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.deltaTime * 15f);
    }
}

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Unit : MonoBehaviour
{
    public State currentState;
    public NavMeshAgent agent;
    public Animator animator;
    public UnitBase unitBase;
    public string enemyTag;
    public GameObject enemyObject;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        enemyTag = gameObject.tag == "PlayerUnit" ? "EnemyUnit" : "PlayerUnit";
    }

    private void OnEnable()
    {
        MoveToEnemyState moveToEnemyState = new MoveToEnemyState();
        GameEvents.StartGame += () => TransitionToState(moveToEnemyState);
    }

    private void Start()
    {
        TransitionToState(currentState);
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
            yield return new WaitForSeconds(1.0f); // Czekaj 1 sekundê przed ponownym sprawdzeniem
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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

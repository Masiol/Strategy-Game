using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour
{
    public UnitBase unit;
    [SerializeField] private float searchRadius = 150f;
    protected Animator animator;
    [HideInInspector] public string enemyTag;
    public GameObject enemy;
    protected NavMeshAgent agent;

    private float noEnemyFoundTimer = 0f;
    private const float noEnemyFoundThreshold = 0.01f;
    private bool lookingForEnemy;

    public enum State
    {
        Idle,
        Search,
        MoveToEnemy,
        Attack,
        Died
    }

    public State currentState = State.Idle;

    protected virtual void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyTag = gameObject.tag == "PlayerUnit" ? "EnemyUnit" : "PlayerUnit";
        agent.speed = unit.TotalSpeed;
    }

    private void OnEnable()
    {
        GameEvents.StartGame += StartUnitLogic;
        GetComponent<HealthSystem>().OnDeath += Death;
    }

    private void OnDisable()
    {
        GameEvents.StartGame -= StartUnitLogic;
        GetComponent<HealthSystem>().OnDeath -= Death;
    }

    private void StartUnitLogic()
    {
        TransitionToState(State.Search);
        lookingForEnemy = true;
    }

    private void Update()
    {
        CheckEnemyStatus();
        PerformStateActions();
        if (lookingForEnemy)
            SearchForEnemy();

        if (currentState == State.Search || currentState == State.MoveToEnemy)
        {
            if (enemy == null)
            {
                noEnemyFoundTimer += Time.deltaTime;
                if (noEnemyFoundTimer >= noEnemyFoundThreshold)
                {
                    TransitionToState(State.Idle);
                    noEnemyFoundTimer = 0f;
                    agent.enabled = false;
                }
            }
            else
            {
                noEnemyFoundTimer = 0f;
            }
        }
    }

    private void CheckEnemyStatus()
    {
        if (enemy != null && enemy.GetComponent<HealthSystem>().isDead)
        {
            enemy = null;
            TransitionToState(State.Search);
        }
    }

    private void PerformStateActions()
    {
        switch (currentState)
        {
            case State.Search:
                animator.SetBool("Attack", false);
                SearchForEnemy();
                break;
            case State.MoveToEnemy:
                MoveToEnemy();
                break;
            case State.Attack:
                FaceTarget(enemy.transform.position);
                ContinueAttack();
                break;
            case State.Died:
                // Handle death logic if necessary
                break;
        }
    }

    private void SearchForEnemy()
    {
        if (enemy == null)
        {
            enemy = FindClosestEnemy();
            if (enemy)
                TransitionToState(State.MoveToEnemy);
        }
    }

    private void Death()
    {
        TransitionToState(State.Died);
        animator.SetBool("Died", true);
        int randomDeath = UnityEngine.Random.Range(0, 3);
        animator.SetInteger("Death_Int", randomDeath);
        animator.SetTrigger("Death");
        agent.ResetPath();
        gameObject.tag = "DiedUnit";
        agent.enabled = false;
    }

    private void MoveToEnemy()
    {
        if (enemy != null && !GetComponent<HealthSystem>().isDead)
        {
            agent.SetDestination(enemy.transform.position);
            FaceTarget(enemy.transform.position);
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= unit.attackRange)
                TransitionToState(State.Attack);
            else if (distance > searchRadius)
                enemy = null;
        }
    }

    private void ContinueAttack()
    {
        if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= unit.attackRange)
        {
            FaceTarget(enemy.transform.position);
            agent.ResetPath();
            Attack();
        }
        else
        {
            TransitionToState(State.Search);
        }
    }

    protected abstract void Attack();

    protected void TransitionToState(State newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        UpdateAnimator(newState);
        if (newState != State.Idle)
        {
            noEnemyFoundTimer = 0f;  // Ensure timer is reset on state change
        }
    }

    private void UpdateAnimator(State state)
    {
        animator.SetBool("Idle", state == State.Idle);
        animator.SetBool("Walk", state == State.MoveToEnemy);

    }

    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance && !hitCollider.GetComponent<HealthSystem>().isDead)
                {
                    closestEnemy = hitCollider.gameObject;
                    closestDistance = distance;
                }
            }
        }
        return closestEnemy;
    }

    protected void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.deltaTime * 2f);
    }
}

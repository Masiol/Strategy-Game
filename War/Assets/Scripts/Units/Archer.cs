using UnityEngine;

public class Archer : Unit
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    [SerializeField] private float launchAngle = 25.0f;
    [SerializeField] private float initialVelocity = 30.0f;
    [SerializeField] private float extraForce = 1.0f;
    [SerializeField] private float minForce = 0.5f;
    [SerializeField] private float maxForce = 2.0f;
    [SerializeField] private float xVariationMin = 0.5f;
    [SerializeField] private float xVariationMax = 2.0f;

    protected override void Awake()
    {
        base.Awake();
        animator.SetFloat("AttackSpeed", unitBase.baseAttackSpeed);
    }

    public override void Attack()
    {
        base.SetAnimation("Attack", true);
    }
    public void PerformAttack()
    {
        if(enemyObject != null)
        ShootArrow(enemyObject.transform, base.enemyTag, base.unitBase.TotalDamage);
    }

    private void ShootArrow(Transform _target, string _tag, int _damage)
    {
        if (_target != null && arrowPrefab != null)
        {
            Vector3 toTarget = _target.position - arrowSpawnPoint.position;
            float distance = new Vector3(toTarget.x, 0, toTarget.z).magnitude;
            float yOffset = _target.position.y - arrowSpawnPoint.position.y;

            float baseVelocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * launchAngle * Mathf.Deg2Rad));

            float randomFactor = Random.Range(minForce, maxForce); 
            float velocity = baseVelocity * randomFactor;

            float randomPos = Random.Range(xVariationMin, xVariationMax);

            Vector3 horizontalDirection = new Vector3(toTarget.x + randomPos, 0, toTarget.z).normalized;

            float horizontalVelocity = velocity * Mathf.Cos(launchAngle * Mathf.Deg2Rad);
            float verticalVelocity = velocity * Mathf.Sin(launchAngle * Mathf.Deg2Rad);

            Vector3 velocityVector = horizontalDirection * horizontalVelocity + Vector3.up * verticalVelocity;

            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.LookRotation(horizontalDirection));
            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
            arrowRb.velocity = velocityVector;
            arrow.GetComponent<Arrow>().Setup(_tag, _damage);
        }
    }


}

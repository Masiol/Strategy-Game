using Unity.Burst.CompilerServices;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    private string enemyTag;
    private int damage;
    private DamageController damageController;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("EnableObject", 0.05f);
        damageController = new DamageController();
    }

    private void EnableObject()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void Setup(string _tag, int _damage)
    {
        enemyTag = _tag;
        damage = _damage;
    }

    private void FixedUpdate()
    {
        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
            transform.Rotate(90f, 180f, 0f);
        }
    }

    private void OnTriggerEnter(Collider _other)
    { 
        if(_other.tag == "Ground")
        {
            rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }
        if(_other.tag == enemyTag)
        {
            float damageArorow = damageController.CalculateDamage(damage);
            damageController.ApplyDamage(_other.gameObject, damageArorow);
            Destroy(gameObject);
        }
    }
}
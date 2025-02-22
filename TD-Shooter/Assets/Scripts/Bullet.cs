using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed;
    public float lifeTime;
    public int damage;

    private Vector3 prevPosition;

    public LayerMask mask;

    private GameObject triggeringEnemy;

    private void Start()
    {
        prevPosition = transform.position;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.fixedDeltaTime);

        if (Physics.Raycast(prevPosition, transform.forward, out RaycastHit hit, (prevPosition - transform.position).magnitude, mask))
        {
            hit.transform.GetComponent<HealthController>().TakeDamage(damage);

            Destroy(gameObject);
        }

        prevPosition = transform.position;

        Destroy(gameObject, lifeTime);
    }

}

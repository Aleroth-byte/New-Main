using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Equip(Collider playerCollider, Transform weaponAttachPoint)
    {
        rb.isKinematic = true;
        transform.SetParent(weaponAttachPoint);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        Physics.IgnoreCollision(playerCollider, GetComponent<Collider>());
    }

    public void Drop(Collider playerCollider, Vector3 playerForward)
    {
        transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(playerForward * 10, ForceMode.Impulse);
        StartCoroutine(DropCooldown(playerCollider));
    }

    IEnumerator DropCooldown(Collider playerCollider)
    {
        yield return new WaitForSeconds(3);
        Physics.IgnoreCollision(GetComponent<Collider>(), playerCollider, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the weapon collides with an enemy
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Deal 10 damage to the enemy
            enemy.TakeDamage(10f);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableEnemy : MonoBehaviour
{
    bool canBeDestroyed = false;
    void Start()
    {

    }
    void Update()
    {
        if (transform.position.x < 9)
            canBeDestroyed = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!canBeDestroyed) return;

        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableEnemy : MonoBehaviour
{
    bool canBeDestroyed = false;
    GameManager gameManager;
    [SerializeField] private GameObject explotionAnim;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            gameManager.EnemyDie();
            Instantiate(explotionAnim, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableEnemy : MonoBehaviour
{
    bool canBeDestroyed = false;
    GameManager gameManager;
    [SerializeField] private GameObject explotionAnim;
    [SerializeField] private GameObject[] powerups;
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
            SelectPowerUp();
            Destroy(gameObject);
        }
    }

    private void SelectPowerUp()
    {
        int r = Random.Range(1, 101);
        if(r <= 4) Instantiate(powerups[0], transform.position, Quaternion.identity);
        if(r > 4 && r <=10) Instantiate(powerups[1], transform.position, Quaternion.identity);
    }

}

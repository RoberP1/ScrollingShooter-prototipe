using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    private bool ingame;
    private Player player;
    private GameManager gameManager;

    [Header("Health")]
    public int maxhealth;
    private int health;
    [SerializeField] private Slider healthSlider;

    [Header("Shot")]
    [SerializeField]private float attackDelay;
    [SerializeField]private float bulletImpulse;
    [SerializeField] private Transform torret1;
    [SerializeField] private Transform torret2;
    [SerializeField] private Transform missilTorret1;
    [SerializeField] private Transform missilTorret2;
    [SerializeField] private Transform lazerTorret;
    [SerializeField] private GameObject BulletPref;
    private bool canattack;
    private bool mechanic3;
    private bool mechanic2;
    private int maxattacks = 3;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        healthSlider.value = healthSlider.maxValue = health = maxhealth;   
    }


    void Update()
    {
        if (!ingame) 
        {
            Move();
        } 
        else
        {
            Attack();
        }
        if (mechanic3)
        {
            Vector3 direction = Vector3.zero;
            direction.y = player.transform.position.y - transform.position.y;
            transform.position += direction * Time.deltaTime * moveSpeed;
        }
        else
        {
            Vector3 direction = Vector3.zero;
            direction.y = 0.2f - transform.position.y;
            transform.position += direction * Time.deltaTime * moveSpeed;
        }

    }

    private void Attack()
    {
        if (canattack)
        {   
            StartCoroutine(AttackDelay(attackDelay));
            int random = UnityEngine.Random.Range(1, maxattacks);
            switch (random)
            {
                case 1:
                    StartCoroutine(Mechanic1Delay(0));
                    StartCoroutine(Mechanic1Delay(1));
                    StartCoroutine(Mechanic1Delay(2));
                    break;
                case 2:
                    StartCoroutine(Mechanic2Delay());
                    break;
                case 3:
                    StartCoroutine(Mechanic3Delay());
                    break;
                default:
                    break;
            }
        }
    }

    private void Mechanic1()
    {
        GameObject bullet = Instantiate(BulletPref, torret1.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce((-bullet.transform.position + player.transform.position).normalized * bulletImpulse, ForceMode2D.Impulse);
        GameObject bullet2 = Instantiate(BulletPref, torret2.position, Quaternion.identity);
        bullet2.GetComponent<Rigidbody2D>().AddRelativeForce((-bullet2.transform.position + player.transform.position).normalized * bulletImpulse, ForceMode2D.Impulse);
    }

    private void Move()
    {
        if (transform.position.x >= 8.3)
        {
            Vector2 pos = transform.position;
            pos.x -= moveSpeed * Time.deltaTime;
            transform.position = pos;
        }
        else ingame = canattack = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ingame && collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            LoseHealth();
        }
    }
    private void LoseHealth()
    {
        health--;
        healthSlider.value = health;
        if (health == maxhealth/2)
        {
            maxattacks = 4;
        }
        if (health <= 0)
        {
            gameManager.Win();
        }
    }
    public IEnumerator AttackDelay(float delay)
    {
        canattack = false;
        yield return new WaitForSeconds(delay);
        canattack = true;
    }
    public IEnumerator Mechanic1Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Mechanic1();
    }
    public IEnumerator Mechanic3Delay()
    {
        mechanic3 = true;
        maxattacks = 3;
        StartCoroutine(Mechanic3());
        yield return new WaitForSeconds(9.5f);
        mechanic3 = false;
        maxattacks = 4;
    }   
    public IEnumerator Mechanic3()
    {
        
        GameObject bullet = Instantiate(BulletPref, lazerTorret.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce((Vector2.left).normalized * bulletImpulse, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        if (mechanic3) {
            StartCoroutine(Mechanic3());
        }
    }
    public IEnumerator Mechanic2Delay()
    {
        mechanic2 = true;
        StartCoroutine(Mechanic2());
        yield return new WaitForSeconds(4.3f);
        mechanic2 = false;

    }
    public IEnumerator Mechanic2()
    {

        GameObject bullet = Instantiate(BulletPref, missilTorret1.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce((Vector2.left).normalized * bulletImpulse, ForceMode2D.Impulse);   
        GameObject bullet2 = Instantiate(BulletPref, missilTorret2.position, Quaternion.identity);
        bullet2.GetComponent<Rigidbody2D>().AddRelativeForce((Vector2.left).normalized * bulletImpulse, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        if (mechanic2)
        {
            StartCoroutine(Mechanic2());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    SpriteRenderer spriteRenderer;

    [SerializeField] private AudioSource audio;
    // Player Movement
    [Header("Movement")]
    public float speed;
    private Rigidbody2D rb;

    //shot
    [Header("Shooting")]
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private GameObject shotPref;
    [SerializeField] private float fireImpulse;
    [SerializeField] private Transform fireTransformMid;
    [SerializeField] private Transform fireTransformUp;
    [SerializeField] private Transform fireTransformDown;
    public float fireRate;
    public int guns;
    private bool canShot;

    //health
    public bool canTakeDamage;
    public int health;
    public bool shildActive;
    public GameObject shild;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private AudioClip healthSound;

    void Start()
    {
        shild.SetActive(false);
        healthSlider.value = health;
        gameManager = FindObjectOfType<GameManager>();
        canShot = canTakeDamage = true;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && canShot)
        {
            switch (guns)
            {
                case 1:
                    Firemid();
                    break;
                case 2:
                    FireUp();
                    FireDown();
                    break;
                default:
                    Firemid();
                    FireUp();
                    FireDown();
                    break;
            }
            StartCoroutine(FireCD(fireRate));
            audio.clip = fireSound;
            audio.Play();
        }

    }

    private void FireDown()
    {
        GameObject bullet = Instantiate(shotPref, fireTransformDown.position, fireTransformDown.rotation);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(bullet.transform.right * fireImpulse, ForceMode2D.Impulse);
        Destroy(bullet, 2);
    }

    private void FireUp()
    {
        GameObject bullet = Instantiate(shotPref, fireTransformUp.position, fireTransformUp.rotation);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(bullet.transform.right * fireImpulse, ForceMode2D.Impulse);
        Destroy(bullet, 2);
    }

    private void Firemid()
    {
        GameObject bullet = Instantiate(shotPref, fireTransformMid.position, fireTransformMid.rotation);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(bullet.transform.right * fireImpulse, ForceMode2D.Impulse);
        Destroy(bullet, 2);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        rb.velocity = movement * speed;
        if (!canTakeDamage)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }
    public IEnumerator FireCD(float fireRate)
    {
        canShot = false;
        yield return new WaitForSeconds(1.0f / fireRate);
        canShot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("BulletEnemy"))
        {
            LoseHealth();
        }
    }
    private void LoseHealth()
    {

        if (!canTakeDamage) return;
        if (shildActive)
        {
            shildActive = false;
            shild.SetActive(false);
            return;
        }
        health--;
        healthSlider.value = health;
        if (health <= 0)
        {
            gameManager.Lose();
        }
        StartCoroutine(DamageCD(4));
        audio.clip = healthSound;
        audio.Play();
    }
    public IEnumerator DamageCD(float delay)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(delay);
        canTakeDamage = true;
        spriteRenderer.enabled = true;
    }
}

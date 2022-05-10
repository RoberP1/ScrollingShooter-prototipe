using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    void Start()
    {
        canShot = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && canShot)
        {
            StartCoroutine(FireCD(fireRate));
            GameObject bullet = Instantiate(shotPref, fireTransformMid.position, fireTransformMid.rotation);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(bullet.transform.right * fireImpulse, ForceMode2D.Impulse);
            Destroy(bullet, 2);
            //audio.clip = fireSound;
            //audio.Play();
            if (guns >= 2)
            {
                StartCoroutine(FireCD(fireRate));
                GameObject bullet2 = Instantiate(shotPref, fireTransformUp.position, fireTransformUp.rotation);
                bullet2.GetComponent<Rigidbody2D>().AddRelativeForce(bullet.transform.right * fireImpulse, ForceMode2D.Impulse);
                Destroy(bullet2, 2);
            }
            if (guns == 3)
            {
                StartCoroutine(FireCD(fireRate));
                GameObject bullet2 = Instantiate(shotPref, fireTransformDown.position, fireTransformDown.rotation);
                bullet2.GetComponent<Rigidbody2D>().AddRelativeForce(bullet.transform.right * fireImpulse, ForceMode2D.Impulse);
                Destroy(bullet2, 2);
            }

        }
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        rb.velocity = movement * speed;
    }
    public IEnumerator FireCD(float fireRate)
    {
        canShot = false;
        yield return new WaitForSeconds(1.0f / fireRate);
        canShot = true;
    }
}

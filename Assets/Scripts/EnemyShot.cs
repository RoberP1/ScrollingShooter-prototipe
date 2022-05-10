using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private float fireImpulse;
    [SerializeField] private Transform fireTransform;
    public float fireRate;
    private bool canShot;

   void Start()
    {
        canShot = true;
    }

    // Update is called once per frame
    void Update()
    {

            if (transform.position.x < 9 && canShot)
        {
            StartCoroutine(FireCD(fireRate));
            GameObject bullet = Instantiate(bulletPref, fireTransform.position, fireTransform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(-bullet.transform.right * fireImpulse, ForceMode2D.Impulse);
            Destroy(bullet, 4);
            //audio.clip = fireSound;
            //audio.Play();
        }
    }
    public IEnumerator FireCD(float fireRate)
    {
        canShot = false;
        yield return new WaitForSeconds(1.0f / fireRate);
        canShot = true;
    }
}

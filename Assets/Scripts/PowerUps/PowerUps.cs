using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    [SerializeField] protected AudioSource audio;
    protected Player player;
    public virtual void effect()
    {
        player = FindObjectOfType<Player>();
        audio.Play();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            effect();
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 5);
        }
    }
}

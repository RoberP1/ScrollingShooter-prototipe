using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnim : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    private void Start()
    {
        audio.Play();
    }
    public void ExplosionDone()
    {
        Destroy(gameObject);
    }
}

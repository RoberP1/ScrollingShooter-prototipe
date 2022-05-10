using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawner;
    [SerializeField] private GameObject SpawnPref1;
    [SerializeField] private GameObject SpawnPref2;
    [SerializeField] private GameObject SpawnPref3;
    [SerializeField] private GameObject SpawnPref4;
    [SerializeField] private GameObject SpawnPref5;
    public float delay;
    private bool canspawn;
    
    void Start()
    {
        StartCoroutine(SpawnCD(delay));
    }

    // Update is called once per frame
    void Update()
    {
        if (canspawn)
        {
            Spawn();
            StartCoroutine(SpawnCD(delay));
        }
    }

    public void Spawn()
    {
        int r = Random.Range(1, 101);
        if (r<=25) Instantiate(SpawnPref1, spawner.position, spawner.rotation);
        else if (r<=50) Instantiate(SpawnPref2, spawner.position, spawner.rotation);
        else if (r<=70) Instantiate(SpawnPref3, spawner.position, spawner.rotation);
        else if (r<=90) Instantiate(SpawnPref4, spawner.position, spawner.rotation);
        else Instantiate(SpawnPref5, spawner.position, spawner.rotation);
    }
    public IEnumerator SpawnCD(float delay)
    {
        canspawn = false;
        yield return new WaitForSeconds(delay);
        canspawn = true;
        if (this.delay > 1)
        {
            this.delay -= 0.05f;
        }
    }
}

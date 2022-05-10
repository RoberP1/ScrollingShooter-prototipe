using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;

    private Vector3 startPosition;
    [SerializeField]private float tileSize;
    void Start()
    {
        startPosition = transform.position;
    }

    
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time *scrollSpeed,tileSize);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}

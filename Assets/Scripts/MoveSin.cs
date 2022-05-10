using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour
{
    float sinCenterY;
    [Range(0f,3f)]
    [SerializeField] private float amplitude;

    [Range(0f, 3f)]
    [SerializeField] private float frecuency;

    [SerializeField] private bool inverted;

    void Start()
    {
        sinCenterY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x * frecuency) * amplitude;
        if (inverted) sin *= -1;
        pos.y = sinCenterY + sin;

        transform.position = pos;
    }
}

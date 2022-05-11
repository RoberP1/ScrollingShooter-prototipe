using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : PowerUps
{
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = !spriteRenderer.enabled;
    }
    public override void effect()
    {
        spriteRenderer = null;
        base.effect();
        player.shildActive = true;
        player.shild.SetActive(true);
        
    }
}

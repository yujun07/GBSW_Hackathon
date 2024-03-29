using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hited : MonoBehaviour
{
    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    public void OnHit()
    {
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);
    }
}

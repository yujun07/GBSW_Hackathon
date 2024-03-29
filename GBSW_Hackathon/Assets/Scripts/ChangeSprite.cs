using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private int currentSpriteIndex = 0;
    public Sprite[] sprites;
    
    public void ChangeSprites()
    {
        StartCoroutine(ChangeSpriteas());
    }
    public IEnumerator ChangeSpriteas()
    {
        currentSpriteIndex++;
        spriteRenderer.sprite = sprites[currentSpriteIndex];
        yield return new WaitForSeconds(0.2f);
        currentSpriteIndex++;
        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }
}

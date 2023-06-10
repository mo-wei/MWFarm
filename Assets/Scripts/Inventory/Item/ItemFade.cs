using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemFade : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// »Ö¸´²»Í¸Ã÷
    /// </summary>
    public void FadeIn()
    {
        Color spriteColor = new Vector4(1, 1, 1, 1);

        spriteRenderer.DOColor(spriteColor, Settings.itemFadeDuration);
    }
    /// <summary>
    /// Í¼Æ¬Í¸Ã÷µ­³ö
    /// </summary>
    public void FadeOut()
    {
        Color spriteColor = new Vector4(1, 1, 1, Settings.itemFadeAlpha);

        spriteRenderer.DOColor(spriteColor, Settings.itemFadeDuration);

    }

}

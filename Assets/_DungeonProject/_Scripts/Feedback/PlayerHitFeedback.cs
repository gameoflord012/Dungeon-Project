using System.Collections;
using UnityEngine;

public class PlayerHitFeedback : Feedback
{
    [SerializeField] private Shader flashShader;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 1f;
    [SerializeField] private int flashFrequency = 10;

    private Shader originalShader;

    public override void CreateFeedback()
    {
        originalShader = spriteRenderer.material.shader;
        spriteRenderer.material.shader = flashShader;
        StartCoroutine(FlashRoutine());
    }

    public override void ResetFeedback()
    {
        spriteRenderer.material.shader = originalShader;
        StopAllCoroutines();
        spriteRenderer.material.SetFloat("_TurnFlash", 0);
    }

    public IEnumerator FlashRoutine()
    {
        for(int i = 0; i < flashFrequency; i++)
        {
            spriteRenderer.material.SetFloat("_TurnFlash", 1);
            yield return new WaitForSeconds(flashDuration / 2f / flashFrequency);
            spriteRenderer.material.SetFloat("_TurnFlash", 0);
            yield return new WaitForSeconds(flashDuration / 2f / flashFrequency);
        }
    }
}
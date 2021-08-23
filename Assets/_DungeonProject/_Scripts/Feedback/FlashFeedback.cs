using System.Collections;
using UnityEngine;

public class FlashFeedback : Feedback
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
        if (spriteRenderer.material.HasProperty("_TurnFlash"))
            spriteRenderer.material.SetFloat("_TurnFlash", 0);

        if (originalShader)
            spriteRenderer.material.shader = originalShader;

        originalShader = null;
        StopAllCoroutines();
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

        ResetFeedback();
    }
}
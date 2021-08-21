using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyDeathFeedback : Feedback
{
    [SerializeField] Shader dissolveShader;
    [SerializeField] SpriteRenderer dissolveTarget;

    [SerializeField] float dissolveDuration = .5f;

    Shader originalShader;

    public override void CreateFeedback()
    {
        originalShader = dissolveTarget.material.shader;
        dissolveTarget.material.shader = dissolveShader;

        dissolveTarget.material.DOFloat(0, "_Step", dissolveDuration);
    }

    public override void ResetFeedback()
    {
        dissolveTarget.material.shader = originalShader;
        dissolveTarget.material.DOComplete();
        dissolveTarget.material.SetFloat("_Step", 0);
    }
}
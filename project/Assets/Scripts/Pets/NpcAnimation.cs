
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;


public class NpcAnimation : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float movingScaleFactor = 0.9f;
    public float movingDuration = 0.5f;
    public float idleScaleFactor = 0.9f;
    public float idleDuration = 1f;

    private Tween walkTween;
    private Tween idleTween;

    private bool isAnimWalk;
    private bool isAnimIdle;
    private Vector3 originalScale;


    private void OnDestroy()
    {
        walkTween?.Kill();
        idleTween?.Kill();
    }

    void Start()
    {
        originalScale = transform.localScale;
        walkTween = transform.DOScaleY(originalScale.y * idleScaleFactor, movingDuration)
        .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo).OnUpdate(() =>
            {
            });
    }
}

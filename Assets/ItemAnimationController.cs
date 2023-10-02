using DG.Tweening;
using UnityEngine;

public class ItemAnimationController : MonoBehaviour
{
    public static ItemAnimationController Instance;
    public void Awake()
    {
        if(Instance != null) Instance = this;
        else Destroy(gameObject);
    }
    public void StartScaleAnimation(Vector3 targetScale, float duration, GameObject item)
    {
        item.transform.DOScale(targetScale, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                Debug.Log("AnimationCompleted");
            });
    }
    public void StartHoverAnimation(GameObject item,Vector3 initialPosition)
    {
        item.transform.DOMoveY(initialPosition.y + 0.2f, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                item.transform.DOMoveY(initialPosition.y, 1f)
                .SetEase(Ease.Linear)
                .OnComplete(() => StartHoverAnimation(item, initialPosition));
            });
    }
    public void StopHoverAnimation(GameObject item, Vector3 initialPosition)
    {
        DOTween.Kill(item.transform);
        if (item.transform.position == initialPosition) return;
        item.transform.DOMove(initialPosition, 1f).SetEase(Ease.Linear);
    }
}

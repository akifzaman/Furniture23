using DG.Tweening;
using UnityEngine;

public class ItemAnimationManager : MonoBehaviour
{
    public static ItemAnimationManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    #endregion
    public void StartScaleAnimation(Vector3 targetScale, float duration, GameObject item)
    {
        item?.transform.DOScale(targetScale, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                Debug.Log("AnimationCompleted");
            });
    }
    public void StartHoverAnimation(Vector3 initialPosition, GameObject item)
    {
        item?.transform.DOMoveY(initialPosition.y + 0.2f, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                item.transform.DOMoveY(initialPosition.y, 1f)
                .SetEase(Ease.Linear)
                .OnComplete(() => StartHoverAnimation(initialPosition, item));
            });
    }
    public void StopHoverAnimation(Vector3 initialPosition, GameObject item)
    {
        DOTween.Kill(item?.transform);
        if (item?.transform.position == initialPosition) return;
        item?.transform.DOMove(initialPosition, 1f).SetEase(Ease.Linear);
    }
}

using UnityEngine;
using DG.Tweening;
public class ItemController : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] private Vector3 targetScale;
    public void Initialize()
    {
        StartScaleAnimation(targetScale, 1, prefab);
    }
    private void StartScaleAnimation(Vector3 targetScale, float duration, GameObject item)
    {
        item.transform.DOScale(targetScale, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                Debug.Log("AnimationCompleted");
            });
    }
}

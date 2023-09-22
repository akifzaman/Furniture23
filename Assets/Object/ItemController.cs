using UnityEngine;
using DG.Tweening;
public class ItemController : MonoBehaviour
{
    public GameObject prefab;
    private Vector3 targetScale = new Vector3(0.2f,0.2f,0.2f);
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

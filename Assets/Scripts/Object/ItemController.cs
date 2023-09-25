using UnityEngine;
using DG.Tweening;
using cakeslice;

public class ItemController : MonoBehaviour
{
    public GameObject prefab;
    public float scaleDuration;
    [SerializeField] private Vector3 targetScale;
    public Vector3 initialPosition;
    public void Initialize()
    {
        StartScaleAnimation(targetScale, scaleDuration, prefab);
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("Furniture"))
                    {
                        prefab.GetComponentInChildren<Outline>().color = 2;
                        OnItemSelect();
                    }
                }
                else
                {
                    prefab.GetComponentInChildren<Outline>().color = 0;
                    OnItemDeselect();
                }
            }
        }
    }

    private void StartScaleAnimation(Vector3 targetScale, float duration, GameObject item)
    {
        item.transform.DOScale(targetScale, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                Debug.Log("AnimationCompleted");
            });
    }
    private void OnItemSelect()
    {
        var upperPoint = new Vector3(initialPosition.x, initialPosition.y + 0.2f, initialPosition.z);
        prefab.transform.DOMove(upperPoint, 1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                prefab.transform.DOMove(initialPosition, 1f)
                .SetEase(Ease.Linear)
                .OnComplete(OnItemSelect);
            });
    }
    private void OnItemDeselect()
    {
        DOTween.Kill(prefab.transform);
        if (prefab.transform.position == initialPosition) return;
        prefab.transform.DOMove(initialPosition, 1f).SetEase(Ease.Linear);       
    }
}

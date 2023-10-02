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
        prefab = ApplicationManager.instance.SelectedItem.Prefab;
        ApplicationManager.instance.SelectedObject = prefab;
        prefab.GetComponentInChildren<Outline>().color = 2;
        ItemAnimationController.Instance.StartScaleAnimation(targetScale, scaleDuration, prefab);
        OnItemSelect();
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
                    if (raycastHit.collider.CompareTag("Furniture") && ApplicationManager.instance.SelectedObject == null)
                    {
                        ApplicationManager.instance.SelectedObject = prefab;
                        prefab.GetComponentInChildren<Outline>().color = 2;
                        OnItemSelect();
                    }
                    else if (raycastHit.collider.CompareTag("Furniture") && ApplicationManager.instance.SelectedObject != null)
                    {
                        ApplicationManager.instance.SelectedObject = null;
                        prefab.GetComponentInChildren<Outline>().color = 0;
                        OnItemDeselect();
                    }
                }       
            }
        }
    }

    //private void StartScaleAnimation(Vector3 targetScale, float duration, GameObject item)
    //{
    //    item.transform.DOScale(targetScale, duration)
    //        .SetEase(Ease.Linear)
    //        .OnComplete(() => {
    //            Debug.Log("AnimationCompleted");
    //        });
    //}
    private void OnItemSelect()
    {
        UIManager.instance.ShowTexturePanel();       
        UIManager.instance.InstantiateTextureButtons(ApplicationManager.instance.SelectedItem);
        ItemAnimationController.Instance.StartHoverAnimation(prefab, initialPosition);
    }
    private void OnItemDeselect()
    {
        UIManager.instance.ShowItemPanel();
        ItemAnimationController.Instance.StopHoverAnimation(prefab, initialPosition);
        //DOTween.Kill(prefab.transform);
        //if (prefab.transform.position == initialPosition) return;
        //prefab.transform.DOMove(initialPosition, 1f).SetEase(Ease.Linear);       
    }
}

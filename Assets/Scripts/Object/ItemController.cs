using UnityEngine;
using DG.Tweening;
using cakeslice;

public class ItemController : MonoBehaviour
{
    public float scaleDuration;
    [SerializeField] private Vector3 targetScale;
    public Vector3 initialPosition;
    public void Initialize(GameObject go)
    {
        GetComponentInChildren<Outline>().color = 2;
        ItemAnimationController.instance.StartScaleAnimation(targetScale, scaleDuration, go);
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
                        //ApplicationManager.instance.SelectedObject = prefab;
                        GetComponentInChildren<Outline>().color = 2;
                        OnItemSelect();
                    }
                    else if (raycastHit.collider.CompareTag("Furniture") && ApplicationManager.instance.SelectedObject != null)
                    {
                        //ApplicationManager.instance.SelectedObject = null;
                        GetComponentInChildren<Outline>().color = 0;
                        OnItemDeselect();
                    }
                }
            }
        }
    }
    private void OnItemSelect()
    {
        UIManager.instance.ShowTexturePanel();       
        UIManager.instance.InstantiateTextureButtons(ApplicationManager.instance.SelectedItem);
        ItemAnimationController.instance.StartHoverAnimation(ApplicationManager.instance.SelectedObject, initialPosition);
    }
    private void OnItemDeselect()
    {
        UIManager.instance.ShowItemPanel();
        ItemAnimationController.instance.StopHoverAnimation(ApplicationManager.instance.SelectedObject, initialPosition);     
    }
}

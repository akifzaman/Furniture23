using UnityEngine;
using cakeslice;

public class ItemController : MonoBehaviour
{
    public float scaleDuration;
    [SerializeField] private Vector3 targetScale;
    public Vector3 initialPosition;
    public void Initialize()
    {
        OnItemSelect();
        ItemAnimationController.instance.StartScaleAnimation(targetScale, scaleDuration, gameObject);
    }
    public void OnItemSelect()
    {
        GetComponentInChildren<Outline>().color = 2;
        UIManager.instance.ShowTexturePanel();       
        UIManager.instance.InstantiateTextureButtons(ApplicationManager.instance.SelectedItem);
        ItemAnimationController.instance.StartHoverAnimation(initialPosition, gameObject);
    }
    public void OnItemDeselect()
    {
        GetComponentInChildren<Outline>().color = 0;
        UIManager.instance.ShowItemPanel();
        ItemAnimationController.instance.StopHoverAnimation(initialPosition, gameObject);
    }
}

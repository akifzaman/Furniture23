using UnityEngine;
using cakeslice;

public class ItemController : MonoBehaviour
{
    private Item item;
    public float scaleDuration;
    [SerializeField] private Vector3 targetScale;
    public Vector3 initialPosition;
    public void Initialize(Item item)
    {
        this.item = item;
        OnItemSelect();
        ItemAnimationManager.instance.StartScaleAnimation(targetScale, scaleDuration, gameObject);
    }
    public void OnItemSelect()
    {
        GetComponentInChildren<Outline>().color = 2;
        UIManager.instance.ShowTexturePanel();       
        UIManager.instance.InstantiateTextureButtons(item);
        ItemAnimationManager.instance.StartHoverAnimation(initialPosition, gameObject);
    }
    public void OnItemDeselect()
    {
        GetComponentInChildren<Outline>().color = 0;
        UIManager.instance.ShowItemPanel();
        ItemAnimationManager.instance.StopHoverAnimation(initialPosition, gameObject);
    }
}

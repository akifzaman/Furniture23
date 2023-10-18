using UnityEngine;
using cakeslice;

public class ItemController : MonoBehaviour
{
    public Item item;
    public float scaleDuration;
    [SerializeField] private Vector3 targetScale;
    public Vector3 initialPosition; 
    private Vector3 translationVector;
    private float speedModifier = 0.0020f;
    public bool isModified;
    private float initialDistance;
    private float currentDistance;
    private float rotationAngle = 5f;
    public void Initialize(Item item)
    {
        this.item = item;
        OnItemSelect();
        ItemAnimationManager.instance.StartScaleAnimation(targetScale, scaleDuration, gameObject);
    }
    public void MoveItem(Touch touch)
    {
        translationVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        gameObject.transform.Translate(translationVector * Input.GetTouch(0).deltaPosition.y * speedModifier, Space.World);

        translationVector = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
        gameObject.transform.Translate(translationVector * Input.GetTouch(0).deltaPosition.x * speedModifier, Space.World);
    }
    public void ScaleItem(Touch touch1, Touch touch2)
    {
        if (isModified == false)
        {
            isModified = true;
            initialDistance = Vector2.Distance(touch1.position, touch2.position);
        }
        if (isModified)
        {
            currentDistance = Vector2.Distance(touch1.position, touch2.position);
            if (currentDistance > initialDistance)
                gameObject.transform.localScale *= 1.02f;
            else
                gameObject.transform.localScale /= 1.02f;
        }
    }
    public void RotateItem(Touch touch1, Touch touch2)
    {
        if (touch2.deltaPosition.x < 0)
        {
            gameObject.transform.Rotate(Vector3.up, rotationAngle, Space.World);
        }
        else if (touch2.deltaPosition.x > 0)
        {
            gameObject.transform.Rotate(Vector3.up, -rotationAngle, Space.World);
        }
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

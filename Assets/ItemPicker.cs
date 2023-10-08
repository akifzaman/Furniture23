using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public static ItemPicker instance;

    public GameObject currentGameObject;
    public GameObject previousGameObject;
    private float speedModifier = 0.0020f;
    private Vector3 translationVector;
    private float rotationAngle = 5f;
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
    private void Update()
    {
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("Furniture") && currentGameObject == null)
                    {
                        currentGameObject = raycastHit.collider.gameObject;
                        currentGameObject.GetComponent<ItemController>().OnItemSelect();
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && currentGameObject != null)
            {
                // Convert X-Y touch movement to object translation in world space
                translationVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
                currentGameObject.transform.Translate(translationVector * Input.GetTouch(0).deltaPosition.y * speedModifier, Space.World);

                translationVector = new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
                currentGameObject.transform.Translate(translationVector * Input.GetTouch(0).deltaPosition.x * speedModifier, Space.World);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);    
            if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                
                if (touch1.deltaPosition.x < 0 && touch2.deltaPosition.x < 0)
                {
                    currentGameObject.transform.Rotate(Vector3.up, rotationAngle, Space.World);
                }
                else if (touch1.deltaPosition.x > 0 && touch2.deltaPosition.x > 0)
                {
                    currentGameObject.transform.Rotate(Vector3.up, -rotationAngle, Space.World);
                }
            }
        }
    }
    public void DeselectItem()
    {
            currentGameObject.GetComponent<ItemController>().OnItemDeselect();
            currentGameObject = null;
    }
}
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public static ItemPicker instance;

    public GameObject currentGameObject;
    public GameObject previousGameObject;
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
        if (Input.touchCount > 0)
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
                    else if (raycastHit.collider.CompareTag("Furniture") && currentGameObject != null)
                    {
                        currentGameObject.GetComponent<ItemController>().OnItemDeselect();
                        previousGameObject = currentGameObject;
                        currentGameObject = raycastHit.collider.gameObject;
                        if (currentGameObject == previousGameObject)
                        {
                            currentGameObject = null;
                            return;
                        }
                        currentGameObject.GetComponent<ItemController>().OnItemSelect();
                    }
                }
            }
        }
    }
}
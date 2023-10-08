using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager instance;
    public List<Item> Items = new List<Item>();
    public Item SelectedItem;
    #region Singleton
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    #endregion
    public void DeselectItem()
    {
        TouchManager.instance.currentGameObject.GetComponent<ItemController>().OnItemDeselect();
        TouchManager.instance.currentGameObject = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform ItemButtonPanel;
    public ItemUIController ItemButtonPrefab;
    public void Start()
    {
        InstantiateItemButtons();
    }

    public void InstantiateItemButtons()
    {
        foreach (var item in ApplicationManager.instance.Items)
        {
            var btn = Instantiate(ItemButtonPrefab, ItemButtonPanel);
            btn.Initialize(item);
        }
    }
}

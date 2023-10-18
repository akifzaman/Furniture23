using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public RectTransform ItemButtonsPanel;
    public RectTransform TextureButtonsPanel;
    public Transform ItemButtonsContainer;
    public Transform TextureButtonsContainer;
    public Transform CategoryButtonsContainer;
    public ScrollViewButtonController ItemButtonPrefab;
    public ScrollViewButtonController TextureButtonPrefab;
    public GameObject CategoryButtonPrefab;
    public Vector2 DisplayPosition;
    public Vector2 HidePosition;
    public RectTransform CategoryPanel;
    public Image PanelTogglerIcon;
    public float distance;
    public float tempValue;
    public Button DoneButton;
    public Button RemoveButton;
    public HashSet<ItemType> UniqueItemTypes = new HashSet<ItemType>();
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
    public void Start()
    {
        tempValue = distance;
        IdentifyUniqueItems();
        InstantiateItemButtons();
        DisplayPosition = ItemButtonsPanel.anchoredPosition;
        HidePosition = TextureButtonsPanel.anchoredPosition;
    }

    public void InstantiateItemButtons()
    {
        foreach (var item in ApplicationManager.instance.Items)
        {
            var btn = Instantiate(ItemButtonPrefab, ItemButtonsContainer);
            btn.Initialize(item);
        }
    }
    public void InstantiateTextureButtons(Item item)
    {
        CleanPanel(TextureButtonsContainer);
        for (int i = 0; i < item.AvailableTextures.Count; i++)
        {
            var btn = Instantiate(TextureButtonPrefab, TextureButtonsContainer);
            btn.Initialize(i, item.AvailableTextures[i]);
        }
    }
    public void IdentifyUniqueItems()
    {
        foreach (var item in ApplicationManager.instance.Items)
        {
            UniqueItemTypes.Add(item.Type);
        }
        InstantiateCategoryButtons();
    }
    public void InstantiateCategoryButtons()
    {
        foreach (var item in UniqueItemTypes)
        {
            var btn = Instantiate(CategoryButtonPrefab, CategoryButtonsContainer);
            string itemType = item.ToString();
            btn.GetComponentInChildren<TextMeshProUGUI>().text = itemType;
            btn.GetComponentInChildren<Button>().onClick.AddListener(() => FilterByItemType(itemType));
        }
    }
    public void CleanPanel(Transform panel)
    {
        foreach (Transform element in panel)
        {
            Destroy(element.gameObject);
        }
    }
    public void ShowTexturePanel()
    {
        ItemButtonsPanel.anchoredPosition = HidePosition;
        ItemButtonsPanel.gameObject.SetActive(false);
        TextureButtonsPanel.gameObject.SetActive(true);
        TextureButtonsPanel.DOAnchorPosY(DisplayPosition.y, 1f);
        DoneButton.transform.DOScale(1, 0.5f).SetEase(Ease.InSine);
        RemoveButton.transform.DOScale(1, 0.5f).SetEase(Ease.InSine);
    }
    public void ShowItemPanel()
    {
        TextureButtonsPanel.anchoredPosition = HidePosition;
        TextureButtonsPanel.gameObject.SetActive(false);
        ItemButtonsPanel.gameObject.SetActive(true);
        ItemButtonsPanel.DOAnchorPosY(DisplayPosition.y, 1f);
        DoneButton.transform.DOScale(0, 0.5f).SetEase(Ease.InSine);
        RemoveButton.transform.DOScale(0, 0.5f).SetEase(Ease.InSine);
    }

    public void TogglePanelVisibility()
    {
        distance = CategoryPanel.transform.position.x < 0 ? tempValue : -tempValue;
        CategoryPanel.DOAnchorPosX(CategoryPanel.transform.position.x + distance, 0.10f).SetEase(Ease.Linear); //replace the hardcoded values
        PanelTogglerIcon.transform.DOScaleX(PanelTogglerIcon.transform.localScale.x * -1, 0.10f);      
    }
    public void FilterByItemType(string Type)
    {
        Enum.TryParse(Type, false, out ItemType itemType);
        foreach (Transform item in ItemButtonsContainer)
        {
            item.gameObject.SetActive(true);
        }
        if (itemType == ItemType.All) return;
        foreach (Transform item in ItemButtonsContainer)
        {
            if (item.GetComponent<ScrollViewButtonController>().item.Type != itemType)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}

using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public RectTransform ItemButtonsPanel;
    public RectTransform TextureButtonsPanel;
    public Transform ItemButtonsContainer;
    public Transform TextureButtonsContainer;
    public ScrollViewButtonController ItemButtonPrefab;
    public ScrollViewButtonController TextureButtonPrefab;
    public Vector2 DisplayPosition;
    public Vector2 HidePosition;
    public RectTransform Dropdown;
    public Image DropdownTogglerIcon;
    public Vector2 DropdownInitialPosition;
    public float distance;
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
        InstantiateItemButtons();
        DisplayPosition = ItemButtonsPanel.anchoredPosition;
        HidePosition = TextureButtonsPanel.anchoredPosition;
        DropdownInitialPosition = Dropdown.anchoredPosition;
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
        foreach (var texture in item.AvailableTextures)
        {
            var btn = Instantiate(TextureButtonPrefab, TextureButtonsContainer);
            btn.Initialize(texture);
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
    }
    public void ShowItemPanel()
    {
        TextureButtonsPanel.anchoredPosition = HidePosition;
        TextureButtonsPanel.gameObject.SetActive(false);
        ItemButtonsPanel.gameObject.SetActive(true);
        ItemButtonsPanel.DOAnchorPosY(DisplayPosition.y, 1f);
    }

    public void ToggleDropdownVisibility()
    {
        distance = Dropdown.gameObject.activeInHierarchy ? -distance : distance;
        Dropdown.gameObject.SetActive(true);
        Dropdown.DOAnchorPosX(DropdownInitialPosition.x + distance, 0.25f).SetEase(Ease.Linear); //replace the hardcoded values
        DropdownTogglerIcon.transform.DOScaleX(DropdownTogglerIcon.transform.localScale.x * -1, 0.10f);      
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

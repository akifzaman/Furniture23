using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Create New Item")]
public class Item: ScriptableObject
{
    public GameObject Prefab;
    public Sprite ItemIcon;
    //public Color ItemColor;
    public string Name;
    public float Price;
    public ItemType Type;
    public int Index;
}
public enum ItemType
{
    All, Chair, Table, Sofa
}
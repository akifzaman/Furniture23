using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Create New Item")]
public class Item: ScriptableObject
{
    public GameObject Prefab;
    public Sprite ItemIcon;
    //public Color ItemColor;
    public string Name;
    public float Price;
    public ItemType Type;
    public List<Sprite> AvailableTextures;
    public List<Material> Materials;
    public Dictionary<Sprite, Material> SpriteToMaterial = new Dictionary<Sprite, Material>();
    public int Index;
}
public enum ItemType
{
    All, Chair, Table, Sofa
}
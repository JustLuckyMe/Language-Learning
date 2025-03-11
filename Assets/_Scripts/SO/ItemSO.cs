using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Question System/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public AudioClip AudioClip;
}

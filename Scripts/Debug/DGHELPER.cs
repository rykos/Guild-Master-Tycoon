using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DGHELPER : MonoBehaviour
{

}

[CustomEditor(typeof(DGHELPER))]
public class DGHELPEREDITOR : Editor
{
    SerializedProperty itemType;
    public override void OnInspectorGUI()
    {

        // if (GUILayout.Button("Add new item"))
        // {
        //     ItemType itemType = (ItemType)Random.Range(1, System.Enum.GetNames(typeof(ItemType)).Length);
        //     Item item = new Item("Embeded Something", itemType, new Stats(10, 5, 15), 10, 15)
        //     {
        //         Image = AssetManager.Instance.RandomIcon(itemType)
        //     };
        //     PlayerManager.Instance.PlayerModel.AddItemToBag(item);
        // }
    }
}
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data Object", menuName = "Inventory/Data Object", order = 1)]
public class MemData : ScriptableObject {

    public new string name;
    public int id;
    public Sprite icon;

    public RoomController associatedRoom;

}

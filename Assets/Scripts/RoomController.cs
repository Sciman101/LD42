using System.Collections;
using UnityEngine;

public class RoomController : MonoBehaviour {

    public MemData roomData;

    PlayerController player;

    public GameObject[] adjacentDoors;
    MeshRenderer[] children;
    bool visible;

    Material roomMat;
    float fade = 0;

	void Start () {

        MemData dat = ScriptableObject.CreateInstance<MemData>();
        dat.name = roomData.name;
        dat.id = roomData.id;
        dat.icon = roomData.icon;

        roomData = dat;
        roomData.associatedRoom = this;

        int t = transform.childCount;
        children = new MeshRenderer[t + adjacentDoors.Length];
        for (int i=0;i<children.Length;i++)
        {
            if (i < t)
            {
                children[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
            }
            else
            {
                children[i] = adjacentDoors[i - t].GetComponentInChildren<MeshRenderer>();
            }
        }

        roomMat = GetComponentsInChildren<MeshRenderer>()[0].material;

        SetVisible(false);
	}

    private void Update()
    {
        if (fade > 0 && visible) {
            fade = Mathf.MoveTowards(fade,0, Time.deltaTime * 2.5f);
            roomMat.SetFloat("_DissolveAmount", fade);
        }
    }

    public void SetVisible(bool visible)
    {
        this.visible = visible;
        for (int i=0;i<children.Length;i++)
        {
            children[i].enabled = visible;
        }
        if (!visible) fade = 1;
    }

    public void Hide()
    {
        SetVisible(false);
    }

}

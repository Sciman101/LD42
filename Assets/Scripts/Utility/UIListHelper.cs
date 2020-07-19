using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIListHelper : MonoBehaviour {

    public Image[] icons;
    public Text[] labels;

	void Start () {

        //Initialize arrays
        icons = new Image[transform.childCount-2];
        labels = new Text[transform.childCount-2];

        //Populate arrays
        for (int i=2;i<transform.childCount;i++)
        {
            labels[i-2] = transform.GetChild(i).GetComponentInChildren<Text>();
            icons[i-2] = transform.GetChild(i).GetComponentsInChildren<Image>()[1];
        }

	}

    public void SetSlot(MemData m, int i)
    {
        if (m != null)
        {
            icons[i].enabled = true;
            icons[i].sprite = m.icon;
            labels[i].text = m.name;
        }
        else
        {
            icons[i].enabled = false;
            labels[i].text = "";
        }
    }
	
}

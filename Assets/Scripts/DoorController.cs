using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public MemData keyCode;

    GameObject doorModel;
    Vector3 startPos;
    bool open;
    float yPos;

	void Start () {
        doorModel = transform.GetChild(0).gameObject;
        startPos = doorModel.transform.position;
    }

    private void Update()
    {
        yPos = Mathf.Lerp(yPos,open ? -4 : 0,Time.deltaTime * 10);
        doorModel.transform.position = startPos + Vector3.up * yPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (keyCode == null || other.GetComponent<MemoryController>().MemoryContains(keyCode))
            {
                open = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            open = false;
        }
    }
}

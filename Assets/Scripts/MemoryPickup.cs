using System.Collections;
using UnityEngine;

public class MemoryPickup : MonoBehaviour {

    public float rotateSpeed;
    public MemData data;

    // Update is called once per frame
    void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
	}

    public void Hide()
    {
        transform.position += Vector3.up * 100;
        Invoke("Show", 5);
    }

    void Show()
    {
        transform.position -= Vector3.up * 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelRegionController : MonoBehaviour {

    Material mat;
    public float duration;
    float time;

	// Find material
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        time = duration;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        float size = 1.001f - (time / duration);
        mat.SetVector("_CellSize", Vector4.one * size);
        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}

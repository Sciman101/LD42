using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CustomImageEffect : MonoBehaviour {

    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);   
    }
}

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Effects/PostProcessing")]
public class ImageEffect : MonoBehaviour
{
    private Shader effectShader;

    [SerializeField]
    private Material effectMaterial;

    //[ImageEffectOpaque]
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if(!effectMaterial)
        {
            Graphics.Blit(src, dst);
            return;
        }

        RenderTexture rt = RenderTexture.GetTemporary(src.width, src.height);
        
        Graphics.Blit(src, rt, effectMaterial, 0);
        Graphics.Blit(rt, dst, effectMaterial, 1);
    }
}

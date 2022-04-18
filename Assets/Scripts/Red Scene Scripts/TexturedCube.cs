using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TexturedCube : MonoBehaviour
{
    [SerializeField] private MeshRenderer redCubeRenderer;

    Vector3 enlargedCubeSize = new Vector3(2f, 2f, 2f);
    Vector3 normalCubeSize = Vector3.one;

    Vector2 textureOffset = new Vector2(-1.5f, 0f);

    float duration = 1f;

    // Update is called once per frame
    void Update()
    {
        ResizingCube();
        MaterialXOffset();
    }

    void ResizingCube()
    {
        // Resizes cube to be enlarged when the cube is in normal size.
        if (transform.localScale == normalCubeSize)
        {
            transform.DOScale(enlargedCubeSize, duration).SetEase(Ease.InOutExpo);
        }
        // Resizes cube to be normal when the cube is enlarged.
        else if (transform.localScale == enlargedCubeSize)
        {
            transform.DOScale(normalCubeSize, duration).SetEase(Ease.InOutExpo);
        }
    }

    // Move the texture of the cube on the X axis.
    void MaterialXOffset()
    {
        redCubeRenderer.material.SetTextureOffset("_MainTex", textureOffset * Time.time);
    }
}

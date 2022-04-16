using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TexturedCube : MonoBehaviour
{
    //[SerializeField] private Transform redCubeTransform;
    [SerializeField] private Vector3 cubeRotation;
    [Space]
    [SerializeField] private MeshRenderer redCubeRenderer;

    Vector3 enlargedCubeSize = new Vector3(2f, 2f, 2f);
    Vector3 normalCubeSize = Vector3.one;

    Vector2 textureOffset = new Vector2(-1.5f, 0f);

    float duration = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //RotateYAxis();
        ResizingCube();
        MaterialXOffset();
    }

    void RotateYAxis()
    {
        transform.Rotate(cubeRotation * Time.deltaTime);
    }

    void ResizingCube()
    {
        if (transform.localScale == normalCubeSize)
        {
            transform.DOScale(enlargedCubeSize, duration).SetEase(Ease.InOutExpo);
        }
        else if (transform.localScale == enlargedCubeSize)
        {
            transform.DOScale(normalCubeSize, duration).SetEase(Ease.InOutExpo);
        }
    }

    void MaterialXOffset()
    {
        redCubeRenderer.material.SetTextureOffset("_MainTex", textureOffset * Time.time);
    }
}

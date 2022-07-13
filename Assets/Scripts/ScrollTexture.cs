using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{   
    public float scrollSpeedY;
    public float scrollSpeedX;
    private MeshRenderer meshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset = new Vector2(
            Time.time * scrollSpeedX, 
            Time.time * scrollSpeedY
        );
    }
}

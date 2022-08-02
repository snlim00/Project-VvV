using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] public Material mat;
    public float scrollSpeed;

    public static BackgroundController S;

    private void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mat.mainTextureOffset = new Vector2(0, mat.mainTextureOffset.y + (scrollSpeed * Time.deltaTime));
    }
}

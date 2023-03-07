using UnityEngine;

public class cross_line : MonoBehaviour
{
    public RenderTexture test;
    //public Color temp;

    void Start()
    {
        // Start web cam feed
        Debug.Log("does something");


    }

    void Update()
    {

        //Debug.Log(toTexture2D(test).GetPixel(0,0));
        Color temp =  toTexture2D(test).GetPixel(0, 0);
        if (temp.r > 0.3 && temp.b <0.2 && temp.g<0.2 )
        {
            //Debug.Log("on red plane");
        }
        // Do processing of data here.
    }


    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
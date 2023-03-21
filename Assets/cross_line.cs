using UnityEngine;

public class cross_line : MonoBehaviour
{
    public RenderTexture test;
    //public Color temp;

    void Start()
    {

    }

    public string Update()
    {

        //Debug.Log(toTexture2D(test).GetPixel(0,0));
        Color temp =  toTexture2D(test).GetPixel(0, 0);
        if (temp.r > 0.5 && temp.b >0.5 && temp.g>0.5 )
        {
            Debug.Log("on white plane");
            return "Please stay in your lane.";
        }
        else
            Debug.Log("Not on white plane");
        return "";


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
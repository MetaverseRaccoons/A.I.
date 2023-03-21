using UnityEngine;

public class temp : MonoBehaviour
{
    public Camera camera;
    private Texture2D texture;
    public Color32 targetColor = Color.white;
    private int inputSizex;
    private int inputSizey;
    private int count;
    void Update()
    {
        inputSizex = 500;
        inputSizey = 500;

        var cameraTexture = RenderTexture.GetTemporary(camera.pixelWidth, camera.pixelHeight, 0);
        camera.targetTexture = cameraTexture;
        camera.Render();
        RenderTexture.active = cameraTexture;

        // Resize the image and convert it to a tensor
        //var texture = new Texture2D(inputSizex, inputSizey);
        texture = toTexture2D(cameraTexture);

        //inputTexture.ReadPixels(new Rect(0, 0, inputSize.x, inputSize.y), 0, 0);

        // Render the camera's view into a texture
        // RenderTexture renderTexture = camera.targetTexture;
        // RenderTexture.active = renderTexture;
        //Debug.Log(renderTexture.width);
        //Debug.Log(renderTexture.height);

        //texture = new Texture2D(renderTexture.width, renderTexture.height);
        //texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        // texture.Apply();

        // Loop through the pixels in the top right corner of the texture
        int x = texture.width - 1;
        int y = texture.height - 1;
        //int x = 70;
        //int y = 70;
        int xor = 0;
        bool tdbreak = false;
        for (x = texture.width - 1; x>0 && !tdbreak; x--)
        {
            for (y = texture.height-1; y > 0 && !tdbreak; y--)
            {
                Debug.Log(x + "," + y);

                Color pixelColor = texture.GetPixel(x, y);
                //Debug.Log(pixelColor.r);
                //Debug.Log(pixelColor.b);
                //Debug.Log(pixelColor.g);


                if (pixelColor.r > 0.9 && pixelColor.b > 0.9 && pixelColor.g > 0.9)
                {
                    Debug.Log("Found white pixel at position (" + x + ", " + y + ")");
                    tdbreak = true;
                    break;
                }
            } 
            
        }
        RenderTexture.ReleaseTemporary(cameraTexture);


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
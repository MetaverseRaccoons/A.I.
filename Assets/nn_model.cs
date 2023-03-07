using System;
using UnityEngine;
using Unity.Barracuda;

public class nn_model : MonoBehaviour
{
    public NNModel onnxAsset;
   //public Texture2D test;
    private IWorker worker;
   public RenderTexture test;

    void Start()
    {

        // Create a worker for executing the model
        worker = onnxAsset.CreateWorker();

        using (var input = new Tensor(test, channels: 3))
        {
            // execute neural network with specific input and get results back
            var output = worker.Execute(input).PeekOutput();

            // the following line will access values of the output tensor causing the main thread to block until neural network execution is done
            // probs = F.softmax(outputs).data.squeeze();
            //Get the class indices of top k probabilities.
            //class_idx = topk(probs, 1)[1];
            var indexWithHighestProbability = output[0];
            int count = 1;
            float max = 0;
            int index = 0;
            for (int i = 0; i < output.length; i++)
            {
                if (max < output[i])
                {
                    max = output[i];
                    index = i;
                }
                //UnityEngine.Debug.Log(i);

                //UnityEngine.Debug.Log(output[i]);

                count = count + 1;
            }

            //UnityEngine.Debug.Log($"Image was recognised as class number: " + output[0] + " " + output[1]);
            UnityEngine.Debug.Log($"Image was recognised as class number: " + index);

        }
    }



void Update()
{

        using (var input = new Tensor(toTexture2D(test), channels: 3))
        {
            // execute neural network with specific input and get results back
            var output = worker.Execute(input).PeekOutput();

            // the following line will access values of the output tensor causing the main thread to block until neural network execution is done
            // probs = F.softmax(outputs).data.squeeze();
            //Get the class indices of top k probabilities.
            //class_idx = topk(probs, 1)[1];
            var indexWithHighestProbability = output[0];
            int count = 1;
            float max = 0;
            int index = 0;
            for (int i = 0; i < output.length; i++)
            {
                if (max < output[i])
                {
                    max = output[i];
                    index = i;
                }
                //UnityEngine.Debug.Log(i);

                //UnityEngine.Debug.Log(output[i]);

                count = count + 1;
            }

            //UnityEngine.Debug.Log($"Image was recognised as class number: " + output[0] + " " + output[1]);
            UnityEngine.Debug.Log($"Image was recognised as class number: " + index);

        }
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(244, 244, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

}
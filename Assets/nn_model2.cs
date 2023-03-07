using UnityEngine;
using Unity.Barracuda;

public class nn_model2: MonoBehaviour
{
    // The camera to capture the image from
    public Camera camera;

    // The neural network model asset
    public NNModel modelAsset;

    // The name of the input tensor
    //public string inputName;

    // The name of the output tensor
    //public string outputName;

    // The size to resize the input image to
    public Vector2Int inputSize;

    // The output tensor shape
    private TensorShape outputShape;

    // The neural network model
    public Model model;

    // The worker for running inference
    private IWorker worker;

    private void Start()
    {
        worker = modelAsset.CreateWorker();

        // Load the neural network model from the asset
        //model = ModelLoader.Load(modelAsset);

        // Create the worker for running inference
        //worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpBurst, model);

        // Compute the output tensor shape
        //outputShape = new TensorShape(1, 1, model.outputs[outputName].shape[1], model.outputs[outputName].shape[0]);
        int index = 14;
        UnityEngine.Debug.Log($"Image was recognised as class number: " + index);

    }

    private void Update()
    {
        // Capture the camera image
        var cameraTexture = RenderTexture.GetTemporary(camera.pixelWidth, camera.pixelHeight, 0);
        camera.targetTexture = cameraTexture;
        camera.Render();
        RenderTexture.active = cameraTexture;

        // Resize the image and convert it to a tensor
        var inputTexture = new Texture2D(inputSize.x, inputSize.y);
        inputTexture.ReadPixels(new Rect(0, 0, inputSize.x, inputSize.y), 0, 0);
        inputTexture.Apply();
        var inputTensor = new Tensor(inputTexture, channels: 3);

        // Run inference on the input tensor
        worker.Execute(inputTensor);
        var output = worker.PeekOutput();

        // Process the output tensor
        int count = 1;
        float max = 0;
        float max1 = 0;
        int index = 0;
        for (int i = 0; i < output.length; i++)
        {
            if (max < output[i])
            {
                max1 = max;
                max = output[i];
                index = i;
            }
            //UnityEngine.Debug.Log(i);

            //UnityEngine.Debug.Log(output[i]);

            count = count + 1;
        }

        //UnityEngine.Debug.Log($"Image was recognised as class number: " + output[0] + " " + output[1]);
        //UnityEngine.Debug.Log($"Image was recognised as class number: " + max)
		UnityEngine.Debug.Log($"Image was recognised as class number: " + index);

        float temp = max - max1;
        //UnityEngine.Debug.Log($"margin: " + temp);
    


        // Clean up
        inputTensor.Dispose();
        output.Dispose();
        RenderTexture.ReleaseTemporary(cameraTexture);
    }

    private void OnDestroy()
    {
        // Dispose of the worker
        worker.Dispose();
    }
}

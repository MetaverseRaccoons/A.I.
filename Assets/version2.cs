using UnityEngine;
using Unity.Barracuda;
using UnityEngine.UI;

public class version2 : MonoBehaviour
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
    public Vector2Int inputSize;//350 x 350

    // The output tensor shape
    private TensorShape outputShape;

    // The neural network model
    public Model model;

    // The worker for running inference
    private IWorker worker;

    public Text m_MyText;

    private string[] names;
    private string new_Text;
    private float temp;
    private int prev;
    private int index;
    private int check;
    private bool send;


    private void Start()
    {
        worker = modelAsset.CreateWorker();
        names = new string[]{ "Speed limit (20km/h)", "Speed limit (30km/h)", "Speed limit (50km/h)", "Speed limit (60km/h)", "Speed limit (70km/h)", "Speed limit (80km/h)", "End of speed limit (80km/h)",
        "Speed limit (100km/h)", "Speed limit (120km/h)", "No passing", "No passing for vehicles over 3.5 metric tons", "Right-of-way at the next intersection", "Priority road", "Yield", "Stop",
        "No vehicles", "Vehicles over 3.5 metric tons prohibited", "No entry", "General caution", "Dangerous curve to the left", "Dangerous curve to the right", "Double curve", "Bumpy road", "Slippery road",
        "Road narrows on the right", "Road work", "Traffic signals", "Pedestrians", "Children crossing", "Bicycles crossing", "Beware of ice/snow", "Wild animals crossing", "End of all speed and passing limits",
        "Turn right ahead", "Turn left ahead", "Ahead only", "Go straight or right", "Go straight or left", "Keep right", "Keep left", "Roundabout mandatory", "End of no passing", "End of no passing by vehicles over 3.5 metric tons"};

        int index = 0;
        // Load the neural network model from the asset
        //model = ModelLoader.Load(modelAsset);

        // Create the worker for running inference
        //worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpBurst, model);

        // Compute the output tensor shape
        //outputShape = new TensorShape(1, 1, model.outputs[outputName].shape[1], model.outputs[outputName].shape[0]);



    }

    public string Update()
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
        prev = index;

        index = 0;
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
        //UnityEngine.Debug.Log($"Image was recognised as class number: " + index);
        m_MyText.text = "This is my text";

        if (index == prev)
        { check++; }
        else
        { check = 0; }

        if (max - max1 > 1 && max > 4.5)
        {
            temp = max - max1;
            m_MyText.text = "You missed a roadsign " + names[index] + max;
            Debug.Log(names[index]);
            send = true;
        }
        else
        {
            m_MyText.text = "";
            prev = -1;
            send = false;
        }

        //UnityEngine.Debug.Log($"margin: " + temp);



        // Clean up
        inputTensor.Dispose();
        output.Dispose();
        RenderTexture.ReleaseTemporary(cameraTexture);
        if (send)
        {
            return "You missed a roadsign " + names[index];
        }
        else
            return "";
    }

    private void OnDestroy()
    {
        // Dispose of the worker
        worker.Dispose();
    }
}

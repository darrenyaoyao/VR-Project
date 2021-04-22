using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;

public class MLmodel : MonoBehaviour
{
    // Start is called before the first frame update
    public NNModel modelfile;
    private Model model;
    private IWorker engine;
    void Start()
    {
        model = ModelLoader.Load(modelfile);
        engine = WorkerFactory.CreateWorker(model, WorkerFactory.Device.GPU);
        int[] shape = { 10, 1, 10 };
        var input = new Tensor(shape);
        var output = engine.Execute(input).PeekOutput();
        Debug.Log("model running");
    }

    // Update is called once per frame
    void Update()
    {
        // int[] shape={10, 10};
        // var input = new Tensor(shape);
        // var output = engine.Execute(input).PeekOutput();

        // Debug.Log("model running");
    }
}

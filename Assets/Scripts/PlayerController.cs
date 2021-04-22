using System;
using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camTransform;
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    private StreamWriter lefthandpose;
    private StreamWriter righthandpose;
    private StreamWriter camerapose;
    private StreamWriter rightquat;
    private StreamWriter leftquat;
    private StreamWriter cameraquat;

    void Start()
    {
        lefthandpose = new StreamWriter("./lefthand_pose.csv");
        righthandpose = new StreamWriter("./righthand_pose.csv");
        camerapose = new StreamWriter("./camera_pose.csv");
        rightquat = new StreamWriter("./quat_righthand.csv");
        leftquat = new StreamWriter("./quat_lefthand.csv");
        cameraquat = new StreamWriter("./quat_camera.csv");
    }

    private void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClientSend.PlayerShoot(camTransform.forward);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ClientSend.PlayerThrowItem(camTransform.forward);
        }
    }

    private void FixedUpdate()
    {
        //        bool[] _inputs = new bool[]
        //{
        //            Input.GetKey(KeyCode.W),
        //            Input.GetKey(KeyCode.S),
        //            Input.GetKey(KeyCode.A),
        //            Input.GetKey(KeyCode.D),
        //            Input.GetKey(KeyCode.Space)
        //};

        //        Vector2 _inputDirection = Vector2.zero;
        //        if (_inputs[0])
        //        {
        //            _inputDirection.y += 1;
        //        }
        //        if (_inputs[1])
        //        {
        //            _inputDirection.y -= 1;
        //        }
        //        if (_inputs[2])
        //        {
        //            _inputDirection.x -= 1;
        //        }
        //        if (_inputs[3])
        //        {
        //            _inputDirection.x += 1;
        //        }


        //        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        //        _moveDirection *= moveSpeed;



        //        controller.Move(_moveDirection);

        SendMotionToServer();


    }


    void SendMotionToServer()
    {
        Vector3[] pos = new Vector3[3]
            {
                camTransform.position, leftHandTransform.position, rightHandTransform.position
        };


        Quaternion[] rot = new Quaternion[3]
    {
                camTransform.rotation, leftHandTransform.rotation, rightHandTransform.rotation
};
        string temp = "";
        Debug.Log("Hello" + pos[0]);

        float[] output = new float[]
        {
            pos[0].x,
            pos[0].y,
            pos[0].z,
            pos[1].x,
            pos[1].y,
            pos[1].z,
            pos[2].x,
            pos[2].y,
            pos[2].z,
            rot[0].x,
            rot[0].y,
            rot[0].z,
            rot[1].x,
            rot[1].y,
            rot[1].z,
            rot[2].x,
            rot[2].y,
            rot[2].z,
        };

        int length = output.Length;
        for (int index = 0; index < length; index++)
        {
            int write_to_csv = index / 3;
            switch (write_to_csv)
            {
                case 0:
                    temp = string.Concat(temp, Convert.ToString(output[index]));
                    temp = string.Concat(temp, ",");
                    if ((index + 1) % 3 == 0)
                    {
                        camerapose.Write(temp);
                        camerapose.Write("\n");
                    }
                    break;
                case 1:
                    temp = string.Concat(temp, Convert.ToString(output[index]));
                    temp = string.Concat(temp, ",");
                    if ((index + 1) % 3 == 0)
                    {
                        lefthandpose.Write(temp);
                        lefthandpose.Write("\n");
                    }
                    break;
                case 2:
                    temp = string.Concat(temp, Convert.ToString(output[index]));
                    temp = string.Concat(temp, ",");
                    if ((index + 1) % 3 == 0)
                    {
                        righthandpose.Write(temp);
                        righthandpose.Write("\n");
                    }
                    break;
                case 3:
                    temp = string.Concat(temp, Convert.ToString(output[index]));
                    temp = string.Concat(temp, ",");
                    if ((index + 1) % 3 == 0)
                    {
                        cameraquat.Write(temp);
                        cameraquat.Write("\n");
                    }
                    break;
                case 4:
                    temp = string.Concat(temp, Convert.ToString(output[index]));
                    temp = string.Concat(temp, ",");
                    if ((index + 1) % 3 == 0)
                    {
                        leftquat.Write(temp);
                        leftquat.Write("\n");
                    }
                    break;
                case 5:
                    temp = string.Concat(temp, Convert.ToString(output[index]));
                    temp = string.Concat(temp, ",");
                    if ((index + 1) % 3 == 0)
                    {
                        rightquat.Write(temp);
                        rightquat.Write("\n");
                    }
                    break;
                default:
                    break;
            }
            if ((index + 1) % 3 == 0) temp = "";
        }


        ClientSend.PlayerMotion(pos, rot);

    }
}

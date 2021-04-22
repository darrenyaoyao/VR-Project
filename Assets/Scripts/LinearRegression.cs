//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.IO;



//public class LinearRegression : MonoBehaviour
//{
//    public PlayerManager self;
//    public float threshold = 0.05f;
//    public float threshold2 = 0.5f;
//    // Start is called before the first frame update
//    float[] right_hand_x;
//    float[] right_hand_y;
//    float[] right_hand_z;
//    float[] left_hand_x;
//    float[] left_hand_y;
//    float[] left_hand_z;
//    float[] head_x;
//    float[] head_y;
//    float[] head_z;
//    float[] head_x_quat;
//    float[] head_y_quat;
//    float[] head_z_quat;
//    float[] left_hand_x_quat;
//    float[] left_hand_y_quat;
//    float[] left_hand_z_quat;
//    float[] right_hand_x_quat;
//    float[] right_hand_y_quat;
//    float[] right_hand_z_quat;

//    private void Start()
//    {
//        right_hand_x = new float[]
//        {
//        0.01945629f,
//        -0.28167213f,
//        -0.04246513f,
//        0.01847044f,
//        1.28438096f
//        };

//        right_hand_y = new float[]
//        {
//        0.02921312f,
//        -0.34109169f,
//        -0.01131041f,
//        0.02338491f,
//        1.29173842f
//        };

//        right_hand_z = new float[]
//        {
//        -0.10548991f,
//        -0.12428783f,
//        -0.0012918f,
//        -0.0045325f,
//        1.22970949f
//        };

//        left_hand_x = new float[]
//        {
//        0.12112454f,
//        -0.36512397f,
//        -0.09696557f,
//        -0.06365581f,
//        1.40220052f
//        };

//        left_hand_y = new float[]
//        {
//        0.0570724051f,
//        -0.369772168f,
//        0.00131037194f,
//        -0.00528317138f,
//        1.31208858f
//        };

//        left_hand_z = new float[]
//        {
//        0.1219576f,
//        -0.36956616f,
//        -0.12627624f,
//        -0.01742455f,
//        1.39017029f
//        };

//        head_x = new float[]
//        {
//        -0.04962093f,
//        0.00182832f,
//        -0.00255591f,
//        0.01620668f,
//        1.03377525f
//        };

//        head_y = new float[]
//        {
//        -0.02383731f,
//        -0.1821687f,
//        -0.00795369f,
//        -0.07215063f,
//        1.28249832f
//        };

//        head_z = new float[]
//        {
//        -0.2208806f,
//        -0.07361227f,
//        0.09875861f,
//        0.08399678f,
//        1.11149815f
//        };

//        head_x_quat = new float[]
//        {
//        -0.07542466f,
//        -0.08889163f,
//        -0.02223263f,
//        -0.05706766f,
//        1.23644378f
//        };

//        head_y_quat = new float[]
//        {
//        -0.00648618f,
//        0.0045622f,
//        -0.00321986f,
//        -0.00249813f,
//        1.00485889f
//        };

//        head_z_quat = new float[]
//        {
//        -0.07771779f,
//        -0.00672535f,
//        -0.06023885f,
//        -0.03078835f,
//        1.16059681f
//        };

//        left_hand_x_quat = new float[]
//        {
//        0.02036296f,
//        -0.02146914f,
//        -0.02855746f,
//        0.03254041f,
//        0.94162638f
//        };

//        left_hand_y_quat = new float[]
//        {
//        -0.01477332f,
//        0.05048649f,
//        -0.03680759f,
//        -0.01339094f,
//        0.99290038f
//        };

//        left_hand_z_quat = new float[]
//        {
//        -0.07302313f,
//        0.211653f,
//        -0.15666977f,
//        0.00792574f,
//        0.98076622f
//        };

//        right_hand_x_quat = new float[]
//        {
//        0.04078259f,
//        -0.05357832f,
//        -0.01890608f,
//        -0.00618548f,
//        0.99026966f
//        };

//        right_hand_y_quat = new float[]
//        {
//        0.03509752f,
//        -0.04401608f,
//        0.00505686f,
//        -0.02572451f,
//        0.99473297f
//        };

//        right_hand_z_quat = new float[]
//        {
//        -0.05722559f,
//        0.07828142f,
//        -0.00469998f,
//        -0.03400972f,
//        0.96605734f
//        };

//    }

//    void FixedUpdate()
//    {
//        //if (self.diff2 < 1000)
//        //{
//        //    GameManager.players[self.id].diff2 = GameManager.players[self.id].diff2 + 1;
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            GameManager.players[self.id].is_trigger = true;
//        }
//        else if (Input.GetKeyDown(KeyCode.Backspace))
//        {
//            GameManager.players[self.id].is_trigger = false;
//        }

//        if (!GameManager.players[self.id].is_trigger) Linear_pos(self.id);
//        else pred_pose(self.id);
//        //}
//        //else
//        //{
//        //    GameManager.players[self.id].waitt += 1;
//        //    if(GameManager.players[self.id].waitt > 30)
//        //    {
//        //       GameManager.players[self.id].diff2 = 0;
//        //    }
//        //    
//        //}
//    }


//    public void Linear_pos(int _id)
//    {
//        //Linear Regression
//        Vector3 head = new Vector3(0.0f, 0.0f, 0.0f);
//        Vector3 l_hand = new Vector3(0.0f, 0.0f, 0.0f);
//        Vector3 r_hand = new Vector3(0.0f, 0.0f, 0.0f);
//        float temp_x = 0, temp_y = 0, temp_z = 0;

//        for (int i = 0; i < 3; i++)
//        {
//            for (int j = 0; j < 5; j++)
//            {
//                switch (i)
//                {
//                    case 0:
//                        temp_x = self.pred_prev_pose[i, j].x * head_x[j];
//                        temp_y = self.pred_prev_pose[i, j].y * head_y[j];
//                        temp_z = self.pred_prev_pose[i, j].z * head_z[j];
//                        head.x = head.x + temp_x;
//                        head.y = head.y + temp_y;
//                        head.z = head.z + temp_z;
//                        break;
//                    case 1:
//                        temp_x = self.pred_prev_pose[i, j].x * left_hand_x[j];
//                        temp_y = self.pred_prev_pose[i, j].y * left_hand_y[j];
//                        temp_z = self.pred_prev_pose[i, j].z * left_hand_z[j];
//                        l_hand.x = l_hand.x + temp_x;
//                        l_hand.y = l_hand.y + temp_y;
//                        l_hand.z = l_hand.z + temp_z;
//                        break;
//                    case 2:
//                        temp_x = self.pred_prev_pose[i, j].x * right_hand_x[j];
//                        temp_y = self.pred_prev_pose[i, j].y * right_hand_y[j];
//                        temp_z = self.pred_prev_pose[i, j].z * right_hand_z[j];
//                        r_hand.x = r_hand.x + temp_x;
//                        r_hand.y = r_hand.y + temp_y;
//                        r_hand.z = r_hand.z + temp_z;
//                        break;
//                }
//            }
//            temp_x = 0;
//            temp_y = 0;
//            temp_z = 0;

//        }
//        self.next_frame_pose[0] = head;
//        self.next_frame_pose[1] = l_hand;
//        self.next_frame_pose[2] = r_hand;

//    }

//    //private float lr(float[] x, float[] y)
//    //{
//    //    float sumx = 0, sumxx = 0, sumy = 0, sumyy = 0, sumxy=0;
//    //    for (int i = 0; i < 5; i++)
//    //    {
//    //        sumx += x[i];
//    //        sumy += y[i];
//    //        sumxx += x[i] * x[i];
//    //        sumxy += x[i] * y[i];
//    //        sumyy += y[i] * y[i];
//    //    }
//    //    float a = ((sumy * sumxx) - (sumx * sumxy)) / (5 * sumxx - sumx * sumx);
//    //    float b = ((5 * sumxy) - (sumx * sumy)) / (5 * sumxx - sumx * sumx);
//    //    return 6*a+b;
//    //}
//    //public void Linear_pos2(int _id)
//    //{
//    //    //Linear Regression
//    //    for (int i = 0; i < 3; i++)
//    //    {
//    //        float[] x = { 1, 2, 3, 4, 5 };
//    //        float[] y = { 1, 2, 3, 4, 5 };
//    //        for (int j = 0; j < 5; j++)
//    //        {
//    //            y[j] = self.pred_prev_pose[i, j].x;
//    //        }
//    //        self.next_frame_pose[i].x = lr(x, y);
//    //        for (int j = 0; j < 5; j++)
//    //        {
//    //            y[j] = self.pred_prev_pose[i, j].y;
//    //        }
//    //        self.next_frame_pose[i].y = lr(x, y);
//    //        for (int j = 0; j < 5; j++)
//    //        {
//    //            y[j] = self.pred_prev_pose[i, j].z;
//    //        }
//    //        self.next_frame_pose[i].z = lr(x, y);
//    //    }
//    //    GameManager.players[_id].cam.transform.position = self.next_frame_pose[0];
//    //    GameManager.players[_id].leftController.transform.position = self.next_frame_pose[1];
//    //    GameManager.players[_id].rightController.transform.position = self.next_frame_pose[2];
//    //}





//    public void pred_pose(int _id)
//    {
    
//    //Linear Regression
//    Vector3 head = new Vector3(0.0f, 0.0f, 0.0f);
//    Vector3 l_hand = new Vector3(0.0f, 0.0f, 0.0f);
//    Vector3 r_hand = new Vector3(0.0f, 0.0f, 0.0f);
//    float temp_x = 0, temp_y = 0, temp_z = 0;

//    for (int j = 0; j < 3; j++)  
//    {
//        for (int i = 1; i < 5; i++)
//        {
//            self.pred_prev_pose[j, i - 1] = self.pred_prev_pose[j, i];
//        }
//    }
//    self.pred_prev_pose[0, 4] = self.next_frame_pose[0];
//    self.pred_prev_pose[1, 4] = self.next_frame_pose[1];
//    self.pred_prev_pose[2, 4] = self.next_frame_pose[2];

//    for (int i = 0; i < 3; i++)
//    {
//        for (int j = 0; j < 5; j++)
//        {
//            switch (i)
//            {
//                case 0:
//                    temp_x = self.pred_prev_pose[i, j].x * head_x[j];
//                    temp_y = self.pred_prev_pose[i, j].y * head_y[j];
//                    temp_z = self.pred_prev_pose[i, j].z * head_z[j];
//                    head.x = head.x + temp_x;
//                    head.y = head.y + temp_y;
//                    head.z = head.z + temp_z;
//                    break;
//                case 1:
//                    temp_x = self.pred_prev_pose[i, j].x * left_hand_x[j];
//                    temp_y = self.pred_prev_pose[i, j].y * left_hand_y[j];
//                    temp_z = self.pred_prev_pose[i, j].z * left_hand_z[j];
//                    l_hand.x = l_hand.x + temp_x;
//                    l_hand.y = l_hand.y + temp_y;
//                    l_hand.z = l_hand.z + temp_z;
//                    break;
//                case 2:
//                    temp_x = self.pred_prev_pose[i, j].x * right_hand_x[j];
//                    temp_y = self.pred_prev_pose[i, j].y * right_hand_y[j];
//                    temp_z = self.pred_prev_pose[i, j].z * right_hand_z[j];
//                    r_hand.x = r_hand.x + temp_x;
//                    r_hand.y = r_hand.y + temp_y;
//                    r_hand.z = r_hand.z + temp_z;
//                    break;
//            }
//        }
//        temp_x = 0;
//        temp_y = 0;
//        temp_z = 0;
//    }

//    //////////////////////////////////Head
//    if (Mathf.Abs(head.y - self.pred_prev_pose[0, 4].y) >= threshold) 
//    {
//        if (head.y > 2.0f || head.y < 0.0f)
//        {
//            head.y = self.pred_prev_pose[0, 4].y + threshold;
//        }
//        //else
//        //{
//        //    head.y = self.pred_prev_pose[0, 4].y + threshold;
//        //}
//    }
//    if (Mathf.Abs(head.x - self.pred_prev_pose[0, 4].x) >= threshold) 
//    {
//        if (head.x > 1.0f || head.x < -1.0f)
//        {
//            head.x = self.pred_prev_pose[0, 4].x + threshold;
//        }
//        //else
//        //{
//        //    head.x = self.pred_prev_pose[0, 4].x + threshold;
//        //}
//    }

//    if (Mathf.Abs(head.z - self.pred_prev_pose[0, 4].z) >= threshold) 
//    {
//        if (head.z > 1.0f || head.z < -1.0f)
//        {
//            head.z = self.pred_prev_pose[0, 4].z + threshold;
//        }
//        //else
//        //{
//        //    head.z = self.pred_prev_pose[0, 4].z + threshold;
//        //}
//    }

//        //////////////////////////////////Lhand
//    if (Mathf.Abs(l_hand.x - self.pred_prev_pose[1, 4].x) >= threshold) 
//    {
//        if (l_hand.x > 1.0f || l_hand.x < -1.0f)
//        {
//            l_hand.x = self.pred_prev_pose[1, 4].x + threshold;
//        }
//        //else
//        //{
//        //    l_hand.x = self.pred_prev_pose[1, 4].x + threshold;
//        //}
//    }

//    if (Mathf.Abs(l_hand.y - self.pred_prev_pose[1, 4].y) >= threshold)
//    {
//        if (l_hand.y > 2.0f || l_hand.y < 0.0f)
//        {
//            l_hand.y = self.pred_prev_pose[1, 4].y + threshold;
//        }
//        //else
//        //{
//        //    l_hand.y = self.pred_prev_pose[1, 4].y + threshold;
//        //}
//    }

//    if (Mathf.Abs(l_hand.z - self.pred_prev_pose[1, 4].z) >= threshold) 
//    {
//        if (l_hand.z > 1.0f || l_hand.z < -1.0f)
//        {
//            l_hand.z = self.pred_prev_pose[1, 4].z + threshold;
//        }
//        //else
//        //{
//        //    l_hand.z = self.pred_prev_pose[1, 4].z + threshold;
//        //}
//    }


//    //////////////////////////////////Rhand
//    if (Mathf.Abs(r_hand.x - self.pred_prev_pose[2, 4].x) >= threshold)
//    {
//        if (r_hand.x > 1.0f || r_hand.x < -1.0f)
//        {
//            r_hand.x = self.pred_prev_pose[2, 4].x + threshold;
//        }
//        //else
//        //{
//        //    r_hand.x = self.pred_prev_pose[2, 4].x + threshold;
//        //}
//    }

//    if (Mathf.Abs(r_hand.y - self.pred_prev_pose[2, 4].y) >= threshold) 
//    {
//        if (r_hand.y > 2.0f || r_hand.y < 0.0f)
//        {
//            r_hand.y = self.pred_prev_pose[2, 4].y + threshold;
//        }
//        //else
//        //{
//        //    r_hand.y = self.pred_prev_pose[2, 4].y + threshold;
//        //}
//    }

//    if (Mathf.Abs(r_hand.z - self.pred_prev_pose[2, 4].z) >= threshold) 
//    {
//        if (r_hand.z > 1.0f || r_hand.z < -1.0f)
//        {
//            r_hand.z = self.pred_prev_pose[2, 4].z + threshold;
//        }
//        //else
//        //{
//        //    r_hand.z = self.pred_prev_pose[2, 4].z + threshold;
//        //}
//    }

//    if (Mathf.Abs(head.x) <= 0.001f) head.x = -0.9f;
//    if (Mathf.Abs(head.z) <= 0.001f) head.z = -0.9f;
//    if (Mathf.Abs(l_hand.x) <= 0.001f) l_hand.x = -0.9f;
//    if (Mathf.Abs(l_hand.z) <= 0.001f) l_hand.x = -0.9f;
//    if (Mathf.Abs(r_hand.x) <= 0.001f) r_hand.x = 0.9f;
//    if (Mathf.Abs(r_hand.z) <= 0.001f) r_hand.x = 0.9f;

//    head.y = self.pery;
//    l_hand.y = self.pery1;
//    r_hand.y = self.pery2;
//    self.next_frame_pose[0] = head;
//    self.next_frame_pose[1] = l_hand;
//    self.next_frame_pose[2] = r_hand;
//    Debug.Log("pred head = " + self.next_frame_pose[0]);
//    Debug.Log("pred lhand = " + self.next_frame_pose[1]);
//    Debug.Log("pred rhand = " + self.next_frame_pose[2]);
//    GameManager.players[_id].cam.transform.position = self.next_frame_pose[0];
//    GameManager.players[_id].leftController.transform.position = self.next_frame_pose[1];
//    GameManager.players[_id].rightController.transform.position = self.next_frame_pose[2];
//    }
//}





///*
//public void pred_quat(int _id)
//{
//    //Linear Regression
//    Quaternion head_q = new Quaternion(0.0f, 0.0f, 0.0f);
//    Quaternion l_hand_q = new Quaternion(0.0f, 0.0f, 0.0f);
//    Quaternion r_hand_q = new Quaternion(0.0f, 0.0f, 0.0f);
//    float temp_x = 0, temp_y = 0, temp_z = 0;

//    for (int i = 0; i < 3; i++)
//    {
//        for (int j = 0; j < 5; j++)
//        {
//            switch (i)
//            {
//                case 0:
//                    temp_x = GameManager.players[_id].pred_prev_quat[i, j].x * head_x_quat[j];
//                    temp_y = GameManager.players[_id].pred_prev_quat[i, j].y * head_y_quat[j];
//                    temp_z = GameManager.players[_id].pred_prev_quat[i, j].z * head_z_quat[j];
//                    head_q.x = head_q.x + temp_x;
//                    head_q.y = head_q.y + temp_y;
//                    head_q.z = head_q.z + temp_z;
//                    break;
//                case 1:
//                    temp_x = GameManager.players[_id].pred_prev_quat[i, j].x * left_hand_x_quat[j];
//                    temp_y = GameManager.players[_id].pred_prev_quat[i, j].y * left_hand_y_quat[j];
//                    temp_z = GameManager.players[_id].pred_prev_quat[i, j].z * left_hand_z_quat[j];
//                    l_hand_q.x = l_hand_q.x + temp_x;
//                    l_hand_q.y = l_hand_q.y + temp_y;
//                    l_hand_q.z = l_hand_q.z + temp_z;
//                    break;
//                case 2:
//                    temp_x = GameManager.players[_id].pred_prev_quat[i, j].x * right_hand_x_quat[j];
//                    temp_y = GameManager.players[_id].pred_prev_quat[i, j].y * right_hand_y_quat[j];
//                    temp_z = GameManager.players[_id].pred_prev_quat[i, j].z * right_hand_z_quat[j];
//                    r_hand_q.x = r_hand_q.x + temp_x;
//                    r_hand_q.y = r_hand_q.y + temp_y;
//                    r_hand_q.z = r_hand_q.z + temp_z;
//                    break;
//            }
//        }
//        temp_x = 0;
//        temp_y = 0;
//        temp_z = 0;

//    }
//    GameManager.players[_id].next_frame_quat[0] = head_q;
//    GameManager.players[_id].next_frame_quat[1] = l_hand_q;
//    GameManager.players[_id].next_frame_quat[2] = r_hand_q;

//    Debug.Log("predict head_q = " + GameManager.players[_id].next_frame_quat[0]);
//    Debug.Log("predict l_hand_q = " + GameManager.players[_id].next_frame_quat[1]);
//    Debug.Log("predict r_hand_q = " + GameManager.players[_id].next_frame_quat[2]);
 
//}
//   */



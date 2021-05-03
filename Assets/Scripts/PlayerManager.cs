using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool is_trigger = false;
    public float inter_po;
    public int id;
    public float pery;
    public float pery1;
    public float pery2;
    public float timestamp = 0.0f;
    public string username;
    public float health;
    public Vector3[,] pred_prev_pose = new Vector3[3, 5];
    public Quaternion[,] pred_prev_quat = new Quaternion[3, 5];
    public Vector3[] next_frame_pose = new Vector3[3];
    public Vector3[] prev_pose = new [] { new Vector3 (0f, 0f, 0f ), new Vector3 (0f, 0f, 0f), new Vector3 (0f, 0f, 0f )};
    public Quaternion[] prev_rot = new Quaternion[3]; 
    
    public Quaternion[] next_frame_quat = new Quaternion[3];
    public float maxHealth = 100f;
    public int itemCount = 0;
    public MeshRenderer model;
   

    public GameObject cam;
    public GameObject leftController;
    public GameObject rightController;

    public Vector3[] predictpos = new Vector3[3]; 
    public Quaternion[] predictrot = new Quaternion[3]; 
    

    private void start()
    {
        if(communicate.instance.frame_start == 7)
        {
            inter_po = 0.1f;
        }
        else if(communicate.instance.frame_start == 10)
        {
            inter_po = 0.08f;
        }
        else if(communicate.instance.frame_start == 18)
        {
            inter_po = 0.1f;
        }
        else if(communicate.instance.frame_start == 22)
        {
            inter_po = 0.02f;
        }
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;
    }

    public void SetHealth(float _health)
    {
        health = _health;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        model.enabled = false;
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }

    public void Update(){
        if(Input.GetKeyDown("space")) 
        {
            if (is_trigger==false){ is_trigger = true; }
            else { is_trigger = false; } 
        }
        if(is_trigger == true)
        {
            // if (predictpos[0] == Vector3.zero && predictpos[1] == Vector3.zero && predictpos[2] == Vector3.zero)
            // {
            //     Debug.Log("pass");
            //     cam.transform.position = Vector3.Lerp(cam.transform.position, prev_pose[0], inter_po);
            //     leftController.transform.position = Vector3.Lerp(leftController.transform.position, prev_pose[1], inter_po);
            //     rightController.transform.position = Vector3.Lerp(rightController.transform.position, prev_pose[2], inter_po);
            //     Debug.Log("pred cam prev = " + prev_pose[0]);
            //     Debug.Log("pred left prev = " + prev_pose[1]);
            //     Debug.Log("pred right prev = " + prev_pose[2]);
            //     cam.transform.rotation = prev_rot[0];
            //     leftController.transform.rotation = prev_rot[1];
            //     rightController.transform.rotation = prev_rot[2];
            // }
            // else
            // {

            //foreach (KeyValuePair<int, PlayerManager> kvp in GameManager.players)
            //{
            //    if (id == kvp.Key)
            //    {
            //        Debug.Log("datapackets: kvp" + kvp.Key);
            //        for (int i = 0; i < 3; i++)
            //        {

            //            GameManager.players[kvp.Key].predictpos[i] = GameManager.players_pos_datapacket[kvp.Key][frame_, i];
            //            GameManager.players[kvp.Key].predictrot[i] = GameManager.players_rot_datapacket[kvp.Key][frame_start, i];
            //        }

            //    }
            //}
                cam.transform.position = predictpos[0];//Vector3.Lerp(cam.transform.position, , inter_po);
                leftController.transform.position = predictpos[1];//Vector3.Lerp(leftController.transform.position, predictpos[1], inter_po);
                rightController.transform.position = predictpos[2];//Vector3.Lerp(rightController.transform.position, predictpos[2], inter_po);
                //cam.transform.rotation = predictrot[0];
                //leftController.transform.rotation = predictrot[1];
                //rightController.transform.rotation = predictrot[2];
                cam.transform.rotation = prev_rot[0];
                leftController.transform.rotation = prev_rot[1];
                rightController.transform.rotation = prev_rot[2];
                // prev_pose = predictpos;
  
                Debug.Log("pred cam = " +id+ ", "+ predictpos[0]);
                Debug.Log("pred left = " + id + ", " + predictpos[1]);
                Debug.Log("pred right = " + id + ", " + predictpos[2]);
                Debug.Log("pred rot left = " + id + ", " + predictrot[1]);
                Debug.Log("pred rot right = " + id + ", " + predictrot[2]);
           // }
           
        }

    }
}

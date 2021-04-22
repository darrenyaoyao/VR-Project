using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiceiveDataPacket
{

    public const int joint_num = 3;
    public const int sequence_num = 25;
    private const int length = sequence_num * joint_num * 3 * 2 * 4 + 4; //Data structure size
    
    int _id;
    Vector3[,] _pos = new Vector3[sequence_num, joint_num];
    Quaternion[,] _rot = new Quaternion[sequence_num, joint_num];
    //public static Dictionary<int, Vector3[,]> POS_packet = new Dictionary<int, Vector3[,]>();
    //public static Dictionary<int, Vector3[,]> ROT_packet = new Dictionary<int, Vector3[,]>();
    public ReiceiveDataPacket(byte[] b)
    {
        // byte b to data structure
        int start = 0;
        _id = BitConverter.ToInt32(b, start);
        start += 4;
        for (int j = 0; j < sequence_num; j++)
        {
            for (int i = 0; i < joint_num; i++)
            {
                _pos[j, i].x = BitConverter.ToSingle(b, start);
                _pos[j, i].y = BitConverter.ToSingle(b, start + 4);
                _pos[j, i].z = BitConverter.ToSingle(b, start + 8);
                start += 12;
            }
             for (int i = 0; i < joint_num; i++)
            {
                _rot[j, i].x = BitConverter.ToSingle(b, start);
                _rot[j, i].y = BitConverter.ToSingle(b, start + 4);
                _rot[j, i].z = BitConverter.ToSingle(b, start + 8);
                //Done
                _rot[j, i].w = (float)Math.Sqrt(1-_rot[j, i].x*_rot[j, i].x-_rot[j, i].y*_rot[j, i].y-_rot[j, i].z*_rot[j, i].z);
                start += 12;
            }
        }
        
        Debug.Log("datapackets: "+_id);
        if (!GameManager.players_pos_datapacket.ContainsKey(_id))
        {
            GameManager.players_pos_datapacket.Add(_id, _pos);
            GameManager.players_rot_datapacket.Add(_id, _rot);
        }
        else
        {
            GameManager.players_pos_datapacket[_id] = _pos;
            GameManager.players_rot_datapacket[_id] = _rot;
        }

    }
    public static int Length => length;
    public int id => _id;
    public Vector3[,] pos => _pos;
    public Quaternion[,] rot => _rot;
    
 //POS_packet[_id] = _pos;

}


public class SendDataPacket
{

    public const int joint_num = 3;
    private const int length = joint_num * 3 * 2 * 4 + 4; //Data structure size
    //Data structure
    int _id;
    Vector3[] _pos = new Vector3[joint_num];
    Quaternion[] _rot = new Quaternion[joint_num];

    public SendDataPacket(int id, Vector3[] pos, Quaternion[] rot)
    {
        //initialize packet
        _id = id;
        Array.Copy(pos, _pos, joint_num);
        Array.Copy(rot, _rot, joint_num);
    }
    public static int Length => length;

    public void tobyte(byte[] b)
    {
        //packet to bytearray

        int start = 0;
        Buffer.BlockCopy(BitConverter.GetBytes(_id), 0, b, start, 4);
        start += 4;
        for (int i = 0; i < joint_num; i++)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(_pos[i].x), 0, b, start, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(_pos[i].y), 0, b, start + 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(_pos[i].z), 0, b, start + 8, 4);
            start += 12;
        }
        for (int i = 0; i < joint_num; i++)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(_rot[i].x), 0, b, start, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(_rot[i].y), 0, b, start + 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(_rot[i].z), 0, b, start + 8, 4);
            start += 12;
        }
    }
}
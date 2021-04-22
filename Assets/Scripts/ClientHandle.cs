using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        float now = (float)((DateTime.UtcNow - new DateTime(2021, 4, 5, 0, 0, 0, 0).ToLocalTime()).TotalMilliseconds);
        int _id = _packet.ReadInt();
        float time_stamp = _packet.ReadFloat();
        if (Client.instance.myId == _id) return;

        //Debug.Log("timedelay = " + (now - time_stamp));
        // GameManager.players[_id].countdown_timer = GameManager.players[_id].countdown_timer - 1;
        // Debug.Log("countdown = " + GameManager.players[_id].countdown_timer);
        // if(GameManager.players[_id].countdown_timer == 0)
        // {
        //     GameManager.players[_id].is_trigger = true;
        // }

        List<Vector3> positions = new List<Vector3>();
        List<Quaternion> rotations = new List<Quaternion>();

        for (int i = 0; i < 3; ++i)
        {
            positions.Add(_packet.ReadVector3());
            rotations.Add(_packet.ReadQuaternion());
        }
        // Array.Copy(GameManager.players[_id].prev_rot, rotations.ToArray(),3);
        
        Debug.Log("Show package info " + _id + " " + string.Join(", ", positions.ToArray()) + " " + string.Join(", ", rotations.ToArray()));
        communicate.instance.sendToPython(new SendDataPacket(_id, positions.ToArray(), rotations.ToArray()));

        for(int i = 0; i < 3; i++)
        {
            GameManager.players[_id].prev_rot[0] = rotations[0];
            GameManager.players[_id].prev_rot[1] = rotations[1];
            GameManager.players[_id].prev_rot[2] = rotations[2];
        }


        if(GameManager.players[_id].is_trigger == false)
        {
            Debug.Log("fuck");
            GameManager.players[_id].cam.transform.position = positions[0];
            GameManager.players[_id].leftController.transform.position = positions[1];
            GameManager.players[_id].rightController.transform.position = positions[2];
            GameManager.players[_id].cam.transform.rotation = rotations[0];
            GameManager.players[_id].leftController.transform.rotation = rotations[1];
            GameManager.players[_id].rightController.transform.rotation = rotations[2];
        }

        // for (int j = 0; j < 3; j++)
        // {
        //     for (int i = 1; i < 5; i++)
        //     {
        //         GameManager.players[_id].pred_prev_pose[j, i - 1] = GameManager.players[_id].pred_prev_pose[j, i];
        //         //GameManager.players[_id].pred_prev_quat[j, i - 1] = GameManager.players[_id].pred_prev_quat[j, i];
        //     }
        // }
        // GameManager.players[_id].pery = positions[0].y;
        // GameManager.players[_id].pery1 = positions[1].y;
        // GameManager.players[_id].pery2 = positions[2].y;

        // GameManager.players[_id].pred_prev_pose[0, 4] = positions[0];
        // GameManager.players[_id].pred_prev_pose[1, 4] = positions[1];
        // GameManager.players[_id].pred_prev_pose[2, 4] = positions[2];
        // GameManager.players[_id].pred_prev_quat[0, 4] = rotations[0];
        // GameManager.players[_id].pred_prev_quat[1, 4] = rotations[1];
        // GameManager.players[_id].pred_prev_quat[2, 4] = rotations[2];

        //if (GameManager.players[_id].is_trigger == false)
        //{

        //}
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        if (Client.instance.myId == _id) return;

        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_health);
    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].Respawn();
    }

    public static void CreateItemSpawner(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        Vector3 _spawnerPosition = _packet.ReadVector3();
        bool _hasItem = _packet.ReadBool();

        GameManager.instance.CreateItemSpawner(_spawnerId, _spawnerPosition, _hasItem);
    }

    public static void ItemSpawned(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemSpawned();
    }

    public static void ItemPickedUp(Packet _packet)
    {
        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemPickedUp();
        GameManager.players[_byPlayer].itemCount++;
    }

    public static void SpawnProjectile(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _thrownByPlayer = _packet.ReadInt();

        GameManager.instance.SpawnProjectile(_projectileId, _position);
        GameManager.players[_thrownByPlayer].itemCount--;
    }

    public static void ProjectilePosition(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.projectiles[_projectileId].transform.position = _position;
    }

    public static void ProjectileExploded(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.projectiles[_projectileId].Explode(_position);
    }
}

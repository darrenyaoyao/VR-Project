﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientSend : MonoBehaviour
{
    /// <summary>Sends a packet to the server via TCP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to the server via UDP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    /// <summary>Sends player input to the server.</summary>
    /// <param name="_inputs"></param>
    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }

    /// <summary>Sends player motion to the server.</summary>
    /// position, rotation. sequence: head, lefthand, right hand
    public static void PlayerMotion(Vector3[] positions, Quaternion[] rotations)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            float ret = (float)((DateTime.UtcNow - new DateTime(2021, 4, 14, 0, 0, 0, 0).ToLocalTime()).TotalMilliseconds);
            //Debug.Log("time = " + ret);
            //Debug.Log("local time =" + DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy"));
            _packet.Write(ret);
            _packet.Write(positions[0]);
            _packet.Write(rotations[0]);

            _packet.Write(positions[1]);
            _packet.Write(rotations[1]);

            _packet.Write(positions[2]);
            _packet.Write(rotations[2]);
            SendUDPData(_packet);
        }
    }


    public static void PlayerShoot(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }

    public static void PlayerThrowItem(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerThrowItem))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }
    #endregion
}

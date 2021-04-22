using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;



public class communicate : MonoBehaviour
{
    // Start is called before the first frame update
    private int DISTANCE = 1;
    public Transform camTransform;
    public int frame_start;
    public static communicate instance;
    private TcpClient tcpClient;
    private Boolean client_health;
    NetworkStream netStream;
    private Thread tcpListenerThread;
    private byte[] buffer1 = new byte[SendDataPacket.Length];
    private byte[] buffer2 = new byte[ReiceiveDataPacket.Length];
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Start()
    {
        client_health = StartClient();
        if (client_health)
        {
            tcpListenerThread = new Thread(new ThreadStart(listenPython));
            tcpListenerThread.IsBackground = true;
            tcpListenerThread.Start();
        }
    }
    private Boolean StartClient()
    {
        try
        {
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 60000);
            tcpClient = new TcpClient();
            tcpClient.Connect(ipEndPoint);
            if (!tcpClient.Connected)
            {
                throw new Exception("connection to python server failed!");
            }
            netStream = tcpClient.GetStream();
            if (!netStream.CanWrite)
            {
                throw new Exception("stream write to python server failed!");
            }
            if (!netStream.CanRead)
            {
                throw new Exception("stream read to python server failed!");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
            return false;
        }
        catch (Exception Exception)
        {
            Debug.Log("Exception " + Exception.ToString());
            return false;
        }
        return true;

    }

    public void sendToPython(SendDataPacket packet)
    {
        if (!netStream.CanWrite)
        {
            throw new Exception("stream write to python server failed!");
        }
        packet.tobyte(buffer1);
        netStream.Write(buffer1, 0, SendDataPacket.Length);
    }
    private void listenPython()
    {
        int total_length = ReiceiveDataPacket.Length;
        
        while (true)
        {
            int length = 0;
            
            while (length < total_length)
            {
                length = length + netStream.Read(buffer2, length, total_length - length);
            }
            Debug.Log("pred");
            ReiceiveDataPacket packet = new ReiceiveDataPacket(buffer2);
            handledata(packet);
        }
    }

    private Boolean checkPlayerInValidDistance(Vector3 localplayerPosition, Vector3 playerPosition) {
        
        if(((localplayerPosition.x - playerPosition.x)*(localplayerPosition.x - playerPosition.x) + 
           (localplayerPosition.y - playerPosition.y)*(localplayerPosition.y - playerPosition.y)) > DISTANCE*DISTANCE)
        return false;
        else return true;
    }

    private void handledata(ReiceiveDataPacket packet)
    {
        //handle reiceive packet (Place pose of player, select pose, interpolation)
        //TODO
        // if (GameManager.players[packet.id].is_trigger == true) checkPlayerInValidDistance(camTransform.position, packet.pos[frame_start, 0])
        // {
        if(frame_start>=22) 
        {
            frame_start =22;
        }

        // if(checkPlayerInValidDistance(camTransform.position, packet.pos[frame_start, 0])) {

        foreach (KeyValuePair<int, PlayerManager> kvp in GameManager.players)
        {
            if (packet.id == kvp.Key)
             {
                    Debug.Log("datapackets: kvp" + kvp.Key);
            for (int i = 0; i < 3; i++)
                {

                    GameManager.players[kvp.Key].predictpos[i] = GameManager.players_pos_datapacket[kvp.Key][frame_start,i];
                    GameManager.players[kvp.Key].predictrot[i] = GameManager.players_rot_datapacket[kvp.Key][frame_start,i];
                }

            }

        }

            
        // }

        
        
        // GameManager.players[packet.id].cam.transform.position = packet.pos[10, 0];
        // GameManager.players[packet.id].leftController.transform.position = packet.pos[10, 1];
        // GameManager.players[packet.id].rightController.transform.position = packet.pos[10, 2];
        //}

    }
    void OnDestroy()
    {
        tcpClient.Close();
        tcpListenerThread.Abort();
    }


}

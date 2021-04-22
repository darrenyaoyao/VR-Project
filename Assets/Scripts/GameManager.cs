using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, Vector3[,]> players_pos_datapacket = new Dictionary<int, Vector3[,]>();
    public static Dictionary<int, Quaternion[,]> players_rot_datapacket = new Dictionary<int, Quaternion[,]>();
    public static Dictionary<int, ItemSpawner> itemSpawners = new Dictionary<int, ItemSpawner>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject itemSpawnerPrefab;
    public GameObject projectilePrefab;
    public event Action onPlayerSpawn;

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

    /// <summary>Spawns a player.</summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
           
            foreach (Transform childTransform in _player.transform)
            {
                if (childTransform.gameObject.tag == "players")
                {
                    GameObject player_model = childTransform.gameObject;
                    Debug.Log("here");
                    foreach (Transform ct in player_model.transform)
                    {
                        if (ct.gameObject.tag == "playerid_" + _id.ToString())
                        {
                            Debug.Log("here");
                            ct.gameObject.SetActive(true);
                        }
                    }
                    break;
                }
            }

        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
            foreach (Transform childTransform in _player.transform)
            {
                if (childTransform.gameObject.tag == "players")
                {
                    GameObject player_model = childTransform.gameObject;
                    Debug.Log("here");
                    foreach (Transform ct in player_model.transform)
                    {
                        if (ct.gameObject.tag == "playerid_" + _id.ToString())
                        {
                            Debug.Log("here");
                            ct.gameObject.SetActive(true);
                        }
                    }
                    break;
                }
            }
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }




    public void CreateItemSpawner(int _spawnerId, Vector3 _position, bool _hasItem)
    {
        GameObject _spawner = Instantiate(itemSpawnerPrefab, _position, itemSpawnerPrefab.transform.rotation);
        _spawner.GetComponent<ItemSpawner>().Initialize(_spawnerId, _hasItem);
        itemSpawners.Add(_spawnerId, _spawner.GetComponent<ItemSpawner>());
    }

    public void SpawnProjectile(int _id, Vector3 _position)
    {
        GameObject _projectile = Instantiate(projectilePrefab, _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TargetScripts;
using System;
using PlayerScripts;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }
    public event Action<PlayerScript> OnPlayerSpawned;
    public GameManager GameManager;



    [Header("Prefabs")]
    public GameObject PlayerPrefab;
    public GameObject TargetPrefab;
    public Transform SpawnPoint;
    public List<GunSO> WeaponList = new List<GunSO>();

    [Header("Units")]
    public PlayerScript CurrentPlayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentPlayer = SpawnPlayer(SpawnPoint);
        NotifyPlayerSpawned(CurrentPlayer);
    }
    public PlayerScript SpawnPlayer(Transform SpawnPoint)
    {
        GameObject NewPlayer = Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
        PlayerScript PlayerScript = NewPlayer.GetComponent<PlayerScript>();
        PlayerScript.PlayerOptions = GameManager.gameObject.GetComponent<PlayerOptions>();
        return PlayerScript;
    }

    private void NotifyPlayerSpawned(PlayerScript player)
    {
        if (OnPlayerSpawned != null)
            OnPlayerSpawned(player);
    }
}

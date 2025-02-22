using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform playerPrefab;
    public Transform player;

    public Transform playerSpawnPoint;

    private void Awake()
    {
       player = Instantiate(playerPrefab, playerSpawnPoint);
    }
}

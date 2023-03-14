using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player Player;

    public ObjectPool ObjectPool;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        var exportablePoolObjects = FindObjectsOfType<ExportablePoolObject>(false);
        var gameState = new GameState
        {
            PlayerData = Player.Export(),
            PoolObjects = exportablePoolObjects.Select(o => o.Export()).ToList()
        };
        SaveSystem.SaveGame(gameState);
    }

    public void LoadGame()
    {
        var exportables = FindObjectsOfType<ExportablePoolObject>(false);
        foreach (var e in exportables)
        {
            e.GetComponent<PoolObject>().ReturnToPool();
        }

        var gameState = SaveSystem.LoadGame();
        var player = Player;
        player.Import(gameState.PlayerData);
        gameState.PoolObjects.ForEach(data =>
        {
            data.SelfImport();
        });
    }

    //Test update, to be deleted after finish testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("saving ....");
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("loading ....");
            LoadGame();
        }
    }
}

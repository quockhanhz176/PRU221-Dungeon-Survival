using System;
using System.Collections;
using System.Collections.Generic;
using Script.Enemy.Factories;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public EnemyFactory _nightWolf;
    public EnemyFactory _cursedBowman;
    public EnemyFactory _charger;

    private void Start()
    {
        _nightWolf = new NightWolfEnemyFactory();
        _cursedBowman = new CursedBowmanEnemyFactory();
        _charger = new ChargerEnemyFactory();
        StartCoroutine(NightWolfSpawn());
        StartCoroutine(CursedBowmanSpawn());
    }

    IEnumerator NightWolfSpawn()
    {
        while (true)
        {
            var nightWolfGo = _nightWolf.CreateEnemy();
            var transform1 = GameManager.Instance.Player.transform;
            var position = transform1.position;
            Transform transform2;
            nightWolfGo.transform.position = new Vector3(
                Random.Range(position.x - 10f,
                    position.x + 10f),
                Random.Range((transform2 = GameManager.Instance.Player.transform).position.y - 10f,
                    transform2.position.y + 10f));
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CursedBowmanSpawn()
    {
        while (true)
        {
            var cursedBowmanGO = _cursedBowman.CreateEnemy();
            var transform1 = GameManager.Instance.Player.transform;
            var position = transform1.position;
            Transform transform2;
            cursedBowmanGO.transform.position = new Vector3(
                Random.Range(position.x - 20f,
                    position.x + 20f),
                Random.Range((transform2 = GameManager.Instance.Player.transform).position.y - 20f,
                    transform2.position.y + 20f));
            yield return new WaitForSeconds(2.5f);
        }
    }
}
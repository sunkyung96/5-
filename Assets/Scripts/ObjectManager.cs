using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject itemPowerPrefab;
    public GameObject playerBulletAPrefab;
    public GameObject enemyBulletAPrefab;
    public GameObject enemyBulletBPrefab;
    public GameObject enemyBulletCPrefab;

    GameObject[] enemy;

    GameObject[] itemPower;

    GameObject[] playerBulletA;
    GameObject[] enemyBulletA;
    GameObject[] enemyBulletB;
    GameObject[] enemyBulletC;

    GameObject[] targetPool;

    void Awake()
    {
        enemy = new GameObject[10];

        itemPower = new GameObject[20];

        playerBulletA = new GameObject[100];
        enemyBulletA = new GameObject[100];
        enemyBulletB = new GameObject[100];
        enemyBulletC = new GameObject[100];

        Generate();
    }

    void Generate()
    {
        // 1.Enemy
        for(int index=0; index < enemy.Length; index++)
        {
            enemy[index] = Instantiate(enemyPrefab);
            enemy[index].SetActive(false);
        }
            
        // 2.Item
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }
            
        // 3.Bullet
        for (int index = 0; index < playerBulletA.Length; index++)
        {
            playerBulletA[index] = Instantiate(playerBulletAPrefab);
            playerBulletA[index].SetActive(false);
        }

        for (int index = 0; index < enemyBulletA.Length; index++)
        {
            enemyBulletA[index] = Instantiate(enemyBulletAPrefab);
            enemyBulletA[index].SetActive(false);
        }

        for (int index = 0; index < enemyBulletB.Length; index++)
        {
            enemyBulletB[index] = Instantiate(enemyBulletBPrefab);
            enemyBulletB[index].SetActive(false);
        }

        for (int index = 0; index < enemyBulletC.Length; index++)
        {
            enemyBulletC[index] = Instantiate(enemyBulletCPrefab);
            enemyBulletC[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "Enemy":
                targetPool = enemy;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "PlayerBulletA":
                targetPool = playerBulletA;
                break;
            case "EnemyBulletA":
                targetPool = enemyBulletA;
                break;
            case "EnemyBulletB":
                targetPool = enemyBulletB;
                break;
            case "enemyBulletC":
                targetPool = enemyBulletC;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
}

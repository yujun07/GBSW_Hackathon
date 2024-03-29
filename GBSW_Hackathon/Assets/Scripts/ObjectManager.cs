using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyCPrefab;
    public GameObject enemyDPrefab;

    GameObject[] enemyA;
    GameObject[] enemyB;
    GameObject[] enemyC;
    GameObject[] enemyD;

    GameObject[] targetPool;
    void Awake()
    {
        enemyA = new GameObject[30];
        enemyB = new GameObject[30];
        enemyC = new GameObject[30];
        enemyD = new GameObject[30];

        Generate();
    }

    void Generate()
    {
        for(int i = 0; i < enemyA.Length; i++)
        {
            enemyA[i] = Instantiate(enemyAPrefab);
            enemyA[i].SetActive(false);
        }
        
        for(int i = 0; i < enemyB.Length; i++)
        {
            enemyB[i] = Instantiate(enemyBPrefab);
            enemyB[i].SetActive(false);
        }

        for(int i = 0; i < enemyC.Length; i++)
        {
            enemyC[i] = Instantiate(enemyCPrefab);
            enemyC[i].SetActive(false);
        }
        
        for(int i = 0; i < enemyD.Length; i++)
        {
            enemyD[i] = Instantiate(enemyDPrefab);
            enemyD[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "EnemyA":
                targetPool = enemyA;
                break;
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyC":
                targetPool = enemyC;
                break;
            case "EnemyD":
                targetPool = enemyD;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyA":
                targetPool = enemyA;
                break;
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyC":
                targetPool = enemyC;
                break;
            case "EnemyD":
                targetPool = enemyD;
                break;
        }

        return targetPool;
    }
}

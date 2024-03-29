using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;
    public Transform playerPos;

    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public TextMeshProUGUI scoreText;
    public Image[] lifeImage;
    public Sprite lifeOverSprite;
    public Sprite lifeOnSprite;
    public GameObject gameOverSet;
    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawni;
    public bool spawnEnd;

    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "EnemyA", "EnemyB", "EnemyC", "EnemyD" };
        StageStart();
    }

    public void StageStart()
    {
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<TextMeshProUGUI>().text = "Stage " + stage + "\nStart";
        clearAnim.GetComponent<TextMeshProUGUI>().text = "Stage " + stage + "\nClear!";

        ReadSpawnFile();
        fadeAnim.SetTrigger("In");
    }

    public void StageEnd()
    {
        clearAnim.SetTrigger("On");
        fadeAnim.SetTrigger("Out");

        player.transform.position = playerPos.position;

        stage++;
        if (stage < 2)
            Invoke("GameOver", 6);
        else
            Invoke("StageStart", 5);
    }

    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawni = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringreader = new StringReader(textFile.text);

        while (stringreader != null)
        {
            string line = stringreader.ReadLine();
            Debug.Log(line);

            if (line == null)
                break;
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringreader.Close();
        nextSpawnDelay = spawnList[0].delay;

    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        TPSController playerLogic = player.GetComponent<TPSController>();

        // Formatting
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int enemyi = 0;
        switch (spawnList[spawni].type)
        {
            case "A":
                enemyi = 0;
                break;
            case "B":
                enemyi = 1;
                break;
            case "C":
                enemyi = 2;
                break;
            case "D":
                enemyi = 3;
                break;
        }

        // Spawning Enemies
        int enemyPoint = spawnList[spawni].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyi]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody rigid = enemy.GetComponent<Rigidbody>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;

        rigid.velocity = new Vector3(enemyLogic.speed * (-1), 0, 0);

        spawni++;
        if (spawni == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawni].delay;
    }
    public void UpdateLifeIcon(int life)
    {
        // Life icon set
        for (int i = 0; i < 3; i++)
        {

            lifeImage[i].sprite = lifeOverSprite;
        }

        for (int i = 0; i < life; i++)
        {
            lifeImage[i].color = new Color(1, 1, 1, 1);
            lifeImage[i].sprite = lifeOnSprite;
        }
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

}

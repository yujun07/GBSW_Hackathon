using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int health;
    public int enemyScore;

    public GameManager gameManager;
    public ObjectManager objectManager;
    public GameObject objects;
    public GameObject mesh;
    public GameObject player;


    GameObject bullet;
    Collider collider;

    void Start()
    {

        collider = gameObject.GetComponent<Collider>();
        gameManager = GetComponent<GameManager>();
        objectManager = GetComponent<ObjectManager>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health--;
            if(health == 0)
            {
                Destroy(collider);
                bullet = other.gameObject;
                StartCoroutine(Desttory2dObject());
                //mesh.SetActive(true);
            }
        }
    }
    private IEnumerator Desttory2dObject()
    {
        yield return new WaitForSeconds(0.02f);
        Destroy(bullet);
        
        yield return new WaitForSeconds(0.1f);
        objects.SetActive(false);
    }

}

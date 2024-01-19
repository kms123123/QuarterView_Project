using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField] GameObject enemyPrefab;

    public Queue<GameObject> objectPool = new Queue<GameObject>();

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < 100; i++)
        {
            GameObject enemyObject = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
            objectPool.Enqueue(enemyObject);
            enemyObject.SetActive(false);
        }
    }

    public void InsertQueue(GameObject enemyObject)
    {
        objectPool.Enqueue(enemyObject);
        enemyObject.SetActive(false);
    }
    
    public GameObject GetQueue(Vector3 position)
    {
        GameObject enemyObject = objectPool.Dequeue();
        enemyObject.SetActive(true);
        enemyObject.transform.position = position;

        enemyObject.GetComponent<EnemyController>().Initialize();

        return enemyObject;
    }

}

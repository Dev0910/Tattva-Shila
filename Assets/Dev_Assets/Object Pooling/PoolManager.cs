using CustomPool;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public List<Pool> pools = new List<Pool>();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach (var pool in pools)
        {
            //PoolOperator.InitalSpawn(pool,this.transform);
            pool.InitalSpawn(this.transform);
        }
    }
    public void AddToPool(GameObject _gameobject, EPool poolType)
    {
        foreach (var pool in pools)
        {
            if (pool.name == poolType)
            {
                //PoolOperator.AddToList(_gameobject,pool);
                pool.AddToList(_gameobject);
            }
        }
    }
    public GameObject TakeFromPool(EPool poolType)
    {
        foreach (var pool in pools)
        {
            if (pool.name == poolType)
            {
                //return PoolOperator.TakeFromList(pool);
                return pool.TakeFromList();
            }
        }
        return null;
    }
}

public enum EPool
{
    none,
    Bullets
}
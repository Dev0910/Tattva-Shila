using System.Collections.Generic;
using UnityEngine;

namespace CustomPool
{
    public class PoolOperator : MonoBehaviour
    {
        //call this function in the start of the game
        public static void InitalSpawn(Pool pool,Transform parent)
        {
            if(!pool.placeHolder)
            {
                pool.placeHolder = new GameObject(pool.prefab.name);
                pool.placeHolder.transform.parent = parent;
            }

            pool.list.Clear();

            for (int i = 0; i < pool.initialSpawnNumber; i++)
            {
                GameObject temp = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity);
                temp.transform.parent = pool.placeHolder.transform;
                pool.list.Add(temp);
                temp.SetActive(false);
            }
        }

        //call this to get a gameobject from the list
        public static GameObject TakeFromList(Pool pool)
        {
            if(pool != null)
            {
                if(pool.list.Count > 0)
                {
                    GameObject temp = pool.list[0];
                    pool.list.RemoveAt(0);
                    temp.SetActive(true);
                    return temp;
                }
                else
                {
                    Debug.LogWarning(pool.name + " List Ran out of " + pool.name);
                    GameObject temp = Instantiate(pool.prefab);
                    temp.transform.parent = pool.placeHolder.transform;
                    return temp;
                }
            }
            return null;
        }

        //call this function to add(Destroy) a Gameobject to the list
        public static void AddToList(GameObject gameObject,Pool pool)
        {
            if( pool != null )
            {
                pool.list.Add(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class Pool:IPool
    {
        public EPool name;
        public GameObject prefab;
        public int initialSpawnNumber;
        public List<GameObject> list;
        internal GameObject placeHolder;

        public void InitalSpawn(Transform parent)
        {
            PoolOperator.InitalSpawn(this, parent);
        }
        public GameObject TakeFromList()
        {
            GameObject temp = PoolOperator.TakeFromList(this);
            return temp;
        }
        public void AddToList(GameObject gameObject)
        {
            PoolOperator.AddToList(gameObject, this);
        }
    }

    public interface IPool
    {
        public void InitalSpawn(Transform parent);
        public GameObject TakeFromList();
        public void AddToList(GameObject gameObject);

    }
}




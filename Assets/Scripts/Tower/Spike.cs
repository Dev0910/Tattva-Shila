using UnityEngine;

public class Spike : MonoBehaviour
{
    private SoTower tower;
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D");
        if (other.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
        {
            baseEnemy.TakeSingleDamage(tower.towerType, tower.aoeDamage);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
        {
            baseEnemy.TakeSingleDamage(tower.towerType, tower.aoeDamage);
        }
    }
    public void SetTower(SoTower _tower)
    {
        tower = _tower;
    }
}

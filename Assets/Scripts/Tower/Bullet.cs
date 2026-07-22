using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    SoBullet bulletData;

    public void seek(Transform _target, SoBullet _bulletData)
    {
        target = _target;
        bulletData = _bulletData;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = bulletData.sprite;
    }

    void Update()
    {
        if (target == null)
        {
            //gameObject.SetActive(false);
            PoolManager.instance.AddToPool(this.gameObject, EPool.Bullets);
            return;
        }

        Vector2 direction = target.position - transform.position; //direction from bullet to target
        float distanceThisFrame = bulletData.speed * Time.deltaTime;//calculate the distance to travel this frame

        if (Vector2.Distance(this.transform.position, target.position) < 0.1f)//if distance from target is less then distance travel this frame
        {
            HitTarget();//call the hit function
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);//move towards the target
    }
    void HitTarget()
    {
        if (bulletData.bulletType == BulletType.Single)
        {
            BaseEnemy baseEnemy = target.GetComponent<BaseEnemy>();
            if (baseEnemy != null)
            {
                if (!bulletData.hasBurn)
                {
                    baseEnemy.TakeSingleDamage(bulletData.towerType, bulletData.damage);
                }
                else
                {
                    baseEnemy.TakeBurnDamage(bulletData.towerType, bulletData.damage, bulletData.burnDuration);
                }

                if (bulletData.hasSlowEffect)
                {
                    baseEnemy.SlowEnemyForDuration(bulletData.slowEffectDuration, bulletData.speedMultiplier);
                }
                if (bulletData.hasThrowBack)
                {
                    baseEnemy.ThrowBackEnemyForDuration(bulletData.throwBackSpeed);
                }
            }
        }
        else if (bulletData.bulletType == BulletType.AOE)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, bulletData.aoeRange, 6);
            Debug.Log(hits.Length);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
                    {
                        if (!bulletData.hasBurn)
                        {
                            baseEnemy.TakeSingleDamage(bulletData.towerType, bulletData.damage);
                        }
                        else
                        {
                            baseEnemy.TakeBurnDamage(bulletData.towerType, bulletData.damage, bulletData.burnDuration);
                        }

                        if (bulletData.hasSlowEffect)
                        {
                            baseEnemy.SlowEnemyForDuration(bulletData.slowEffectDuration, bulletData.speedMultiplier);
                        }
                        if (bulletData.hasThrowBack)
                        {
                            baseEnemy.ThrowBackEnemyForDuration(bulletData.throwBackSpeed);
                        }
                    }
                }
            }
        }
        if (bulletData.effect != null)
        {
            GameObject effect = Instantiate(bulletData.effect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        PoolManager.instance.AddToPool(this.gameObject, EPool.Bullets);
    }
}


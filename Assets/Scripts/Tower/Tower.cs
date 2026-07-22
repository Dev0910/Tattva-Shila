using System.Collections;
using CustomPool;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] EElements firstElements = EElements.None;
    [SerializeField] EElements secondElement = EElements.None;
    [SerializeField] SoTower[] towerData;
    [SerializeField] SoTower currentTowerData;
    [SerializeField] TMP_Text displayText;
    private Transform target;
    private GameObject aoeEffect;
    void Start()
    {
        SetData();

        GameManager.instance.onGameOver += OnGameOver;
    }
    Transform GetTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float _tempDistance = float.MaxValue;
        Transform _target = null;
        foreach (GameObject enemy in enemies)
        {
            if (Vector2.Distance(this.transform.position, enemy.transform.position) < _tempDistance && Vector2.Distance(this.transform.position, enemy.transform.position) < currentTowerData.range)
            {
                _target = enemy.transform;
                _tempDistance = Vector2.Distance(this.transform.position, enemy.transform.position);
            }
        }
        return _target;
    }

    public void AddElement(EElements element)
    {
        if (firstElements == EElements.None)
        {
            firstElements = element;
            SetData();
        }
        else if (secondElement == EElements.None)
        {
            secondElement = element;
            SetData();
        }
    }
    public void SetData()
    {
        foreach (SoTower soTowers in towerData)
        {
            if ((firstElements == soTowers.firstElement && secondElement == soTowers.secondElement) || (secondElement == soTowers.firstElement && firstElements == soTowers.secondElement))
            {
                GetComponent<SpriteRenderer>().sprite = soTowers.towerSprite;
                currentTowerData = soTowers;
                OnCurrentTowerChanged();
                UpdateDisplayText();
                return;
            }
        }
    }
    void UpdateDisplayText()
    {
        switch (currentTowerData.towerType)
        {
            case ETowerType.Base:
                {
                    displayText.text = "Base";
                    break;
                }
            case ETowerType.Fire:
                {
                    displayText.text = "Fire";
                    break;
                }
            case ETowerType.Water:
                {
                    displayText.text = "Water";
                    break;
                }
            case ETowerType.Earth:
                {
                    displayText.text = "Earth";
                    break;
                }
            case ETowerType.Air:
                {
                    displayText.text = "Air";
                    break;
                }
            case ETowerType.Steam:
                {
                    displayText.text = "Steam";
                    break;
                }
            case ETowerType.Flamethrower:
                {
                    displayText.text = "Smoke";
                    break;
                }
            case ETowerType.SandStorm:
                {
                    displayText.text = "SandStorm";
                    break;
                }
            case ETowerType.Ice:
                {
                    displayText.text = "Ice";
                    break;
                }
            case ETowerType.Mud:
                {
                    displayText.text = "Mud";
                    break;
                }
            case ETowerType.Lava:
                {
                    displayText.text = "Lava";
                    break;
                }
        }
    }

    void OnCurrentTowerChanged()
    {
        StopAllCoroutines();
        if (aoeEffect != null)
        {
            Destroy(aoeEffect);
            aoeEffect = null;
        }
        if (currentTowerData.towerType == ETowerType.Flamethrower)
        {
            StartCoroutine(Flamethrower());
        }
        else if (currentTowerData.towerType == ETowerType.SandStorm)
        {
            StartCoroutine(SandStorm());
        }
        else if (currentTowerData.towerType == ETowerType.Mud)
        {
            StartCoroutine(Mud());
        }
        else if (currentTowerData.towerType == ETowerType.Lava)
        {
            StartCoroutine(Lava());
        }
        else if (currentTowerData.towerType == ETowerType.Steam)
        {
            StartCoroutine(Steam());
        }
        else
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / currentTowerData.fireRate);
            Transform _target = GetTarget();
            if (_target != null)
            {
                GameObject bulletGO = PoolManager.instance.TakeFromPool(EPool.Bullets);
                bulletGO.transform.position = transform.position;

                Bullet bullet = bulletGO.GetComponent<Bullet>();

                if (bullet != null)
                {
                    bullet.seek(_target, currentTowerData.bulletData);
                }
            }
        }
    }
    IEnumerator Steam()
    {
        aoeEffect = Instantiate(currentTowerData.aoeEffectPrefab, transform.position, Quaternion.identity);
        aoeEffect.transform.localScale *= currentTowerData.range * 2;
        while (true)
        {
            aoeEffect.SetActive(true);
            yield return new WaitForSeconds(currentTowerData.aoeEffectDuration);
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentTowerData.range, currentTowerData.enemyLayer);
            //Debug.DrawRay(transform.position, transform.right * currentTowerData.range, Color.red);
            //Debug.Log(hits.Length);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
                    {
                        baseEnemy.TakeSingleDamage(currentTowerData.towerType, currentTowerData.aoeDamage);
                    }
                }
            }
            yield return new WaitForSeconds(currentTowerData.aoeEffectDuration);
            aoeEffect.SetActive(false);
            yield return new WaitForSeconds(1f / currentTowerData.fireRate);
        }
    }
    IEnumerator SandStorm()
    {
        aoeEffect = Instantiate(currentTowerData.aoeEffectPrefab, transform.position, Quaternion.identity);
        aoeEffect.transform.localScale *= currentTowerData.range * 2;
        aoeEffect.SetActive(true);
        while (true)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentTowerData.range, currentTowerData.enemyLayer);
            Debug.DrawRay(transform.position, transform.right * currentTowerData.range, Color.red);
            //Debug.Log(hits.Length);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
                    {
                        baseEnemy.TakeSingleDamage(currentTowerData.towerType, currentTowerData.aoeDamage);
                        baseEnemy.SlowEnemyForDuration(currentTowerData.slowEffectDuration, currentTowerData.speedMultiplier);
                    }
                }
            }
            //yield return new WaitForSeconds(currentTowerData.aoeEffectDuration);
            //aoeEffect.SetActive(false);
            yield return new WaitForSeconds(1f / currentTowerData.fireRate);
        }
    }
    IEnumerator Mud()
    {
        aoeEffect = Instantiate(currentTowerData.aoeEffectPrefab, transform.position, Quaternion.identity);
        aoeEffect.transform.localScale *= currentTowerData.range * 2f;
        while (true)
        {
            aoeEffect.SetActive(true);
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentTowerData.range, currentTowerData.enemyLayer);
            //Debug.DrawRay(transform.position, transform.right * currentTowerData.range, Color.red);
            //Debug.Log(hits.Length);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
                    {
                        baseEnemy.SlowEnemyForDuration(currentTowerData.slowEffectDuration, currentTowerData.speedMultiplier);
                    }
                }
            }

            yield return new WaitForSeconds(currentTowerData.aoeEffectDuration);
            aoeEffect.SetActive(false);
            yield return new WaitForSeconds(1f / currentTowerData.fireRate);
        }
    }
    IEnumerator Flamethrower()
    {
        aoeEffect = Instantiate(currentTowerData.aoeEffectPrefab, transform.position, Quaternion.identity);
        aoeEffect.transform.localScale *= currentTowerData.range * 2;
        while (true)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentTowerData.range, currentTowerData.enemyLayer);
            //Debug.DrawRay(transform.position, transform.right * currentTowerData.range, Color.red);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
                    {
                        baseEnemy.TakeSingleDamage(currentTowerData.towerType, currentTowerData.aoeDamage);
                    }
                }
            }
            yield return new WaitForSeconds(1f / currentTowerData.fireRate);
        }
    }
    IEnumerator Lava()
    {
        aoeEffect = Instantiate(currentTowerData.aoeEffectPrefab, transform.position, Quaternion.identity);
        aoeEffect.transform.localScale *= currentTowerData.range * 2;
        while (true)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentTowerData.range, currentTowerData.enemyLayer);
            //Debug.DrawRay(transform.position, transform.right * currentTowerData.range, Color.red);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.gameObject.TryGetComponent<BaseEnemy>(out BaseEnemy baseEnemy))
                    {
                        baseEnemy.SlowEnemyForDuration(currentTowerData.slowEffectDuration, currentTowerData.speedMultiplier);
                        baseEnemy.TakeBurnDamage(currentTowerData.towerType, currentTowerData.aoeDamage, currentTowerData.burnDuration);
                    }
                }
            }
            yield return new WaitForSeconds(1f / currentTowerData.fireRate);
        }
    }
    public ETowerType GetTowerType()
    {
        return currentTowerData.towerType;
    }

    void OnDisable()
    {
        GameManager.instance.onGameOver -= OnGameOver;
    }
    void OnGameOver()
    {
        StopAllCoroutines();
    }
    //draw a circle of range size
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (currentTowerData != null)
        {
            Gizmos.DrawWireSphere(transform.position, currentTowerData.range);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, towerData[0].range);
        }
    }
}


using UnityEngine.UI;
using System.Collections;
using UnityEngine;


public class BaseEnemy : MonoBehaviour
{
    public EElements elementType;
    public float maxHealth;
    public float health;
    [SerializeField] Image healthBar;
    public float speed;
    protected float currentSpeed;
    protected Transform target;
    public float stoppingDistance;
    protected bool isSlowed;
    private bool isBurning;
    private bool isSteamed;

    [Header("Attack")]
    public int damage;
    public float attackInterval;
    protected float timer;
    [Header("Animation")]
    [SerializeField] protected Animation[] animations;
    [SerializeField] protected float animationSpeed;
    protected int animationIndex;



    protected GameObject attacking;
    void Start()
    {
        maxHealth += Time.time;
        health = maxHealth;
        isBurning = false;
        isSlowed = false;
        isSteamed = false;
        currentSpeed = speed;
        animationIndex = -1;
        target = GameManager.instance.target.transform;
    }
    public void TakeSingleDamage(ETowerType towerType, float damage)
    {
        float damageMultiplier = CalculateDamageMultiplier(towerType);
        TakeDamage(damage * damageMultiplier);
    }

    float CalculateDamageMultiplier(ETowerType towerType)
    {
        switch (towerType)
        {
            case ETowerType.Base:
                {
                    return 1;
                }
            case ETowerType.Fire:
                {
                    if (elementType == EElements.Air)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Fire)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.Water:
                {
                    if (elementType == EElements.Fire)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Water)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.Earth:
                {
                    if (elementType == EElements.Water)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Earth)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.Air:
                {
                    if (elementType == EElements.Earth)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Air)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.Steam:
                {
                    if (elementType == EElements.Air)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Fire || elementType == EElements.Water)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.Flamethrower:
                {
                    if (elementType == EElements.Water)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Air || elementType == EElements.Fire)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.SandStorm:
                {
                    if (elementType == EElements.Water)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Air || elementType == EElements.Earth)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.Ice:
                {
                    if (elementType == EElements.Fire)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Air || elementType == EElements.Water)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.Mud:
                {
                    if (elementType == EElements.Fire)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Earth || elementType == EElements.Water)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
            case ETowerType.Lava:
                {
                    if (elementType == EElements.Water)
                    {
                        return 1.5f;
                    }
                    else if (elementType == EElements.Fire || elementType == EElements.Earth)
                    {
                        return 0.75f;
                    }
                    else
                    {
                        return 1;
                    }
                }
        }
        return 0;
    }

    public void TakeBurnDamage(ETowerType towerType, float totalDamage, float duration)
    {
        float damageMultiplier = CalculateDamageMultiplier(towerType);
        if (isBurning)
        {
            StopCoroutine(BurnDamage(damage * damageMultiplier, duration));
        }
        StartCoroutine((BurnDamage(damage * damageMultiplier, duration)));
    }


    IEnumerator BurnDamage(float totalDamage, float duration)
    {
        isBurning = true;
        for (int i = 0; i < 100; i++)
        {
            TakeDamage(totalDamage / 100);
            yield return new WaitForSeconds(duration / 100);
        }
        isBurning = false;
    }
    public void SlowEnemyForDuration(float duration, float speedMultiplier)
    {
        if (isSlowed)
        {
            StopCoroutine(SlowEnemy(duration, speedMultiplier));
        }
        StartCoroutine(SlowEnemy(duration, speedMultiplier));
    }
    IEnumerator SlowEnemy(float duration, float speedMultiplier)
    {
        isSlowed = true;
        currentSpeed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        currentSpeed = speed;
        isSlowed = false;
    }
    public void ThrowBackEnemyForDuration(float _speed)
    {
        StartCoroutine(ThrowBackEnemy(_speed));
    }

    IEnumerator ThrowBackEnemy(float _speed)
    {
        currentSpeed = _speed * -1;
        yield return new WaitForSeconds(1f);
        currentSpeed = speed;
    }
    public void TakeSteamDamage(ETowerType type, float totalDamage)
    {
        totalDamage /= 1000;
        if (isSteamed)
        {
            StopCoroutine(SteamThrow(totalDamage));
        }
        StartCoroutine(SteamThrow(totalDamage));
    }

    IEnumerator SteamThrow(float totalDamage)
    {
        isSteamed = true;
        for (int i = 0; i < 100; i++)
        {
            TakeDamage(totalDamage += Time.deltaTime);
            yield return new WaitForSeconds(1f);
        }
        isSteamed = false;
    }
    protected void PlayAnimation(int index)
    {
        if (index != animationIndex)
        {
            StopCoroutine(Animation(index));
            animationIndex = index;
            StartCoroutine(Animation(index));
        }
    }
    IEnumerator Animation(int index)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        while (index == animationIndex)
        {
            if (GameManager.instance.gameOver)
            {
                break;
            }
            foreach (Sprite sprite in animations[index].animation)
            {
                sr.sprite = sprite;
                yield return new WaitForSeconds(1 / animationSpeed);
            }
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
[System.Serializable]
public class Animation
{
    public string name;
    public Sprite[] animation;
}


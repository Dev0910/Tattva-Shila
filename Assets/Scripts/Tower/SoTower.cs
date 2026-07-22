using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Scriptable Object/Tower")]
public class SoTower : ScriptableObject
{
    public Sprite towerSprite;
    public ETowerType towerType;
    public float fireRate;
    public float range;

    public EElements firstElement = EElements.None;
    public EElements secondElement = EElements.None;

    [Header("Bullet")]
    public SoBullet bulletData;

    [Header("AOE Towers Only")]
    public GameObject aoeEffectPrefab;
    public float aoeDamage;
    public float aoeEffectDuration;
    public LayerMask enemyLayer;

    [Header("Burn Effect")]
    public float burnDuration;


    [Header("Slow Effect")]
    [Range(0, 1.0f)]
    public float speedMultiplier;
    public float slowEffectDuration;


    [Header("Throw Back Effect")]
    public float throwBackSpeed;

}



using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Scriptable Object/Bullet")]
public class SoBullet : ScriptableObject
{
    public BulletType bulletType;
    public ETowerType towerType;
    public Sprite sprite;
    public float damage = 50;
    public float speed = 1;
    public GameObject effect;


    [Header("Burn Effect")]
    public bool hasBurn;
    public float burnDuration;


    [Header("Slow Effect")]
    public bool hasSlowEffect;
    [Range(0, 1.0f)]
    public float speedMultiplier;
    public float slowEffectDuration;


    [Header("Throw Back Effect")]
    public bool hasThrowBack;
    public float throwBackSpeed;



    [Header("AOE")]
    public float aoeRange;
    public LayerMask enemyLayer;

}

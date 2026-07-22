using System;
using System.Collections;
using UnityEngine;

public class EnemyController : BaseEnemy
{
    private void FixedUpdate()
    {
        if (GameManager.instance.gameOver)
        {
            return;
        }
        if (target != null && attacking == null)
        {
            Vector2 direction = (target.position - this.transform.position).normalized;
            if (direction.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (direction.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            transform.Translate(direction * currentSpeed * 0.01f, Space.World);
            PlayAnimation(0);
            if ((this.transform.position - target.position).magnitude <= stoppingDistance)
            {
                currentSpeed = 0;
                if (!GameManager.instance.gameOver)
                {
                    GameManager.instance.GameOver();
                }
            }
        }
        else if (attacking != null)
        {
            Attack();
            PlayAnimation(1);
        }
    }
    void Attack()
    {
        if (timer <= 0)
        {
            attacking.GetComponent<WallHealth>().TakeDamage(damage);
            timer = attackInterval;
        }
        else
        {
            timer -= Time.fixedDeltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<WallHealth>(out WallHealth wallHealth))
        {
            attacking = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        try
        {
            if (other.gameObject == attacking.gameObject)
            {
                attacking = null;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}

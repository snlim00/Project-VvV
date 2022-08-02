using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    public float givingExp;

    [SerializeField] protected Vector2 speed;

    [SerializeField] protected Damage damage;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    protected void CheckMove()
    {
        if (doMove == false)
            return;
    }

    protected abstract void Move();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collision(collision);
    }

    protected virtual void Collision(Collider2D collision)
    {
        switch(collision.tag)
        {
            case TAG.PLAYER:

                if (PlayerController.S.isGod == true)
                    return;

                damage.damage *= 0.5f;
                PlayerController.S.TakeDamage(damage);
                Dead();

                break;

            case TAG.BORDER:

                PlayerController.S.curPp += (int)damage.damage * 0.33f;
                Delete();

                break;
        }
    }

    protected override void Dead()
    {
        PlayerController.S.curExp += givingExp;
        PlayerController.S.score += givingExp * 10;

        base.Dead();
    }
}

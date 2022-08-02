using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class MoveProperties
{
    public float defaultSpeed = 10;
    public float speed;
    [HideInInspector] private float _basicSpeed, _extraSpeed, _speedMultiplier;

    public float basicSpeed
    {
        get { return _basicSpeed; }
        set { _basicSpeed = value; SetSpeed(); }
    }

    public float extraSpeed
    {
        get { return _extraSpeed; }
        set { _extraSpeed = value; SetSpeed(); }
    }

    public float speedMultiplier
    {
        get { return _speedMultiplier; }
        set { _speedMultiplier = value; SetSpeed(); }
    }

    private void SetSpeed()
    {
        speed = (_basicSpeed + _extraSpeed) * speedMultiplier;
    }

    public void Init()
    {
        speedMultiplier = 1;
        extraSpeed = 0;
        basicSpeed = defaultSpeed;
    }
}

[Serializable]
public class FireProperties
{
    public GameObject bulletPref;
    public BulletController[] bulletArr;

    [HideInInspector] public int bulletCount = 200;
    public int bulletNum = 0;

    public Damage damage;
    public WaitForSeconds wfsDelay = new WaitForSeconds(0.2f);
    public float _delay;
    public float delay
    {
        get { return _delay; }
        set 
        {
            _delay = value; 
            wfsDelay = new WaitForSeconds(_delay); 
        }
    }
    public float speed;

    public int maxLevel = 4;
    public int level = 0;
    public float[,] angle = { { 0, 0, 0, 0, 0 }, { -3, 3, 0, 0, 0 }, { -6, 0, 6, 0, 0 }, { -9, -3, 3, 9, 0 }, { -12, -6, 0, 6, 12 } };
    public float range;

    public bool doAttack = true;

    public void Init()
    {
        bulletArr = new BulletController[bulletCount];
        bulletNum = 0;
    }

    public void Upgrade(int level = 1)
    {
        if (this.level >= maxLevel)
        {
            this.level = maxLevel;
            return;
        }

        this.level += level;
        damage.knockbackPower -= 0.3f;
    }
}
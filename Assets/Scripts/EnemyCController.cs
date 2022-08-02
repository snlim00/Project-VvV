using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCController : Enemy
{
    private int dir;
    private Vector3 playerPos;
    private float moveXDirection;
    private float movedDirection = 0;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Color bulletColor;
    [SerializeField] private float bulletSpeed = 6;

    protected override void Init()
    {
        base.Init();

        dir = Random.Range(0, 2);
        if (dir == 0)
            dir = -1;

        Vector3 pos;
        pos.x = dir == 1 ? 1.1f : -0.1f;
        pos.y = Random.Range(0.7f, 0.9f);
        pos.z = -mainCam.transform.position.z;
        transform.position = mainCam.ViewportToWorldPoint(pos);

        moveXDirection = Random.Range(6f, 8f);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
    }

    private bool isFire = false;
    List<int> angleList;
    protected override void Move()
    {
        CheckMove();

        if(movedDirection <= moveXDirection)
        {
            float movePos = -dir * speed.x * Time.deltaTime;
            transform.Translate(movePos, -speed.y * Time.deltaTime, 0);
            movedDirection += Mathf.Abs(movePos);
        }
        else
        {
            if (isFire == false)
            {
                isFire = true;
                angleList = new List<int>();

                Fire();
            }

            transform.Translate(0, -speed.y * Time.deltaTime * 2.5f, 0);
        }
    }

    bool CheckAngle(int a)
    {
        for(int i = 0; i < angleList.Count; ++i)
        {
            if (a == angleList[i])
                return true;
        }

        return false;
    }

    void Fire()
    {
        for (int i = 0; i < 7; ++i)
        {
            BulletController bullet = Instantiate(bulletPref).GetComponent<BulletController>();
            bullet.GetComponent<SpriteRenderer>().color = bulletColor;
            bullet.transform.localScale = bullet.transform.localScale * 0.7f;

            TrailRenderer tr = bullet.GetComponent<TrailRenderer>();
            tr.startColor = bulletColor;
            tr.time = 0.2f;

            int angle = Random.Range(9, 27);
            while (CheckAngle(angle) == true)
            {
                angle = Random.Range(9, 27);
            }
            angleList.Add(angle);

            bullet.Fire(damage, bulletSpeed, angle * 10, 15, this.transform);
        }
    }
}


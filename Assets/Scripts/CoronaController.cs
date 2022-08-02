using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaController : Enemy
{
    [SerializeField] private GameObject enemyG;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Color bulletColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Move()
    {
        //안움직여~
    }

    new protected void Collision(Collider2D collision)
    {
        switch (collision.tag)
        {
            case TAG.PLAYER:

                if (PlayerController.S.isGod == true)
                    return;

                damage.damage *= 0.5f;
                PlayerController.S.TakeDamage(damage);
                damage.damage *= 2;

                break;
        }
    }

    public IEnumerator Appare()
    {
        transform.position = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 1.5f, -mainCam.transform.position.z));

        float t = 0;
        Vector2 targetPos = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.88f, -mainCam.transform.position.z));

        Vector2 pos = transform.position;

        isGod = true;

        FindObjectOfType<EnemyGenerator>().doSpawn = false;

        float startScrollSpeed = BackgroundController.S.scrollSpeed;

        while (t < 1)
        {
            t += Time.deltaTime / 10;

            transform.position = Vector2.Lerp(pos, targetPos, t);
            BackgroundController.S.scrollSpeed = Mathf.Lerp(startScrollSpeed, 1, t);

            yield return null;
        }
        isGod = false;

        PlayerController.S.scoreMultiplier = -20;
        StartCoroutine(Pattern1());
    }

    IEnumerator Pattern1()
    {
        WaitForSeconds delay = new WaitForSeconds(0.06f);

        for(int i = 0; i < 200 * GameInfo.stage; ++i)
        {
            BulletController bullet = Instantiate(bulletPref).GetComponent<BulletController>();

            bullet.GetComponent<SpriteRenderer>().color = bulletColor;
            TrailRenderer tr = bullet.GetComponent<TrailRenderer>();
            tr.startColor = bulletColor;

            float angle = Random.Range(-30, -60) * 4;
            bullet.Fire(damage, 9, angle, 20, this.transform);
            yield return delay;
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(Pattern2());
    }

    IEnumerator Pattern2()
    {
        WaitForSeconds delay = new WaitForSeconds(0.8f - (0.2f * GameInfo.stage));

        for(int i = 0; i < 30 * GameInfo.stage; ++i)
        {
            Instantiate(enemyG).GetComponent<Enemy>().givingExp = 0;
            yield return delay;
        }

        yield return new WaitForSeconds(4);
        StartCoroutine(Pattern3());
    }

    IEnumerator Pattern3()
    {
        WaitForSeconds delay = new WaitForSeconds(0.15f);

        for (int i = 0; i < 40; ++i)
        {
            BulletController bullet = Instantiate(bulletPref).GetComponent<BulletController>();

            bullet.GetComponent<SpriteRenderer>().color = bulletColor;
            TrailRenderer tr = bullet.GetComponent<TrailRenderer>();
            tr.startColor = bulletColor;

            bullet.Fire(damage, 19, PlayerController.S.transform.position, 20, this.transform);
            yield return delay;
        }

        StartCoroutine(Pattern4());
    }

    IEnumerator Pattern4()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f - (0.1f * GameInfo.stage));

        for (int i = 0; i < 17 + (5 * GameInfo.stage); ++i)
        {
            for(int j = 0; j < 15; ++j)
            {
                BulletController bullet = Instantiate(bulletPref).GetComponent<BulletController>();

                bullet.GetComponent<SpriteRenderer>().color = bulletColor;
                TrailRenderer tr = bullet.GetComponent<TrailRenderer>();
                tr.startColor = bulletColor;


                float angle = Random.Range(-24, -60) * 5;
                bullet.Fire(damage, 8, angle, 20, this.transform);
            }
            yield return delay;
        }

        StartCoroutine(Pattern1());
    }

    protected override void Dead()
    {
        PlayerController.S.GameOver();

        base.Dead();
    }
}

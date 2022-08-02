using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Camera mainCam;

    [SerializeField] protected float maxHp;

    [SerializeField] protected float curHp;

    [SerializeField] private float _stance;

    public bool isGod = false;

    public float stance
    {
        get { return _stance; }
        set
        {
            _stance = 1 - value;

            if(stance > 1)
            {
                stance = 1;
            }
        }
    }

    protected bool isKnockback = false;
    protected Coroutine corKnockback;

    protected float stunDuration;
    protected Coroutine corStun;
    protected bool doMove = true;

    protected SpriteRenderer spr;
    protected Color defaultColor;
    [SerializeField] protected Color hitColor = Color.white;

    protected virtual void Init()
    {
        mainCam = Camera.main;

        spr = GetComponent<SpriteRenderer>();
        defaultColor = spr.color;

        curHp = maxHp;
    }

    public virtual void Heal(float heal)
    {
        maxHp += 2;
        curHp += heal;
        if(curHp > maxHp)
        {
            curHp = maxHp;
        }
    }

    public virtual void TakeDamage(Damage dmg)
    {
        if (isGod == true)
            return;

        curHp -= dmg.damage;

        if (curHp <= 0)
        {
            curHp = 0;

            Dead();
        }

        if (isKnockback == true)
        {
            StopCoroutine(corKnockback);
            isKnockback = false;
        }
        corKnockback = StartCoroutine(Knockback(dmg));


        if(stunDuration > 0 && stunDuration < dmg.stunDuration)
        {
            StopCoroutine(corStun);

            stunDuration = -1;
        }
        corStun = StartCoroutine(Stun(dmg.stunDuration));

        StartCoroutine(HitFlash());
    }

    protected WaitForSeconds hitFlashWfs = new WaitForSeconds(0.05f); 
    protected virtual IEnumerator HitFlash()
    {
        spr.color = hitColor;
        yield return hitFlashWfs;
        spr.color = defaultColor;
    }

    protected IEnumerator Knockback(Damage dmg)
    {
        isKnockback = true;

        float t = 0;

        while(t <= dmg.knockbackDuration)
        {
            t += Time.deltaTime;

            transform.Translate(dmg.knockbackDirection * dmg.knockbackPower * stance * Time.deltaTime);

            yield return null;
        }

        isKnockback = false;
    }

    protected IEnumerator Stun(float duration)
    {
        doMove = false;

        Vector2 pos = transform.position;

        stunDuration = duration;

        while(stunDuration >= 0)
        {
            stunDuration -= Time.deltaTime;

            transform.position = pos;

            yield return null;
        }

        doMove = true;
    }

    protected virtual void Dead()
    {
        Delete();
    }

    protected virtual void Delete()
    {
        Destroy(this.gameObject);
    }
}

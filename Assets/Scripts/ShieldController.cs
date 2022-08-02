using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : Attack
{
    private SpriteRenderer spr;
    private Color defaultColor;
    public bool isRest = true;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        spr = GetComponent<SpriteRenderer>();
        defaultColor = spr.color;

        //StartCoroutine(Bomb());
        Rest();
    }

    private void Update()
    {
        if(isRest == false)
            transform.position = PlayerController.S.transform.position;
    }

    private void Rest()
    {
        isRest = true;
        transform.position = new Vector2(100, 100);
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        spr.color = defaultColor;
        //gameObject.SetActive(false);
    }

    public IEnumerator Bomb()
    {
        damage = new Damage(5, 4, 0.3f, Vector2.up, 2f);
        float t = 0;

        while (t <= 0.25f)
        {
            t += Time.deltaTime / 7;

            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), 0.5f * t);
            spr.color = Color.Lerp(spr.color, new Color(1, 1, 1, 0), 0.3f * t);

            yield return null;
        }

        Rest();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : Attack
{
    Transform casterTrans;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Fire(Vector2 targetPos, Transform caster)
    {
        this.caster = caster.tag;
        casterTrans = caster;

        transform.position = caster.position;
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

        StartCoroutine(_Fire(targetPos));
    }

    private IEnumerator _Fire(Vector2 targetPos)
    {
        float t = 0;
        Vector2 startPos = transform.position;

        while (t <= 1)
        {
            t += Time.deltaTime / 9;

            transform.position = Vector2.Lerp(transform.position, targetPos, 0.5f * t);

            yield return null;
        }

    }

    public IEnumerator Return()
    {
        float t = 0;
        Vector2 startPos = transform.position;
        damage.damage *= 4f;
        damage.knockbackDuration *= 3;
        transform.localScale *= 1.3f;

        while(t <= 1)
        {
            t += Time.deltaTime * 2.5f;

            transform.position = Vector2.Lerp(startPos, casterTrans.position, t);

            yield return null;
        }

        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGController : Enemy
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        Vector3 pos;
        int a = Random.Range(0, 2);
        pos.x = a == 1 ? 1.1f : -0.1f;
        pos.y = Random.Range(0.2f, 0.9f);
        pos.z = -mainCam.transform.position.z;
        transform.position = mainCam.ViewportToWorldPoint(pos);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Move());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        
    }

    IEnumerator _Move()
    {
        float t = 0;
        Vector2 pos = transform.position;

        Vector2 targetPos;
        targetPos.x = Random.Range(0.1f, 0.9f);
        targetPos.y = Random.Range(0.1f, 0.9f);
        targetPos = mainCam.ViewportToWorldPoint(targetPos);

        while (t < 1)
        {
            if(doMove == false)
            {
                yield return null;
                continue;
            }

            t += Time.deltaTime / 2;

            transform.position = Vector2.Lerp(pos, targetPos, t);

            yield return null;
        }

        StartCoroutine(_Move());
    }
}

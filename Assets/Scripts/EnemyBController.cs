using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBController : Enemy
{
    private bool isPassedPlayer = false;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        transform.position = mainCam.ViewportToWorldPoint(new Vector3(Random.Range(0.2f, 0.8f), 1.1f, -mainCam.transform.position.z));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        CheckMove();

        if(isPassedPlayer == false)
        {
            transform.Translate((PlayerController.S.transform.position - transform.position).normalized.x * speed.x * Time.deltaTime, -speed.y * 0.5f * Time.deltaTime, 0);
            
            if (transform.position.y < PlayerController.S.transform.position.y)
                isPassedPlayer = true;
        }
        else
        {
            transform.Translate(Vector2.down * speed.y * 2f * Time.deltaTime);
        }

    }
}

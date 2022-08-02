using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWController : Enemy
{
    private ItemGenerator itemGenerator;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        itemGenerator = FindObjectOfType<ItemGenerator>();

        transform.position = mainCam.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 1.1f, -mainCam.transform.position.z));
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
        transform.Translate(Vector2.down * speed.y * Time.deltaTime);
    }

    bool isDead = false;
    protected override void Dead()
    {
        if (isDead == true)
            return;

        isDead = true;
        itemGenerator.AttackItemGeneration(this.transform.position);

        base.Dead();
    }
}

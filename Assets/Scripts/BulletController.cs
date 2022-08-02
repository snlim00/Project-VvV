using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Attack
{
    private Camera mainCam;

    private Vector2 watingPos;

    [SerializeField] private bool isFire = false;

    private TrailRenderer tr;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        mainCam = Camera.main;

        tr = GetComponent<TrailRenderer>();

        watingPos = mainCam.ViewportToWorldPoint(new Vector3(2000, 2000, -mainCam.transform.position.z));

        _Rest();

        trTime = tr.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _Rest()
    {
        isFire = false;
        transform.position = watingPos;
    }

    float trTime;
    public IEnumerator Rest()
    {
        tr.emitting = false;

        tr.time = 0;

        isFire = false;

        yield return null;
        transform.position = watingPos;
    }

    public void Fire(Damage damage, float speed, float angle, float range, Transform caster)
    {
        if (isFire == true)
        {
            Debug.LogWarning("사용 중인 총알을 사용하려고 시도했습니다.");
            return;
        }


        this.damage = damage;
        this.speed = speed;
        transform.eulerAngles = new Vector3(0, 0, angle);
        this.caster = caster.tag;
        StartCoroutine(_Fire(range, caster));
    }

    private IEnumerator _Fire(float range, Transform caster)
    {
        isFire = true;

        float movedDistance = 0;

        transform.position = caster.transform.position;

        yield return null;
        tr.emitting = true;
        tr.time = trTime;

        while (movedDistance <= range)
        {
            Vector2 moveVector = Vector2.up * speed * Time.deltaTime;
            transform.Translate(moveVector);
            movedDistance += moveVector.magnitude;

            yield return null;
        }

        if (this.caster != TAG.PLAYER)
            Destroy(this.gameObject);

        StartCoroutine(Rest());
    }

    protected override bool Collision(Collider2D collision)
    {
        if(base.Collision(collision) == true)
        {
            if(caster != TAG.PLAYER)
                    Destroy(this.gameObject);

            else
                StartCoroutine(Rest());
        }

        return false;
    }


    public void Fire(Damage damage, float speed, Vector2 targetPos, float range, Transform caster)
    {
        if (isFire == true)
        {
            Debug.LogError("사용 중인 총알을 사용하려고 시도했습니다.");
            return;
        }


        this.damage = damage;
        this.speed = speed;
        this.caster = caster.tag;
        StartCoroutine(_Fire(targetPos, range, caster));
    }

    private IEnumerator _Fire(Vector3 targetPos, float range, Transform caster)
    {
        isFire = true;

        float movedDistance = 0;

        transform.position = caster.transform.position;

        yield return null;
        tr.emitting = true;

        Vector2 dir = (targetPos - transform.position).normalized;

        while (movedDistance <= range)
        {
            Vector2 moveVector = dir * speed * Time.deltaTime;
            transform.Translate(moveVector);
            movedDistance += moveVector.magnitude;

            yield return null;
        }

        if (this.caster != TAG.PLAYER)
            Destroy(this.gameObject);

        StartCoroutine(Rest());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : Entity
{
    public static PlayerController S;

    [SerializeField] private GameObject turretPref;
    [SerializeField] private List<TurretController> turretList;

    [SerializeField] private Image expBar;
    [SerializeField] private Image HpBar;
    [SerializeField] private Image PpBar;
    [SerializeField] private Text HpText;
    [SerializeField] private Text PpText;
    [SerializeField] private Text scoreText;

    private float maxExp = 200, _curExp;
    public float curExp
    {
        get { return _curExp; }
        set
        {
            _curExp = value;

            if(_curExp > maxExp)
            {
                LevelUp();
            }

            UIRenewal();
        }
    }

    public float score = 0;

    public bool isGameOver = false;

    [SerializeField] private Vector2 playerSize;
    [SerializeField] private float maxPp;

    [SerializeField] public MoveProperties moveProp = new MoveProperties();
    [SerializeField] public FireProperties fireProp = new FireProperties();
    [SerializeField] public RingController ringCtrl;
    [SerializeField] private ItemGenerator itemGenerator;

    private float _curPp;
    public float curPp
    {
        get { return _curPp; }
        set
        {
            _curPp = value;

            if(_curPp > maxPp)
            {
                _curPp = maxPp;
                isGameOver = true;
                GameOver();
            }
            else if(_curPp < 0)
            {
                _curPp = 0;
            }

            UIRenewal();
        }
    }

    private bool doAttack = true;

    [SerializeField] private GameObject ringObject;

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        S = this;

        UIRenewal();
        curPp = GameInfo.stage == 1 ? 10 : 30;

        turretList = new List<TurretController>();
        shield = FindObjectOfType<ShieldController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveProp.Init();
        fireProp.Init();

        //ringCtrl.Upgrade();

        bulletGeneration();

        StartCoroutine(Fire());

        StartCoroutine(Turret());

        SetBullet();
    }

    void SetBullet()
    {
        int i = 0;

        while(i < fireProp.bulletCount)
        {
            fireProp.bulletArr[i].Fire(fireProp.damage, fireProp.speed, 0, 0.1f, this.transform);
            ++i;
        }
    }
    
    new private WaitForSeconds hitFlashWfs = new WaitForSeconds(0.1f);
    protected override IEnumerator HitFlash()
    {
        isGod = true;

        for(int i = 0; i < 7; ++i)
        {
            spr.color = hitColor;
            yield return hitFlashWfs;
            spr.color = defaultColor;
            yield return hitFlashWfs;
        }

        isGod = false;
    }

    void bulletGeneration()
    {
        for(int i = 0; i < fireProp.bulletCount; ++i)
        {
            fireProp.bulletArr[i] = Instantiate(fireProp.bulletPref).GetComponent<BulletController>();
        }
    }

    [SerializeField] private GameObject[] e;
    public float scoreMultiplier = 10;
    // Update is called once per frame
    void Update()
    {
        Move();
        SetAttack();

        if (isGameOver == false)
            score += Time.deltaTime * scoreMultiplier;

        if (score < 0)
            score = 0;



        scoreText.text = Convert.ToString((int)score);

        if(Input.GetKeyDown(KeyCode.G))
        {
            if(isGod == false) isGod = true;
            else if(isGod == true) isGod = false;
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            curHp = maxHp;
            curPp = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
            fireProp.Upgrade();

        if (Input.GetKeyDown(KeyCode.X))
            ringCtrl.Upgrade();

        if (Input.GetKeyDown(KeyCode.X))
            TurretUpgrade();

        if (Input.GetKeyDown(KeyCode.V))
            StartCoroutine(Shield());

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameOver();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale > 0)
                Time.timeScale = 0;
            else if (Time.timeScale != 1)
                Time.timeScale = 1;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            GameObject[] enemy = GameObject.FindGameObjectsWithTag(TAG.ENEMY);
            for(int i = 0; i < enemy.Length; ++i)
            {
                Destroy(enemy[i]);
            }
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            Instantiate(e[0]);
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            Instantiate(e[1]);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            isGameOver = true;
            GameOver();
        }
    }

    void Move()
    {
        if(doMove == true)
            transform.Translate(Input.GetAxisRaw("Horizontal") * moveProp.speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * moveProp.speed * Time.deltaTime, 0);

        Vector3 pos = mainCam.WorldToViewportPoint(transform.position);

        if (pos.x > 1 - playerSize.x) pos.x = 1 - playerSize.x; 
        else if (pos.x < 0 + playerSize.x) pos.x = 0 + playerSize.x; 
        if (pos.y > 1 - playerSize.y) pos.y = 1 - playerSize.y; 
        else if (pos.y < 0 + playerSize.y) pos.y = 0 + playerSize.y;

        transform.position = mainCam.ViewportToWorldPoint(pos);
    }

    void SetAttack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            doAttack = doAttack == true ? false : true;
        }
    }

    IEnumerator Fire()
    {
        int i = 0;

        while(true)
        {
            if (doAttack == false)
            {
                yield return fireProp.wfsDelay;
                continue;
            }

            for(int j = 0; j < fireProp.level + 1; ++j)
            {
                fireProp.bulletArr[i].Fire(fireProp.damage, fireProp.speed, fireProp.angle[fireProp.level, j], fireProp.range, this.transform);
                ++i;

                if (i >= fireProp.bulletCount)
                    i = 0;
            }

            
            yield return fireProp.wfsDelay;
        }
    }

    public override void TakeDamage(Damage dmg)
    {
        base.TakeDamage(dmg);

        UIRenewal();
    }

    void UIRenewal()
    {
        expBar.fillAmount = curExp / maxExp;

        HpBar.fillAmount = curHp / maxHp;
        PpBar.fillAmount = curPp / maxPp;

        HpText.text = "HP " + (int)curHp + " / " + maxHp;
        PpText.text = "PP " + (int)curPp + " / " + maxPp;
    }

    public void PpHeal(float heal)
    {
        curPp -= heal;
        maxPp += 3;
        if(curPp < 0)
        {
            curPp = 0;
        }
    }

    void LevelUp()
    {
        float overExp = curExp - maxExp;

        curExp = 0;
        maxExp *= 1.2f;

        itemGenerator.ItemGeneration();

        curExp += overExp;
    }

    public void TurretUpgrade()
    {
        turretLevel += 1;
        if(turretLevel > 3)
        {
            turretLevel = 2;
        }
    }

    public int turretLevel = -1;
    public float turretCooldown = 2;
    IEnumerator Turret()
    {
        float t = 0;
        float turretCount = 0;
        WaitForSeconds wfs = new WaitForSeconds(turretCooldown);

        while(true)
        {
            t += Time.deltaTime;
            if(t > turretCooldown)
            {
                t = 0;

                for(int i = 0; i < turretLevel + 1; ++i)
                {
                    turretList.Add(Instantiate(turretPref).GetComponent<TurretController>());

                    float x = UnityEngine.Random.Range(0.1f, 0.9f);
                    float playerY = mainCam.WorldToViewportPoint(transform.position).y;
                    float y = UnityEngine.Random.Range(0.75f, 0.9f);
                    Vector3 pos = mainCam.ViewportToWorldPoint(new Vector3(x, y, -mainCam.transform.position.z));
                    turretList[turretList.Count - 1].Fire(pos, this.transform);
                }
                turretCount += 1;

                if(turretCount >= 3)
                {
                    yield return wfs;

                    for(int i = 0; i < turretList.Count; ++i)
                    {
                        StartCoroutine(turretList[i].Return());
                    }

                    turretList.Clear();

                    turretCount = 0;
                }
            }


            yield return null;
        }
    }

    private ShieldController shield;
    public IEnumerator Shield()
    {
        //shield.gameObject.SetActive(true);
        shield.isRest = false;
        isGod = true;

        yield return new WaitForSeconds(2.5f);
        StartCoroutine(shield.Bomb());
        StartCoroutine(HitFlash());
    }

    protected override void Dead()
    {
        isGameOver = true;
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        GameOver();
    }

    public void GameOver()
    {
        moveProp.speedMultiplier = 0;
        fireProp.doAttack = false;

        score = score * ((curHp / 100) + 0.5f);
        score = score * (((100 - curPp) / 100) + 0.5f);

        if(isGameOver == true)
        {
            ChangeScene(SCENE.RESULT);
        }
        else if (GameInfo.stage == 1)
        {
            GameInfo.firstStageScore = score;
            if (curHp > 0)
            {
                GameInfo.stage = 2;
                StartCoroutine(ChangeScene(SCENE.START_SCENE));
                return;
            }
        }
        else if (GameInfo.stage == 2)
        {

            GameInfo.secondStageScore = score;
        }

        StartCoroutine(ChangeScene(SCENE.RESULT));
    }

    [SerializeField] private Image cover;
    IEnumerator ChangeScene(string scene)
    {
        float t = 0;

        while (t < 1)
        {
            Debug.Log(t);
            t += Time.deltaTime;

            cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, Mathf.Lerp(0, 1, t));
            Debug.Log(Mathf.Lerp(0, 255, t));

            yield return null;
        }

        SceneManager.LoadScene(scene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : Attack
{
    public GameObject ringPref;
    public GameObject[] ringObject = new GameObject[5];

    public int maxLevel = 4;
    public int level = -1;
    public int[,] angle = { { 0, 0, 0, 0, 0 }, { 0, 180, 0, 0, 0 }, { 0, 120, 240, 0, 0 }, { 0, 90, 180, 270, 0 }, { 0, 72, 144, 216, 288 } };


    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        caster = TAG.PLAYER;
    }

    public void Upgrade()
    {
        this.level += 1;

        if (this.level > maxLevel)
        {
            this.level = maxLevel;
            return;
        }

        ringObject[level] = Instantiate(ringPref) as GameObject;
        ringObject[level].transform.SetParent(this.transform);

        for (int i = 0; i < level + 1; ++i)
        {
            ringObject[i].transform.localPosition = Vector3.zero;
            ringObject[i].transform.localEulerAngles = new Vector3(0, 0, angle[level, i]);
            //ebug.Log(angle[level, i]);
            ringObject[i].transform.Translate(Vector2.up * 2.5f);
        }

        speed = 270 - (20 * level);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerController.S.transform.position;
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    
}

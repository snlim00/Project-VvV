using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private float speed = 2;

    public bool spawnedByEnemy = false;

    private Camera mainCam;

    private PlayerController playerCtrl;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        mainCam = Camera.main;
    }

    public void SetPosition(int lane)
    {
        spawnedByEnemy = false;
        transform.position = mainCam.ViewportToWorldPoint(new Vector3(0.25f * (lane + 1), 1.1f, -mainCam.transform.position.z));
    }

    public void SetPosition(Vector2 pos)
    {
        spawnedByEnemy = true;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void Collision(Collider2D collision)
    {
        if (collision.tag != TAG.PLAYER)
            return;

        GiveItem();

        RemoveItem();
    }

    protected abstract void GiveItem();

    protected void RemoveItem()
    {
        PlayerController.S.score += 200;

        if(spawnedByEnemy == true)
        {
            Destroy(this.gameObject);
            return;
        }

        GameObject[] itemGo = GameObject.FindGameObjectsWithTag(TAG.ITEM);
        Item[] itemArr = new Item[itemGo.Length];
        for(int i = 0; i < itemGo.Length; ++i)
        {
            itemArr[i] = itemGo[i].GetComponent<Item>();
        }

        for(int i = 0; i < itemArr.Length; ++i)
        {
            if (itemArr[i].gameObject != this.gameObject && itemArr[i].spawnedByEnemy == false)
                Destroy(itemArr[i].gameObject);

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collision(collision);   
    }
}

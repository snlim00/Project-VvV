using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Damage damage;
    public float speed;

    public string caster;

    protected virtual void Init()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != TAG.BORDER && collision.tag != caster)
        {
            Collision(collision);
        }
    }

    protected virtual bool Collision(Collider2D collision)
    {
        if (collision.tag != TAG.BORDER && collision.tag != caster && collision.tag != this.tag && collision.tag != TAG.ITEM)
        {
            collision.GetComponent<Entity>().TakeDamage(damage);
            return true;
        }
        else
            return false;
    }
}

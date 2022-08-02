using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Damage
{
    public float damage;
    public float knockbackPower;
    public float knockbackDuration;
    public Vector2 knockbackDirection;
    public float stunDuration;

    public Damage(float dmg)
    {
        damage = dmg;

        knockbackPower = 0;
        knockbackDuration = 0;
        knockbackDirection = Vector2.zero;
        stunDuration = 0;
    }

    public Damage(float dmg, float knPower, float knDur, Vector2 knDir)
    {
        damage = dmg;
        knockbackPower = knPower;
        knockbackDuration = knDur;
        knockbackDirection = knDir;

        stunDuration = 0;
    }
    
    public Damage(float dmg, float knPower, float knDur, Vector2 knDir, float stDur)
    {
        damage = dmg;
        knockbackPower = knPower;
        knockbackDuration = knDur;
        knockbackDirection = knDir;
        stunDuration = stDur;
    }
}

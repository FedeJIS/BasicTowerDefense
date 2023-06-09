using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float CauseDamage();

    void ReceiveDamage(float damage);
}

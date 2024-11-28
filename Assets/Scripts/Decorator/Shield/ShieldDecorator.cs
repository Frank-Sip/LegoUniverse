using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDecorator : IDamageable
{
    private readonly IDamageable _decoratedEntity;
    private float _shieldAmount;

    public float ShieldAmount => _shieldAmount;

    public ShieldDecorator(IDamageable decoratedEntity, float shieldAmount)
    {
        _decoratedEntity = decoratedEntity;
        _shieldAmount = shieldAmount;
    }

    public void TakeDamage(float damage)
    {
        if (_shieldAmount > 0)
        {
            if (damage <= _shieldAmount)
            {
                _shieldAmount -= damage;
                return;
            }
            else
            {
                damage -= _shieldAmount;
                _shieldAmount = 0;
            }
        }
        
        _decoratedEntity.TakeDamage(damage);
    }
    
    public float GetShieldAmount()
    {
        return _shieldAmount;
    }
}



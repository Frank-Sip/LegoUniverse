using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IEnemyFactory
{
    [SerializeField] private Enemy enemyPrefab;
    private ObjectPool<Enemy> enemyPool;

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(enemyPrefab);
    }

    public void Initialize(Enemy enemyPrefab)
    {
        this.enemyPrefab = enemyPrefab;
        enemyPool = new ObjectPool<Enemy>(enemyPrefab);
    }
    
    public GameObject CreateEnemy(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = enemyPool.GetFromPool(position, rotation);
        enemy.healthComponent.HealToMax();
        return enemy.gameObject;
    }

    public void ReturnToPool(Enemy enemy)
    {
        enemyPool.ReturnToPool(enemy);
    }
}
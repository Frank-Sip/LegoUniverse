using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
    public abstract GameObject Create(Vector3 position, Quaternion rotation);
    public abstract void ReturnToPool(GameObject obj);
}

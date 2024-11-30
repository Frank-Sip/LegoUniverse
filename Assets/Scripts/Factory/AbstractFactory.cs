using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory
{
    public abstract GameObject Create(Vector3 position, Quaternion rotation);
}

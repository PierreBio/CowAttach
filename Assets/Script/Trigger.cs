using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    [SerializeField] protected BreakableObstacle m_breakableObstacle;

    protected abstract void OnTriggerEnter(Collider _other);

    protected abstract void OnTriggerExit(Collider _other);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Species
{
    COW
}
public abstract class Animal : MonoBehaviour
{
    protected Species m_species;
    protected float m_speed;

    public abstract void Init();
    public abstract void Targeted();
    public abstract void SetDestination();

    public abstract void Kill();
    public Species Species
    {
        get
        {
            return m_species;
        }
    }
}

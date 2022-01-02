using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BreakableObstacleStatus
{
    FULL,
    DEAD
}
public abstract class BreakableObstacle : MonoBehaviour
{
    protected float m_health;
    protected float m_baseHealth;
    protected BreakableObstacleStatus m_status;
    protected List<Animal> _onObstacle;

    protected void Awake()
    {
        _onObstacle = new List<Animal>();
    }

    protected virtual void Start()
    {
        m_baseHealth = m_health;
        SwitchStatus();
    }

    protected virtual void CustomUpdate()
    {
        _onObstacle.RemoveAll(item => item == null);

    }
    public abstract void IsDamaged(bool _damaged, Animal _animal);
    public abstract void Destroyed();

    protected void SwitchStatus()
    {
        switch (m_health)
        {
            case float n when n > 0:
                m_status = BreakableObstacleStatus.FULL;
                break;
            case float n when n <= 0:
                m_status = BreakableObstacleStatus.DEAD;
                break;
        }
    }
    public BreakableObstacleStatus Status
    {
        get
        {
            return m_status;
        }
    }

}

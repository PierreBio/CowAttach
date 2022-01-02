using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer: MonoBehaviour
{
    private float m_targetTime;
    private float m_currentTime;
    private bool m_status;

    private void Awake()
    {
        m_status = false;
        m_targetTime = 0;
        m_currentTime = 0;
    }
    public void Init(float _targetTime)
    {
        m_targetTime = _targetTime;
        m_currentTime = m_targetTime;
    }

    void Update()
    {
        if (m_status)
        {
            m_currentTime -= Time.deltaTime;

            if (m_currentTime <= 0.0f)
            {
                TimerEnded();
            }
        }
    }

    private void TimerEnded()
    {
        GameManager.Instance.EndGame();
    }

    public void StartTimer()
    {
        m_status = true;
    }

    public void PauseTimer()
    {
        m_status = false;
    }
    public float TargetTime
    {
        get
        {
            return m_targetTime;
        }
    }

    public float CurrentTime
    {
        get
        {
            return m_currentTime;
        }
    }

   
}

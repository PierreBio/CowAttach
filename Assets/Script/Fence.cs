using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fence : BreakableObstacle
{
    public GameObject m_destroyPrefab;

    private AudioSource m_source;
    private int randomX;
    
    protected override void Start()
    {
        m_health = GameManager.Instance.HealthFence;
        base.Start();
        m_source = GetComponent<AudioSource>();
        InvokeRepeating("CustomUpdate", 0, 1f);
    }

    // Update is called once per frame
    protected override void CustomUpdate()
    {
        base.CustomUpdate();

        if (_onObstacle.Count > 0 && m_health > 0)
        {
            m_health -= Mathf.Log(_onObstacle.Count * GameManager.Instance.DamagePerSecond, GameManager.Instance.LogTuning);
            if (!m_source.isPlaying) {
                randomX = Random.Range(0,3);
                if (randomX<=1) {
                    SoundManager.m_instance.PlaySoundFX(m_source, SoundManager.SoundFX.FenceDamage);
                }
            }
        }
        else if (m_health <= 0 && m_status != BreakableObstacleStatus.DEAD)
        {
            Destroyed();
            SwitchStatus();
        }
    }

    public override void IsDamaged(bool _damaged, Animal _animal)
    {
        if (_damaged)
        {
            if (!_onObstacle.Contains(_animal))
            {
                _onObstacle.Add(_animal);
            }
        }
        else
        {
            if (_onObstacle.Contains(_animal))
            {
                _onObstacle.Remove(_animal);
            }
        }
    }

    public override void Destroyed()
    {
        GameManager.Instance.Fences.Remove(this);

        GameObject destroy = GameObject.Instantiate(m_destroyPrefab, transform.position, transform.rotation) as GameObject;
        SoundManager.m_instance.PlaySoundFX(destroy.GetComponent<AudioSource>(), SoundManager.SoundFX.FenceDestroy);
        gameObject.SetActive(false);
        for (int i = 0; i < _onObstacle.Count; i++)
        {
            if (_onObstacle[i].GetComponent<NavMeshAgent>())
            {
                _onObstacle[i].GetComponent<NavMeshAgent>().isStopped = false;
            }
        }
    }
}

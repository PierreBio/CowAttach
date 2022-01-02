using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropField : BreakableObstacle
{
    public GameObject m_destroyPrefab;
    public List<GameObject> m_crops;
    private int m_cropsTotal;
    private AudioSource m_source;
    private int randomX;
    
    protected override void Start()
    {
        m_health = GameManager.Instance.HealthCropField;
        m_cropsTotal = m_crops.Count;
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
            DisplayCrops(CropsToShow());
            m_health -= Mathf.Log(_onObstacle.Count * GameManager.Instance.DamagePerSecond, GameManager.Instance.LogTuning);
            if (!m_source.isPlaying) {
                randomX = UnityEngine.Random.Range(0,3);
                if (randomX<=1) {
                    SoundManager.m_instance.PlaySoundFX(m_source, SoundManager.SoundFX.FieldDamage);
                }
            }
        }
        else if(m_health <= 0 && m_status != BreakableObstacleStatus.DEAD)
        {
            Destroyed();
            SwitchStatus();
        }
    }

    private int CropsToShow()
    {
        return (int)Math.Ceiling((m_health * (m_cropsTotal)) / m_baseHealth);
    }

    private void DisplayCrops(int _toDisplay)
    {
        for(int i = 1; i <= m_crops.Count; i++)
        {
            m_crops[i - 1].SetActive(!(i > _toDisplay));
        }
    }

    public override void IsDamaged(bool _damaged, Animal _animal)
    {
        if (_damaged) {
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
        GameManager.Instance.CropFields.Remove(this);

        GameObject destroy = GameObject.Instantiate(m_destroyPrefab, transform.position, transform.rotation) as GameObject;
        SoundManager.m_instance.PlaySoundFX(destroy.GetComponent<AudioSource>(), SoundManager.SoundFX.FieldDestroy);
        gameObject.SetActive(false);    
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundExplosion : MonoBehaviour
{
    private AudioSource m_source;
    
    private void Awake()
    {
        m_source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.m_instance.PlaySoundFX(m_source, SoundManager.SoundFX.Expl);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

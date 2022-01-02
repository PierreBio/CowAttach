using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    private int m_score;
    private bool m_endGame = false;
    private bool m_pauseGame = false;

    [Header("Game Settings")]
    [SerializeField] private float m_initSpawnRate;
    [SerializeField] private int m_rateBetweenTwoRound;

    [Header("Game Component")]
    [SerializeField] private PlayerAction m_player;
//    [SerializeField] private Timer m_timer;
    [SerializeField] private List<CropField> m_cropFields;
    [SerializeField] private List<Fence> m_fences;
    [SerializeField] private List<AnimalSpawner> m_animalSpawns;

    [SerializeField] private AnimalWaveSpawner m_animalSpawn;

    [SerializeField] private List<Animal> m_allAnimals;

    [SerializeField] private float m_damagePerSecond;
    [SerializeField] private float m_healthCropField;
    [SerializeField] private float m_healthFences;
    [SerializeField] private float m_logTuning;
    [SerializeField] private float m_cowSpeed;
    
    [SerializeField] private int m_maxAnimalCount;

    [SerializeField] private WavePanel m_wavePanel;
    public PanelType PanelType;

    private int m_waveCount;

    public int WaveCount
    {
        get
        {
            return m_waveCount;
        }
        set
        {
            m_waveCount = value;
        }
    }

    [SerializeField] private Transform m_animalContainer;


    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }

        PanelType = PanelType.WAVE;
        m_allAnimals = new List<Animal>();
        m_score = 0;
        m_waveCount = 1;
    }

    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        //m_wavePanel.Display(true);
        StartWave(false);
    }

    private void Update() {

        if (m_cropFields.Count == 0 && !m_endGame)
        {
            EndGame();
            m_endGame = true;
        }
        else if(m_cropFields.Count > 0)
        {
            if (m_allAnimals.Count < 1 && m_animalSpawn.Ended)
            {
                //m_wavePanel.Display(true);
                StartWave(false);
            }
        }
    }

    public void StartWave(bool _start)
    {
        if (_start && PauseGame)
        {
            SoundManager.m_instance.PlayWaveSound();
            PauseGame = false;
            PanelType = PanelType.WAVE;
            m_wavePanel.Animator.SetBool("In", false);
            m_animalSpawn.ActivateSpawner(true);
        }
        else if(!_start && !PauseGame)
        {
            SoundManager.m_instance.PlayAmbienceSound();
            PauseGame = true;
            PanelType = PanelType.WAVE;
            m_wavePanel.Animator.SetBool("In", true);
        }
    }
    public void EndGame()
    {
        m_animalSpawn.ActivateSpawner(false);
        KillAll();
        PanelType = PanelType.END;
        m_wavePanel.Animator.SetBool("In", true);
        //SceneManager.LoadScene("SceneMenu");
    }

    public void LoadScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }
    public void AddScore(int _score)
    {
        m_score += _score;
    }

    public static GameManager Instance { 
        get {
            if (!m_instance)
            {
                m_instance = new GameManager();
            }
            return m_instance; 
        } 
    }

    public void KillAll()
    {
        for (int i = AllAnimals.Count - 1; i >= 0; i--)
        {
            AllAnimals[i].Kill();
        }
    }
    public PlayerAction Player { get { return m_player; } }
    public List<CropField> CropFields { get { return m_cropFields; } }
    public List<Fence> Fences { get { return m_fences; } }

    public List<Animal> AllAnimals { get { return m_allAnimals; } }

    public float DamagePerSecond { get { return m_damagePerSecond; } }
    public float HealthCropField { get { return m_healthCropField; } }
    public float HealthFence { get { return m_healthFences; } }

    public float LogTuning { get { return m_logTuning; } }
    public float CowSpeed { get { return m_cowSpeed; } }
    public float InitSpawnRate { get { return m_initSpawnRate; } }
    public int MaxAnimalCount { get { return m_maxAnimalCount; } }
    public Transform AnimalContainer { get { return m_animalContainer; } }
    public int RateBetweenTwoRound { get { return m_rateBetweenTwoRound; } }
    public bool PauseGame { get { return m_pauseGame; } set { m_pauseGame = value; } }


}

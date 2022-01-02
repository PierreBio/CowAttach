using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnerParameter : MonoBehaviour
{
    public static GameObject[] spawnPositions = null;
    
    public List<Animal> _prefabsAnimals;
    float m_elapsed = 0f;
    float m_spawnRate = 0f;
    private bool m_activated;
    
    public float spawnTimeMaxRate = 100;
    public float incrementalRate = 5;
    
    public AnimationCurve spawnTime;
    
    // Start is called before the first frame update
    private void Awake()
    {
        m_activated = false;
    }

    private void Start()
    {
        m_spawnRate = GameManager.Instance.InitSpawnRate;
        StartCoroutine("SpawningUpdate");
    }

    private IEnumerator SpawningUpdate()
    {
        float time = 0;
        while (m_activated && GameManager.Instance.AllAnimals.Count < GameManager.Instance.MaxAnimalCount)
        {
            yield return new WaitForSeconds(spawnTime.Evaluate(time/spawnTimeMaxRate));
            SpawnAnimal(Species.COW);
            if (time<spawnTimeMaxRate) 
            {
                time+=incrementalRate;
            }
        }
    }

    private void SpawnAnimal(Species _species)
    {
        Animal newAnimal = null;
        switch (_species)
        {
            case Species.COW:
                newAnimal = Instantiate(
                    _prefabsAnimals[UnityEngine.Random.Range(0, _prefabsAnimals.Count - 1)], 
                    spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length - 1)].gameObject.transform.position , 
                    Quaternion.identity) as Cow;
                newAnimal.transform.parent = transform;
                newAnimal.Init();
                break;
        }
    }

    public void ActivateSpawner(bool _activate)
    {
        m_activated = _activate;
    }

    public float SpawnRate
    {
        set
        {
            m_spawnRate = value;
        }
        get
        {
            return m_spawnRate;
        }
    }
}

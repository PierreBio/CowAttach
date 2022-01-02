using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public List<Animal> _prefabsAnimals;
    float m_elapsed = 0f;
    float m_spawnRate = 0f;
    private bool m_activated;
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
        Debug.Log("SpawningUpdate");
        while (m_activated && GameManager.Instance.AllAnimals.Count < GameManager.Instance.MaxAnimalCount)
        {
            yield return new WaitForSeconds(m_spawnRate);
            SpawnAnimal(Species.COW);
        }
    }

    private void SpawnAnimal(Species _species)
    {
        Animal newAnimal = null;
        switch (_species)
        {
            case Species.COW:
                newAnimal = Instantiate(_prefabsAnimals[0], transform.position , Quaternion.identity, GameManager.Instance.AnimalContainer) as Cow;
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

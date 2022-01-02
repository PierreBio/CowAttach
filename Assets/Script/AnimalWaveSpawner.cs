using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimalWaveSpawner : MonoBehaviour
{
    
    public float cowsSpawnRate = 0.2f;

    public int waves = 5;
    public int cowsPerWave = 5;
    public int addCowsPerWave = 1;
    
    public List<Animal> _prefabsAnimals;
    float m_elapsed = 0f;
    float m_spawnRate = 0f;
    float m_roundRate = 0f;
    private bool m_activated;
    private List<Transform> spawnPositions = null;
    private bool m_init = false;
    public bool Ended;
    private void Awake()
    {
        m_activated = false;
    }

    private IEnumerator SpawningUpdate() 
    {
        //Debug.Log("SpawningUpdate");
       for(int i = 0; i < GameManager.Instance.WaveCount; i++) 
       {
            yield return StartCoroutine("SpawningWave");  
       }
        ActivateSpawner(false);
    }

    private IEnumerator SpawningWave()
    {
        for (int i = 0; i < waves; i++) 
        {
            yield return SpawnAnimals();
        }
        cowsPerWave += addCowsPerWave;
        yield return new WaitForSeconds(m_roundRate);
    }

    private IEnumerator SpawnAnimals() 
    {
        foreach (Transform tran in GetRandomItemsFromList(spawnPositions, cowsPerWave)) 
        {
            SpawnAnimal(Species.COW, tran.position);
            yield return new WaitForSeconds(cowsSpawnRate);
        }
        yield return new WaitForSeconds(m_spawnRate);
    }

    private void SpawnAnimal(Species _species, Vector3 spawnPosition)
    {
        Animal newAnimal = null;
        switch (_species) {
            case Species.COW:
                newAnimal = Instantiate(
                    _prefabsAnimals[UnityEngine.Random.Range(0, _prefabsAnimals.Count - 1)],
                    spawnPosition, Quaternion.identity) as Cow;
                newAnimal.Init();
                break;
        }
    }

    public void Init()
    {
        spawnPositions = allSpawnPoint(transform);
        m_roundRate = GameManager.Instance.RateBetweenTwoRound;
        m_spawnRate = GameManager.Instance.InitSpawnRate;
        m_init = true;
    }

    public void ActivateSpawner(bool _activate)
    {
        if (!m_init)
        {
            Init();
        }

        if (_activate)
        {
            Ended = false;
            StartCoroutine("SpawningUpdate");
        }
        else
        {
            Ended = true;
            GameManager.Instance.WaveCount++;
            StopCoroutine("SpawningUpdate");
        }
    }

    public static List<T> GetRandomItemsFromList<T> (List<T> list, int number)
    {
        List<T> tmpList = new List<T>(list);
        List<T> newList = new List<T>();
        while (newList.Count < number && tmpList.Count > 0)
        {
            int index = Random.Range(0, tmpList.Count);
            newList.Add(tmpList[index]);
            tmpList.RemoveAt(index);
        }
        return newList;
    }
    
    private List<Transform> allSpawnPoint(Transform parent) {
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        List<Transform> listReturn = new List<Transform>();
        foreach (Transform child in allChildren)
        {
           if (child.GetComponentsInChildren<Transform>().Length == 1) {
//                Debug.Log(child.gameObject.name);
                listReturn.Add(child);
            }
        }

        return listReturn;
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
    
    void OnDrawGizmos() {
        spawnPositions = allSpawnPoint(transform);
        if (spawnPositions != null) {
            foreach (Transform point in spawnPositions) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(point.position, 0.15f);
            }
        }
    }
    
}

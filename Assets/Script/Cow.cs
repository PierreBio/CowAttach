using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cow : Animal
{
    private NavMeshAgent m_pathfinder;
    [SerializeField] private BoxCollider m_collider;
    private CropField m_target;

    [SerializeField] private Rigidbody m_rigidBody;

    private bool m_isInit;
    private AudioSource m_source;
    private int randomX;
    private void Awake()
    {
        m_pathfinder = GetComponentInChildren<NavMeshAgent>();
        m_source = GetComponent<AudioSource>();
        m_isInit = false;
        //m_rigidBody.detectCollisions = false;
    }

    private void Update()
    {
        if (m_isInit)
        {
            if(m_target?.Status == BreakableObstacleStatus.DEAD)
            {
                SetDestination();
            }
        }
        if (!m_source.isPlaying) {
            randomX = Random.Range(0,1000);
            if (randomX<=1) {
                SoundManager.m_instance.PlaySoundFX(m_source, SoundManager.SoundFX.Cows);
            }
        }
    }
    public override void Init()
    {
        m_species = Species.COW;
        m_speed = GameManager.Instance.CowSpeed;
        m_pathfinder.speed = m_speed;
        SetDestination();
        GameManager.Instance.AllAnimals.Add(this);
        m_isInit = true;
    }

    public override void Targeted()
    {
        StartCoroutine(Explode());
    }

    public  IEnumerator Explode()
    {
        Debug.Log("COW TEST");
        m_pathfinder.enabled = false;
        m_rigidBody.isKinematic = false;
        Vector3 direction = new Vector3((float)Random.Range(-10, 10), 25, (float)Random.Range(-10, 10));

        m_rigidBody.AddForce(direction, ForceMode.Impulse);
        GameManager.Instance.AllAnimals.Remove(this);
        yield return new WaitForSeconds(2);
        Kill();
    }

    public override void Kill()
    {
        GameManager.Instance.AllAnimals.Remove(this);
        Destroy(gameObject);
    }
    public override void SetDestination()
    {
        float bestDistance = float.PositiveInfinity;
        CropField newTarget = null;
        
        if (GameManager.Instance.CropFields.Count > 0)
        {
            for(int i = 0; i < GameManager.Instance.CropFields.Count; i++)
            {
                NavMeshPath path = new NavMeshPath();
                NavMeshHit hit;
                //Debug.Log("START PATH =" + NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas));
                NavMesh.CalculatePath(transform.position, GameManager.Instance.CropFields[i].transform.position, NavMesh.AllAreas, path);
                //Debug.Log("CALCULATE PATH =" + );
                if(path != null && path.status != NavMeshPathStatus.PathInvalid) {
                    float pathDistance = CalculatePathValue(path);
                    if (pathDistance < bestDistance)
                    {
                        bestDistance = pathDistance;
                        newTarget = GameManager.Instance.CropFields[i];
                    }
                }
            }

            if(newTarget != null)
            {
                m_target = newTarget;
                m_pathfinder.SetDestination(m_target.transform.position);
            }
        }
    }

    public float CalculatePathValue(NavMeshPath _path)
    {
        float distance = 0;
        if (_path.corners.Length > 1) {
            for (int i = 1; i < _path.corners.Length; i++)
            {
                distance += Vector3.Distance(_path.corners[i - 1], _path.corners[i]);
            }
        }
        return distance;
    }
}

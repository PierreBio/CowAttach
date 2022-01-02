using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{
    public GameObject m_target;
    public Vector3 m_targetPosition;
    public GameObject collisionExplosion;
    public float speed;


    // Update is called once per frame
    void Update()
    {
        // transform.position += transform.forward * Time.deltaTime * 300f;// The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        if (m_targetPosition != null)
        {
            if (transform.position == m_targetPosition)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, step);
        }

    }

    public void setTarget(Vector3 _targetPosition, GameObject _target)
    {
        m_target = _target;
        m_targetPosition = _targetPosition;
    }

    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation);
            m_target.SendMessage("Targeted");
            Destroy(explosion, 1f);
            Destroy(gameObject);
        }
    }

}
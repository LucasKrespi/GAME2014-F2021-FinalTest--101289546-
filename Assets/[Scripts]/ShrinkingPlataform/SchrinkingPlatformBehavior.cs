/*
Student: Lucas Krespi dos Santos
ID: 101289546
Version: 1.0

Final Exame Mobile Game Development Fall 2021.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrinkingPlatformBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject m_goPlatform;
    [SerializeField]
    private Transform m_trasformPlataform;

    [SerializeField]
    private Transform maxAmplitude;
    [SerializeField]
    private Transform minAmplitude;

    private bool isPlayerInPlatform;
    private bool hasNormalSize = true;


    public bool m_bGoingUp;

    public AudioSource[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        m_goPlatform = gameObject;
        m_trasformPlataform = GetComponent<Transform>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (isPlayerInPlatform)
        {
            m_trasformPlataform.localScale *= 0.98f;

            if(m_trasformPlataform.localScale.y < 0.2f)
            {
                m_goPlatform.GetComponent<Collider2D>().enabled = false;
                isPlayerInPlatform = false;
            }
        }
        else
        {
           
            if (!hasNormalSize)
            {
                m_trasformPlataform.localScale *= 1.02f;

                if (m_trasformPlataform.localScale.y >= 1.0f)
                {
                    m_goPlatform.GetComponent<Collider2D>().enabled = true;
                    hasNormalSize = true;
                    sounds[1].Stop();
                }
            }
           
        }

        if (m_bGoingUp)
        {
            m_trasformPlataform.position = new Vector3(m_trasformPlataform.position.x, Mathf.Lerp(m_trasformPlataform.position.y, maxAmplitude.position.y, 0.01f), m_trasformPlataform.position.z);
           
            if(m_trasformPlataform.position.y >= maxAmplitude.position.y - 0.1)
            {
                m_bGoingUp = false;
            }
        }
        else
        {
            m_trasformPlataform.position = new Vector3(m_trasformPlataform.position.x, Mathf.Lerp(m_trasformPlataform.position.y, minAmplitude.position.y, 0.01f), m_trasformPlataform.position.z);
            if (m_trasformPlataform.position.y <= minAmplitude.position.y + 0.1)
            {
                m_bGoingUp = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInPlatform = true;
            hasNormalSize = false;
            sounds[0].Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInPlatform = false;
            sounds[1].Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorHandler : MonoBehaviour
{
    private Animator m_Animator;
    private NavMeshAgent m_Agent;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponentInParent<NavMeshAgent>();
        m_Animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Agent != null && m_Animator != null)
        {
            m_Animator.SetFloat("Speed", m_Agent.velocity.magnitude / m_Agent.speed);
        }
    }
}

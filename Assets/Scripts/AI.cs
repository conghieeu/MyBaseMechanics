using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] Transform player;
    NavMeshAgent agent;
    Animator anim;
    State currentStage;

    void Start()
    {
        agent=this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        currentStage = new Idle(this.gameObject, agent, anim, player);
    }

    // Update is called once per frame
    void Update()
    {
        currentStage = currentStage.Process();
        
    }
}

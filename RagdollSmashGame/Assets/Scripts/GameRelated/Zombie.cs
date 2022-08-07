using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] Transform movePosTrans;
    [SerializeField] Animator anim;
    NavMeshAgent navMeshAgent;

    string curTriggerName;
    bool stillAlive = true;
    private void OnEnable()
    {
        UpdateManager.onUpdate += myUpdate;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        UpdateManager.onUpdate -= myUpdate;
    }

    public void myUpdate()
    {
        Move();
    }

    void Move()
    {
        if(!stillAlive) return;
        
        if (Vector3.Distance(transform.position, movePosTrans.position) < 1f)
        {
            changeAnimState("DynIdle");
            return;
        }
        navMeshAgent.destination = movePosTrans.position;
        changeAnimState("Running");

    }

    void changeAnimState(string state)
    {
        switch (state)
        {
            case "Running":
                if (curTriggerName == state) break;
                changeAnim(state);
                break;
            case "DynIdle":
                if (curTriggerName == state) break;
                changeAnim(state);
                break;
            case "Death":
                if (curTriggerName == state) break;
                changeAnim(state);
                break;
        }
    }
    void changeAnim(string state)
    {
        anim.SetTrigger(state);
        curTriggerName = state;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "RagdollBullet")
        {
            print("Hit");
            stillAlive = false;
            changeAnimState("Death");
            Destroy(this.gameObject, 2f);
        }
    }

}

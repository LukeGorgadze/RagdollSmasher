using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer rend;
    [SerializeField] public Color color;
    [SerializeField] GameObject ordMesh;
    [SerializeField] GameObject zombieRag;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] float bulletHitForce;

    NavMeshAgent navMeshAgent;

    string curTriggerName;
    bool stillAlive = true;

    private void Start()
    {
        rend.material.color = color;
    }
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
        if (!stillAlive) return;

        if (Vector3.Distance(transform.position, ReferenceManager.instance.movePosTrans.position) < 1f)
        {
            changeAnimState("DynIdle");
            return;
        }
        navMeshAgent.destination = ReferenceManager.instance.movePosTrans.position;
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

    bool onetime = true;
    void takeDamage(Bullet bul)
    {
        if (!onetime) return;
        onetime = false;
        ordMesh.SetActive(false);
        GameObject z = Instantiate(zombieRag, transform.position, Quaternion.identity, null);
        ZombieRagdoll rag = z.GetComponent<ZombieRagdoll>();
        rag.Init(bul.moveDir, bulletHitForce, color);

        ReferenceManager.instance.removeZombie(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "RagdollBullet")
        {
            print("Hit");
            SphereCol bul = other.transform.GetComponent<SphereCol>();
            stillAlive = false;
            takeDamage(bul.bul);
            //changeAnimState("Death");

        }
    }

}

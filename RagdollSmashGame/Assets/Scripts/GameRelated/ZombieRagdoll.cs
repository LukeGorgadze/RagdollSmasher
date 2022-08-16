using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRagdoll : MonoBehaviour
{
    public SkinnedMeshRenderer rend;
    public Rigidbody rb;

    public void Init(Vector3 moveDir, float force,Color col)
    {
        rend.material.color = col;
        rb.AddForce(moveDir * force * Time.deltaTime * 100, ForceMode.Impulse);
        Destroy(this.gameObject, 2f);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    float speed;
    Vector3 moveDir;
    // private void OnEnable()
    // {
    //     // UpdateManager.onUpdate += myUpdate;
    //     UpdateManager.onFixedUpdate += myFixedUpdate;
    // }

    // private void OnDisable()
    // {
    //     // UpdateManager.onUpdate -= myUpdate;
    //     UpdateManager.onFixedUpdate -= myFixedUpdate;
    // }
    // void myFixedUpdate()
    // {


    // }
    public void Init(float speed, Vector3 moveDir)
    {
        this.speed = speed;
        this.moveDir = moveDir;
        rb.AddForce(moveDir * speed, ForceMode.Impulse);
    }
}

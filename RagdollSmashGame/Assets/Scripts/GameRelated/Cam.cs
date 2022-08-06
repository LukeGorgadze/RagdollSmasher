using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Vector3 offset;
    public float minHeight;

    Vector3 followSpeed;
    private void OnEnable()
    {
        UpdateManager.onFixedUpdate += myFixedUpdate;
    }

    private void OnDisable()
    {
        UpdateManager.onFixedUpdate -= myFixedUpdate;

    }

    private void myFixedUpdate()
    {
        Transform plTr = ReferenceManager.instance.Pl.transform;

        Vector3 newPos = plTr.position + Vector3.forward * offset.z;
        newPos.y = Mathf.Max(newPos.y + offset.y, minHeight);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref followSpeed, 0.1f);
        //transform.LookAt(plTr.position + plTr.forward * 5);
    }
}

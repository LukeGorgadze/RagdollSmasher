using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] FloatingJoystick joy;
    [SerializeField] Rigidbody rb;
    [SerializeField] Bullet bullet;
    [SerializeField] Transform tip;
    [SerializeField] GameObject shootEff;
    [SerializeField] GameObject[] wheelEffs;

    [Header("Parameters")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float bulletSpeed;
    [SerializeField] float backForce;

    // Meh variables
    Vector2 xy;
    float X, Y;
    bool oneShot;

    private void OnEnable()
    {
        UpdateManager.onUpdate += myUpdate;
        UpdateManager.onFixedUpdate += myFixedUpdate;
    }

    private void OnDisable()
    {
        UpdateManager.onUpdate -= myUpdate;
        UpdateManager.onFixedUpdate -= myFixedUpdate;
    }

    public void myUpdate()
    {
        getInput();
        turn();
        shoot();
    }

    public void myFixedUpdate()
    {
        move();
    }

    bool oneTimeSet;
    void move()
    {
        if (rb.velocity.magnitude < 0.2f)
        {
            setWheelEffs(false);
            oneTimeSet = true;
        }
        else
        {
            if (oneTimeSet)
            {
                setWheelEffs(true);
                oneTimeSet = false;
            }
        }
        if (xy.magnitude == 0) return;

        Vector3 movedir = new Vector3(X, 0, Y);
        rb.AddForce(movedir * speed, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        oneShot = true;


    }

    void getInput()
    {
        X = joy.Horizontal;
        Y = joy.Vertical;

        xy = new Vector2(X, Y);
    }

    void turn()
    {
        Vector3 curRot = transform.rotation.eulerAngles;
        if (xy.magnitude == 0)
        {
            transform.rotation = Quaternion.Euler(0, curRot.y, 0);
            return;
        };
        float angle = Mathf.Atan2(X, Y) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                Quaternion.Euler(0, angle, 0),
                                    Time.deltaTime * turnSpeed);
    }

    void shoot()
    {
        if (xy.magnitude != 0 || !oneShot) return;
        Bullet bul = Instantiate(bullet, tip.position, Quaternion.identity);
        Instantiate(shootEff, tip.position, Quaternion.identity);
        bul.Init(bulletSpeed, transform.forward);
        oneShot = false;

        rb.AddForce(-transform.forward * backForce, ForceMode.Impulse);

    }

    void setWheelEffs(bool state)
    {
        foreach (GameObject obj in wheelEffs)
        {
            obj.SetActive(state);
        }
    }
}

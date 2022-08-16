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
    [SerializeField] Transform CannonHead;
    [SerializeField] GameObject[] wheelEffs;
    [SerializeField] Transform RightWheel;
    [SerializeField] Transform LeftWheel;
    [SerializeField] MeshRenderer rend;

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
    bool gameDone;

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

        CheckWinOrLose();
    }

    public void myFixedUpdate()
    {
        move();
    }

    bool oneTimeSet;
    void move()
    {
        if (rb.velocity.magnitude < 0.1f)
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
        rotateWheels();
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

    void rotateWheels()
    {
        float ang = Vector2.SignedAngle(new Vector2(transform.forward.x, transform.forward.z).normalized, new Vector2(X, Y).normalized);
        float fac = Vector2.Dot(new Vector2(transform.forward.x, transform.forward.z).normalized, new Vector2(X, Y).normalized);
        float rFac = 1;
        float lFac = 1;
        if (ang < 0)
            rFac = 0.25f * fac;
        else if (ang > 0)
            lFac = 0.25f * fac;
        // print(rb.velocity);
        RightWheel.Rotate(Vector3.right * rb.velocity.magnitude * rFac * Time.deltaTime * 100);
        LeftWheel.Rotate(Vector3.right * rb.velocity.magnitude * lFac * Time.deltaTime * 100);
    }
    void shoot()
    {
        if (xy.magnitude != 0 || !oneShot) return;
        AudioManager.instance.Play("Cannon");
        Bullet bul = Instantiate(bullet, tip.position, Quaternion.identity);
        Instantiate(shootEff, tip.position, Quaternion.identity);
        bul.Init(bulletSpeed, CannonHead.forward);
        oneShot = false;

        rb.AddForce(-transform.forward * backForce * Time.fixedDeltaTime * 10, ForceMode.Impulse);

    }

    void setWheelEffs(bool state)
    {
        foreach (GameObject obj in wheelEffs)
        {
            obj.SetActive(state);
        }
    }

    void CheckWinOrLose()
    {
        if (gameDone) return;
        if (ReferenceManager.instance.zombieList.Count == 0)
        {
            GameManager.instance.UpdateGameState(GameManager.GameState.WIN);
            gameDone = true;
        }
        if (transform.position.y <= -5)
        {
            GameManager.instance.UpdateGameState(GameManager.GameState.LOSE);
            gameDone = true;
        }
    }

    public void setMyCol(Color col)
    {
        rend.materials[0].color = col;
    }
}

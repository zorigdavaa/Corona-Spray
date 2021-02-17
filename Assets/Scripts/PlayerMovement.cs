using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Cached Reference
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject bullet;

    //Config
    [SerializeField] float speed = 6;

    //game parameters
    private bool gameStarted = false;
    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }

    bool alive = true;
    public bool Alive
    {
        get { return alive; }
        set
        {
            if (value == false)
            {
                // doosh unana
                rb.constraints = new RigidbodyConstraints();
                GameManager.instance.afterDieMenu.SetActive(true);
            }
            else // else means true or alive
            {
                // rotate iig hyazgaarlana
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.velocity = new Vector3(0, 0, 0);
                if (transform.position.y < -5)
                {
                    Vector3 spawnPos = new Vector3(0, 1, transform.position.z);
                    transform.position = spawnPos;
                }
            }
            alive = value;
        }
    }

    [SerializeField]
    private int remainingBullet;
    public int RemainingBullet
    {
        get { return remainingBullet; }
        set
        {
            if (value < 0)
            {
                remainingBullet = 0;
                return;
            }
            remainingBullet = value;
            GameManager.instance.RefreshBulletText();
        }
    }

    [SerializeField]
    int maxBulletCapacity = 10;
    public int MaxBulletCapacity
    {
        get { return maxBulletCapacity; }
        set
        {
            maxBulletCapacity = value;

        }
    }

    private float distance;
    public float Distance
    {
        get { return distance; }
        set
        {
            distance = value;
            GameManager.instance.RefreshDistanceText();
        }
    }

    //Movement params
    float horizontalInput;
    Vector3 jump;
    float jumpForce = 3f;
    bool isGrounded;

    //Bottle Sizes
    [SerializeField]
    Vector3 bottleSize;
    Vector3 emptyBottle = new Vector3(0.6f, 1, 0.6f),
            bigger1 = new Vector3(0.8f, 1, 0.8f),
            bigger2 = new Vector3(1f, 1, 1f),
            bigger3 = new Vector3(1.2f, 1, 1.2f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bottleSize = gameObject.transform.GetChild(0).transform.localScale;
        RemainingBullet = 0;
        StartingPoint();
    }

    public void StartingPoint()
    {
        horizontalInput = 0;
        jump = new Vector3(0.0f, 2.0f, 0.0f);


        GameManager.instance.beforeStartMenu.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (GameStarted)
        {
            if (!Alive)
            {
                return;
            }
            Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;

            Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMove + horizontalMove);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Distance = transform.position.z;

        if (GameStarted)
        {
            //player unawal uhne
            Vector3 playerPosition = transform.position;
            if (playerPosition.y < -5)
            {
                Die();
            }
            PlayerControl();
            Jump();
            AdjustSize();
            Fire();
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (RemainingBullet > 0)
            {
                GameObject createdBullet = Instantiate(bullet, transform.GetChild(1).position, transform.GetChild(1).rotation);
                createdBullet.transform.Rotate(Vector3.left * 90);
                Rigidbody bulletRb = createdBullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(Vector3.forward * 30, ForceMode.Impulse);
                //bulletRb.velocity = bulletRb.transform.forward * 15;
                RemainingBullet--;

            }
        }
    }

    private void AdjustSize()
    {
        if (RemainingBullet == 0)
        {
            bottleSize = emptyBottle;
        }
        else if (RemainingBullet > 0 && RemainingBullet <= 4)
        {
            bottleSize = bigger1;
        }
        else if (RemainingBullet > 4 && RemainingBullet <= 7)
        {
            bottleSize = bigger2;
        }
        else
        {
            bottleSize = bigger3;
        }
        gameObject.transform.GetChild(0).transform.localScale = bottleSize;
    }

    public void StartGame()
    {
        GameStarted = true;
        Alive = true;
    }

    private void PlayerControl()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
    public void Die()
    {
        GameStarted = false;
        Alive = false;

    }
    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    public void IncreaseGunBy5()
    {
        RemainingBullet += 5;
        if (RemainingBullet > MaxBulletCapacity)
        {
            RemainingBullet = MaxBulletCapacity;
        }
        GameManager.instance.RefreshBulletText();
    }
}

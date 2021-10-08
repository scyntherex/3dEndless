using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public static GameObject player;
    public static GameObject currentPlatform;

    bool canTurn = false;
    Vector3 startPosition;

    public static bool isDead = false;
    Rigidbody rb;

    public GameObject burger;
    public Transform burgerStartPos;
    Rigidbody burgerRB;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fire" || other.gameObject.tag == "Wall")
        {
            animator.SetTrigger("isDead");
            isDead = true;
        }
        else
        {
            currentPlatform = other.gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        burgerRB = burger.GetComponent<Rigidbody>();

        player = this.gameObject;
        startPosition = player.transform.position;

        GenerateWorld.RunDummy();
    }

    void ThrowBurger()
    {
        burger.transform.position = burgerStartPos.position;
        burger.SetActive(true);
        burgerRB.AddForce(this.transform.forward * 4000);
        Invoke("KillBurger", 1);
    }

    void KillBurger()
    {
        burger.SetActive(false);
    }

    void StopJumping()
    {
        animator.SetBool("isJumping", false);
    }
    void StopCastingMagic()
    {
        animator.SetBool("isCastingMagic", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
        {
            GenerateWorld.RunDummy();
        }

        if (other is SphereCollider)
        {
            canTurn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
        {
            canTurn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * 200);
        }
        else if (Input.GetKeyDown(KeyCode.M) && animator.GetBool("isJumping") == false)
        {
            animator.SetBool("isCastingMagic", true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveler.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
            {
                GenerateWorld.RunDummy();
                GenerateWorld.RunDummy();
            }

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummyTraveler.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
            {
                GenerateWorld.RunDummy();
                GenerateWorld.RunDummy();
            }

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(-0.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(0.5f, 0, 0);
        }
    }
}

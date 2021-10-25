using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public static GameObject player;
    public static GameObject currentPlatform;
    public static AudioSource[] sfx;

    bool canTurn = false;
    Vector3 startPosition;

    public static bool isDead = false;
    Rigidbody rb;

    public GameObject burger;
    public Transform burgerStartPos;
    Rigidbody burgerRB;

    int livesLeft;
    public Texture aliveIcon;
    public Texture deadIcon;
    public RawImage[] icons;

    public GameObject gameOverPanel;
    public Text highScore;

    bool falling = false;

    void RestartGame()
    {
        SceneManager.LoadScene("ScrollingWorld", LoadSceneMode.Single);
    }

    void OnCollisionEnter(Collision other)
    {
        if ((falling || other.gameObject.tag == "Fire" || other.gameObject.tag == "Wall") && !isDead)
        {
            if (falling)
            {
                animator.SetTrigger("IsFalling");
            }
            else
            {
                animator.SetTrigger("isDead");
            }
            
            isDead = true;
            sfx[2].Play();
            livesLeft--;
            PlayerPrefs.SetInt("lives", livesLeft);

            if (livesLeft > 0)
            {
                Invoke("RestartGame", 2);
            }
            else
            {
                icons[0].texture = deadIcon;
                gameOverPanel.SetActive(true);

                PlayerPrefs.SetInt("lastscore", PlayerPrefs.GetInt("score"));
                if (PlayerPrefs.HasKey("highscore"))
                {
                    int hs = PlayerPrefs.GetInt("highscore");
                    PlayerPrefs.SetInt("highscore", Mathf.Max(hs, PlayerPrefs.GetInt("score")));
                }
                else
                {
                    PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
                }
            }
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
        sfx = GameObject.FindWithTag("gameData").GetComponentsInChildren<AudioSource>();

        player = this.gameObject;
        startPosition = player.transform.position;

        GenerateWorld.RunDummy();

        if (PlayerPrefs.HasKey("highscore"))
        {
            highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore");
        }
        else
        {
            highScore.text = "High Score: 0";
        }

        isDead = false;
        livesLeft = PlayerPrefs.GetInt("lives");

        for(int i = 0; i < icons.Length; i++)
        {
            if (i >= livesLeft)
            {
                icons[i].texture = deadIcon;
            }
        }
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

    void PlayThrowSound()
    {
        sfx[1].Play();
    }
    public void FootStep1()
    {
        sfx[4].Play();
    }
    public void FootStep2()
    {
        sfx[5].Play();
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

        if (currentPlatform != null)
        {
            if (this.transform.position.y < (currentPlatform.transform.position.y - 5))
            {
                falling = true;
                OnCollisionEnter(null);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * 200);
            sfx[6].Play();
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

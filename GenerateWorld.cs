using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateWorld : MonoBehaviour
{
    static public GameObject dummyTraveler;
    static public GameObject lastPlatform;

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    void Awake()
    {
        dummyTraveler = new GameObject("dummy");
    }

    public static void RunDummy()
    {
        GameObject p = Pool.singleton.GetRandom();
        if (p == null) { return; }

        if (lastPlatform != null)
        {
            if (lastPlatform.tag == "platformTSection")
            {
                dummyTraveler.transform.position = lastPlatform.transform.position +
                PlayerController.player.transform.forward * 20;
            }
            else
            {
                dummyTraveler.transform.position = lastPlatform.transform.position +
                PlayerController.player.transform.forward * 10;
            }

            if (lastPlatform.tag == "stairsUp")
            {
                dummyTraveler.transform.Translate(0, 5, 0);
            }
        }

        lastPlatform = p;
        p.SetActive(true);
        p.transform.position = dummyTraveler.transform.position;
        p.transform.rotation = dummyTraveler.transform.rotation;

        if (p.tag == "stairsDown")
        {
            dummyTraveler.transform.Translate(0, -5, 0);
            p.transform.Rotate(0, 180, 0);
            p.transform.position = dummyTraveler.transform.position;
        }
    }
}

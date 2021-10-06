using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNonDestroyingPlats : MonoBehaviour
{
    GameObject dummyTraveler;

    void Start()
    {
        dummyTraveler = new GameObject("dummy");

        for (int i = 0; i < 20; i++)
        {
            GameObject p = Pool.singleton.GetRandom();
            if (p == null)
            {
                return;
            }
            p.SetActive(true);
            p.transform.position = dummyTraveler.transform.position;
            p.transform.rotation = dummyTraveler.transform.rotation;

            if (p.tag == "stairsUp")
            {
                dummyTraveler.transform.Translate(0, 5, 0);
            }
            else if (p.tag == "stairsDown")
            {
                dummyTraveler.transform.Translate(0, -5, 0);
                p.transform.Rotate(new Vector3(0, 180, 0));
                p.transform.position = dummyTraveler.transform.position;
            }
            else if (p.tag == "platformTSection")
            {
                if (Random.Range(0, 2) == 0)
                {
                    dummyTraveler.transform.Rotate(new Vector3(0, 90, 0));
                }
                else
                {
                    dummyTraveler.transform.Rotate(new Vector3(0, -90, 0));
                }
                dummyTraveler.transform.Translate(Vector3.forward * -10);
            }
            dummyTraveler.transform.Translate(Vector3.forward * -10);
        }
    }
}

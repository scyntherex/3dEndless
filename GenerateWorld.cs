using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public GameObject[] platforms;
    GameObject dummyTraveler;

    void Start()
    {
        dummyTraveler = new GameObject("dummy");

        for(int i = 0; i < 50; i++)
        {
            int platformNumber = Random.Range(0, platforms.Length);
            GameObject p = Instantiate(platforms[platformNumber], 
                dummyTraveler.transform.position, 
                dummyTraveler.transform.rotation);

            if (platforms[platformNumber].tag == "stairsUp")
            {
                dummyTraveler.transform.Translate(0, 5, 0);
            }
            else if (platforms[platformNumber].tag == "stairsDown")
            {
                dummyTraveler.transform.Translate(0, -5, 0);
                p.transform.Rotate(new Vector3(0, 180, 0));
                p.transform.position = dummyTraveler.transform.position;
            }
            else if (platforms[platformNumber].tag == "platformTSection")
            {
                if (Random.Range(0, 2) == 0) {
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

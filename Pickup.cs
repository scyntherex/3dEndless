using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    MeshRenderer[] meshRenders;
    public GameObject scorePrefab;
    public GameObject particlePrefab;
    GameObject canvas;

    void Start()
    {
        meshRenders = this.GetComponentsInChildren<MeshRenderer>();
        canvas = GameObject.Find("Canvas");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameData.singleton.UpdateScore(10);
            PlayerController.sfx[7].Play();
            GameObject scoreText = Instantiate(scorePrefab);
            scoreText.transform.parent = canvas.transform;

            GameObject pE = Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
            Destroy(pE, 1);

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
            scoreText.transform.position = screenPoint;
            foreach(MeshRenderer m in meshRenders)
            {
                m.enabled = false;
            }
        }
    }

    void OnEnabled()
    {
        if (meshRenders != null)
        {
            foreach (MeshRenderer m in meshRenders)
            {
                m.enabled = true;
            }
        }
    }
}

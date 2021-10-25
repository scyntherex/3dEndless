using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    MeshRenderer[] meshRenders;

    void Start()
    {
        meshRenders = this.GetComponentsInChildren<MeshRenderer>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameData.singleton.UpdateScore(10);
            PlayerController.sfx[7].Play();
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

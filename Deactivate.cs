using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    bool deactivationScheduled = false;
    void OnCollisionExit(Collision player)
    {
        if (PlayerController.isDead) { return; }
        if (player.gameObject.tag == "Player" && !deactivationScheduled)
        {
            Invoke("SetInactive", 3.0f);
            deactivationScheduled = true;
        }
    }

    void SetInactive()
    {
        this.gameObject.SetActive(false);
        deactivationScheduled = false;
    }
}

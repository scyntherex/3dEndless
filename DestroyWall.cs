using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public GameObject[] bricks;
    List<Rigidbody> bricksRBs = new List<Rigidbody>();
    List<Vector3> positions = new List<Vector3>();
    List<Quaternion> rotations = new List<Quaternion>();
    public GameObject explosion;

    Collider col;

    void OnEnable()
    {
        col.enabled = true;
        for(int i = 0; i < bricks.Length; i++)
        {
            bricks[i].transform.localPosition = positions[i];
            bricks[i].transform.localRotation = rotations[i];
            bricksRBs[i].isKinematic = true;
        }
    }

    void Awake()
    {
        col = this.GetComponent<Collider>();
        foreach (GameObject obj in bricks)
        {
            bricksRBs.Add(obj.GetComponent<Rigidbody>());
            positions.Add(obj.transform.localPosition);
            rotations.Add(obj.transform.localRotation);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Burger")
        {
            GameObject obj = Instantiate(explosion, other.contacts[0].point, Quaternion.identity);
            Destroy(obj, 2.5f);
            col.enabled = false;
            foreach(Rigidbody r in bricksRBs)
            {
                r.isKinematic = false;
            }
            PlayerController.sfx[3].Play();
        }
    }
}

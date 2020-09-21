using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    private Collider2D coll;


    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }
    public void openDoor()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }

    }
}

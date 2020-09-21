using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToDisable;
    public bool disabled = false;

    void Update()
    {

    }

    public void setActiveStatus()
    {
        if (disabled)
        {
            //disable the cannon
            objectToDisable.SetActive(false);
        }

        else
{
    //enable the cannon
    objectToDisable.SetActive(true);
}
    }
}

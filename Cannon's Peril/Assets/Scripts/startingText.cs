using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class startingText : MonoBehaviour
{

    [SerializeField] float timeLeft = 5.0f;


    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            gameObject.SetActive(false);
        }
    }
}

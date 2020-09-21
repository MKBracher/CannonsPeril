using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{

    [SerializeField] private string sceneName;

    public PlayerController playerController;
    public GameObject player;
    private void Start()
    {

        //finding the player object
        player = GameObject.FindGameObjectWithTag("Player");


        playerController = player.GetComponent<PlayerController>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerController.holdingCannon)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}

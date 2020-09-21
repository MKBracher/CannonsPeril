using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cannonPickup : MonoBehaviour
{

    [SerializeField] private Text pickupText;

    public PlayerController playerController;

    public Transform player;

    public GameObject cannon;

    public GameObject playerObj;

    private bool pickUpAllowed;

    public GameObject cannonSpawner;

    private Spawner spawnScript;

    public bool detachChild;


    private void Start()
    {
        //Text is inactive until a player enters the collider
        pickupText.gameObject.SetActive(false);

        //finding the cannon object
        cannon = GameObject.FindGameObjectWithTag("Cannon");

        //finding the player object
        playerObj = GameObject.FindGameObjectWithTag("Player");

        //finding the cannonspawner object
        cannonSpawner = GameObject.FindGameObjectWithTag("CannonSpawner");

        //finding the cannonSpawner script 
        spawnScript = cannonSpawner.GetComponent<Spawner>();

    }

    private void Update()
    {
        //if pickup is allowed and the player presses E - calls pickup function
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();  
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
        //When player enters the collider of the cannon 
    {
        if (collision.gameObject.tag == "Player")
        {

            //Sets the "Press E to pickup" text
            pickupText.gameObject.SetActive(true);
            //sets pickup to be allowed
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
        //when player exits the collider of the cannon
    {
        if(collision.gameObject.tag == "Player")
        {
            //Hides the "Press E to pickup" text
            pickupText.gameObject.SetActive(false);
            //stops the player from picking up the cannon
            pickUpAllowed = false;
        }

    }

    private void PickUp()
    {
        //Calls the player cannonState function, to change the player to holding the cannon.
        playerController.cannonState();
        //sets player as parent for the cannon position when dropping
        cannon.transform.parent = player.transform;

        //Disables the cannon object in the game
        spawnScript.disabled = true;
        spawnScript.setActiveStatus();

    }

    public void Drop()
    {
        //Sets the location of the cannon to the location of the player
        cannon.transform.SetParent(null);
        //Changes the player to not holding the cannon
        playerController.cannonState();
    }

}

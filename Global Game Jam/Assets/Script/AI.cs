using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private int difficulty;
    private double speed;
    private int bonusIA;
    private double distanceWithPlayer;
    private double probability;

    [SerializeField]
    private float rotationSpeed = 1;

    [SerializeField]
    private GameObject gameControllerObject;

    private NavMeshAgent agent;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
        probability = 0;
        speed = 1;
        bonusIA = 0;
        gameController = gameControllerObject.GetComponent<GameController>();
        agent = GetComponent<NavMeshAgent>();

        print("Je suis né pour te hanter...");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 ghostPos = GameObject.FindGameObjectWithTag("ghost").transform.position;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        Ray ray = new Ray(ghostPos, transform.forward);
        Debug.DrawRay(ghostPos, transform.forward, Color.yellow);

        // UPDATE THE DISTANCE
        distanceWithPlayer = Vector3.Distance(ghostPos, playerPos);

        if (!gameController.getIsHidden() && canSeeThePlayer(ray))
        {
            //UPDATE THE PROBABILITY
            probability = updateProbability();

            if (Random.Range(0, 1) <= probability)
            {
                Debug.Log("Je vais vers toi !");
                agent.destination = playerPos;
            }
        }
        else
        {
            transform.Rotate(new Vector3(0, rotationSpeed, 0));

        }
    }

    double updateProbability()
    {
        if (distanceWithPlayer < 3)
        {
            Debug.Log("Proba: 1");
            return 1.0;
        }
        else if (distanceWithPlayer < 3.67)
        {
            Debug.Log("Proba: " + probability);
            return (distanceWithPlayer / (-10)) + 1.2;
        }
        else
        {
            Debug.Log("Proba: " + probability);
            return (distanceWithPlayer / (-4)) + 1.75;
        }
    }

    bool canSeeThePlayer(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 7.0F))
        {
            Debug.Log("Je te vois !");
            return (hit.transform.CompareTag("Player"));
        }
        Debug.Log("Je ne te vois pas !");
        return false;
    }


    //EVENTS LISTENERS FOR DAYS AND NIGHTS
} 

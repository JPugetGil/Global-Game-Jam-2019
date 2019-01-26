using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private int difficulty;
    private double speed;
    private int bonusIA;
    private double distanceWithPlayer;
    private double probability;
    private bool seeThePlayer;
    private bool attack;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
        probability = 0;
        speed = 1;
        bonusIA = 0;
        seeThePlayer = false;
        attack = false;

        print("Je suis né pour te hanter...");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ghostPos = GameObject.FindGameObjectWithTag("ghost").transform.position;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        Ray ray = new Ray(ghostPos, playerPos);
        // UPDATE THE DISTANCE
        distanceWithPlayer = Vector3.Distance(ghostPos, playerPos);

        //UPDATE THE PROBABILITY
        updateProbability();

        //UPDATE IF IT CAN SEE PLAYER

        seeThePlayer = canSeeThePlayer(ray);

        //UPDATE IF IT GOES ON THE PLAYER
        attack = goesOnPlayer();

    }

    double updateProbability()
    {
        if (distanceWithPlayer < 2)
        {
            return 1.0;
        }
        else if (distanceWithPlayer < 3.67)
        {
            return (distanceWithPlayer / (-10)) + 1.2;
        }
        else
        {
            return (distanceWithPlayer / (-4)) + 1.75;
        }
    }

    bool canSeeThePlayer(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 7.0F))
        {
            return (hit.transform.CompareTag("Player"));
        }

        return false;
    }

    bool goesOnPlayer()
    {
        return (Random.Range(0, (float)probability) < probability);
    }

    //EVENTS LISTENERS FOR DAYS AND NIGHTS
} 

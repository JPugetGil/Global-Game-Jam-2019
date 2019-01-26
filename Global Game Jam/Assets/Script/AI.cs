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

    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
        probability = 0;
        speed = 1;

        //CHECK THE DISTANCE BETWEEN THEM
        distanceWithPlayer = 0;
        print("Je suis né pour te hanter...");
    }

    // Update is called once per frame
    void Update()
    {
        // UPDATE THE DISTANCE
        distanceWithPlayer = 0;

        //UPDATE THE PROBABILITY
        updateProbability();
    }

    bool canSeeThePlayer()
    {
        //A COMPLETER
        //BONUS IA
        return true;
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

    bool goesOnPlayer()
    {
        //A MODIFIER
        return (canSeeThePlayer() && (probability > 0.5));
    }

    //EVENTS LISTENERS FOR DAYS AND NIGHTS
} 

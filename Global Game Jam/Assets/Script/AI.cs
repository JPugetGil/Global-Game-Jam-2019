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
    private float rotationSpeed = 3;

    [SerializeField]
    private GameObject gameControllerObject;

    public DayNightController dayNight;

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
        DayNightController.Instance.AddNightObject(gameObject);

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
                    agent.destination = playerPos;
                }
            }
            else
            {
                // CHOISIR UNE DESTINATION DE MANIERE RANDOM OU NAVIGUER SUR LA CARTE
                transform.Rotate(new Vector3(0, rotationSpeed, 0));
            }

            rotationSpeed = 3 + GameController.Instance.getMatchingMemoryCount()*0.25f;
            speed = 3 + GameController.Instance.getMatchingMemoryCount() * 0.15f;
    }

    double updateProbability()
    {
        if (distanceWithPlayer < 4)
        {
            return 1.0;
        }
        else if (distanceWithPlayer < 7.33)
        {
            return (distanceWithPlayer / (-10)) + 1.4;
        }
        else
        {
            return (distanceWithPlayer / (-4)) + 2.5;
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
 
} 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private double speed;
    private double distanceWithPlayer;
    private double probability;


    //LOOKING FOR THE PLAYER;
    private Vector3 dest;
    private Vector3 lastPos;
    private float waitTime = 1.5f;
    private float lastAnimation = 0.0f;

    [SerializeField]
    private float rotationSpeed = 4;

    [SerializeField]
    private GameObject gameControllerObject;

    private NavMeshAgent agent;
    private GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        probability = 0;
        speed = 1;
        
        gameController = gameControllerObject.GetComponent<GameController>();
        agent = GetComponent<NavMeshAgent>();
        DayNightController.Instance.AddNightObject(gameObject);
        lastPos = agent.transform.position;



        print("Je suis né pour te hanter...");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            Vector3 ghostPos = GameObject.FindGameObjectWithTag("ghost").transform.position;
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            Ray ray = new Ray(ghostPos, transform.forward);
            Ray ray2 = new Ray(ghostPos, transform.right);
            Debug.DrawRay(ghostPos, transform.forward, Color.yellow);
            Debug.DrawRay(ghostPos, transform.right, Color.yellow);


        // UPDATE THE DISTANCE
        distanceWithPlayer = Vector3.Distance(ghostPos, playerPos);
        //Debug.Log("Can see : " + canSeeThePlayer(ray));
        //Debug.Log("Is Hidden : " + gameController.getIsHidden());
        if (!gameController.getIsHidden() && canSeeThePlayer(ray, ray2)) 
        {
            //UPDATE THE PROBABILITY
            probability = updateProbability();

            if (Random.Range(0.0f, 1.0f) <= probability)
            {
                agent.destination = playerPos;
            }

        } else
        {
            // CHOISIR UNE DESTINATION DE MANIERE RANDOM
            transform.Rotate(new Vector3(0, rotationSpeed, 0));
            if (lastPos == ghostPos)
            {
                
                if (lastAnimation <= 0)
                {
                    dest = getRandomPosition(); 
                    if (NavMesh.SamplePosition(dest, out NavMeshHit hit, 25f, NavMesh.AllAreas))
                    {
                        agent.destination = hit.position;
                        lastAnimation = waitTime;
                    }
                } else
                {
                    lastAnimation -= Time.deltaTime;
                }
            }
            else
            {
                lastPos = ghostPos;
                lastAnimation = waitTime;
            }
            
            rotationSpeed = 3 + GameController.Instance.getMatchingMemoryCount() * 0.25f;
            speed = 3 + GameController.Instance.getMatchingMemoryCount() * 0.15f;
        }
    }

    double updateProbability()
    {
        if (distanceWithPlayer < 6)
        {
            return 1.0;
        }
        else 
        {
            return (distanceWithPlayer / (-16)) + 1.375;
        }
    }

    bool canSeeThePlayer(Ray ray, Ray ray2)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 22.0F, 10) || Physics.Raycast(ray, out hit, 22.0F, 10))
        { 
            Debug.Log("I see: " + hit.transform.tag);
            return (hit.transform.CompareTag("Player"));
        }
        return false;
    }
 

    Vector3 getRandomPosition()
    {
        return Random.insideUnitCircle * 25;
    }
} 

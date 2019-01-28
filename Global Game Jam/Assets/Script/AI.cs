using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private double distanceWithPlayer;

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
        gameController = gameControllerObject.GetComponent<GameController>();
        agent = GetComponent<NavMeshAgent>();
        DayNightController.Instance.AddNightObject(gameObject);
        lastPos = agent.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        agent.speed = 3 + 0.5f * GameController.Instance.getMatchingMemoryCount();

        Vector3 ghostPos = GameObject.FindGameObjectWithTag("ghost").transform.position;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        Ray ray = new Ray(ghostPos, transform.forward);
        Debug.DrawRay(ghostPos, transform.forward, Color.yellow);

        // UPDATE THE DISTANCE
        distanceWithPlayer = Vector3.Distance(ghostPos, playerPos);
        if (!gameController.getIsHidden()) 
        {
            agent.destination = playerPos;
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
            
        }
    }

    Vector3 getRandomPosition()
    {
        return Random.insideUnitCircle * 25;
    }

    public void OnTriggerEnter(Collision col)
    {
        Debug.Log(col);
        if (col.gameObject.CompareTag("Player"))
        {
            GameController.Instance.Forward();
        }
    }
} 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    bool canMove = true;
    [Tooltip("FOV, 0-180")]
    [SerializeField]
    float FOV = 50f;
    [Tooltip("True makes this NPC go to a random waypoint")]
    [SerializeField]
    bool RandomizeWaypoints = false;
    [SerializeField]
    Transform[] Waypoints;
    int Waypoint = 0;
    Transform currentWaypoint;
    NavMeshAgent agent;
    public GameObject player;
    int NPCState = 0; // 0 = wandering, 1 = sees player, 2 = scared? Maybe make this an enum, works for now tho
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(Waypoints[Waypoint].position);
    }

    IEnumerator WaittoMove(float seconds)
    {
        canMove = false;
        if (RandomizeWaypoints)
        {
            Waypoint = Random.Range(0, Waypoints.Length);
        }
        else
        {
            Waypoint++;
            if (Waypoint > Waypoints.Length - 1)
                Waypoint = 0;
        }
        yield return new WaitForSeconds(seconds);
        canMove = true;
        agent.SetDestination(Waypoints[Waypoint].position);
        yield return null;
    }

    void FindTarget()
    {
        Vector3 rayStart = (transform.position + new Vector3(0f, 0.2f, 0f));
        var playerPos = player.transform.position - transform.position;
        Ray sight = new Ray(rayStart, playerPos);
        RaycastHit hit;
        float angle = Vector3.Angle(playerPos, transform.forward);
        if (Physics.Raycast(sight, out hit) && (hit.transform.tag == "Player")&&angle <=FOV) //sees player
        {
            if (NPCState != 1)
            {
                NPCState = 1;
                Debug.Log(angle);
            }
            agent.isStopped=true;
        }
        else //does not
        {
            NPCState = 0;
            agent.isStopped = false;
        }
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 3f);
    }

    void Update()
    {
        FindTarget(); //eventually want to spherecast to see if player is in radius at all and then add field of vision but this works for now
        switch (NPCState)
        {
            case 0: //wandering
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (canMove) StartCoroutine(WaittoMove(Random.Range(3f, 6f)));
                }
                break;
            case 1: //sees player
                FaceTarget(player.transform.position);
                break;
    }
    }
}

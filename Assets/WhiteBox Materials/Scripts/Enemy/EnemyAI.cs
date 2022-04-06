using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    //reference to waypoints
    public List<Transform> points;
    //the int value for next point index
    public int nextID = 0;
    //The value of that applies to ID for changing
    int idChangeValue = 1;
    //speed
    public float speed = 2;

    private void Reset()
    {
        Initialize();
    }

    private void Initialize()
    {
        //Make box collider trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
        //Create Root object
        GameObject root = new GameObject(name + "_Root");
        //Reset Position of Root to this gameobject
        root.transform.position = transform.position;
        //Set enemy object as child of root
        transform.SetParent(root.transform);
        //Creatwaypoint object
        GameObject waypoints = new GameObject("WayPoints");
        //Reset waypoints position to root
        //Make waypoints object child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        //Create two points (gameobject) and reset their position to waypoint objects
        //Make the points children of waypoint object
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;
        //Initialise points list then add points to it
        points.Add(p1.transform);
        points.Add(p2.transform);

    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //Get the next point transform
        Transform goalPoint = points[nextID];
        //Flip the enemytransform to look into the points direction
        //if (goalPoint.transform.position.x > transform.position.x)
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //else
            //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //Move the enemy towards the goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position,speed*Time.deltaTime);
        //Check distance between enemy and goal point to trigger next point
        if(Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            //check if we are at the end of the line (make the change -1)
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            //check if we are at the start of the line (make change +1)
            if (nextID == 0)
                idChangeValue = 1;
            //Apply change on the next ID
            nextID += idChangeValue;
        }
    }
}

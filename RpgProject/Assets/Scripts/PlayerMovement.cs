using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public bool reachedEnd = false;
    public List<Vector2> path;
    public Transform body;
    public Animator anim;
    public Vector2 currentwaypoint;
    public bool moving = false;
    public void Update()
    {
        if (path.Count > 0)
        {
            moving = true;
            move();  
        }
        else
        {
            moving = false;
        }
        GetComponent<RaycastInput>().enabled = !moving;
        anim.SetBool("moving", moving);

    }

    public void SetPath(List<Vector2> _path)
    {
        foreach (Vector2 waypoint in _path)
        {
            path.Add(waypoint);
        }
    }

    public void ClearPath()
    {
        path.Clear();
    }


    void move()
    {
        currentwaypoint = path[path.Count - 1];
        if(!reachedWaypoint(currentwaypoint))
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(currentwaypoint.x, 0f, currentwaypoint.y), Time.deltaTime * speed);
            
            float angle = Vector3.Angle(Vector3.forward, (new Vector3(currentwaypoint.x, 0f, currentwaypoint.y) - gameObject.transform.position));
            if (currentwaypoint.x - gameObject.transform.position.x < 0)
                angle = -angle;
            //Debug.Log("angle " + angle);
            body.rotation = Quaternion.Euler(0,angle,0);
        }
        else
        {
            if(path.Count > 0)
            {
                Debug.Log("pop");
                path.Remove(currentwaypoint);
            }
            else
            {
                reachedEnd = true;
                ClearPath();
                gameObject.transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x),0, Mathf.RoundToInt(gameObject.transform.position.z));
            }
        }
    }


    bool reachedWaypoint(Vector2 waypoint)
    {
        Vector2 playerpos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        if (Vector2.Distance(playerpos, waypoint) < 0.1f)
        {
            return true;
        }

        return false;
    }
}

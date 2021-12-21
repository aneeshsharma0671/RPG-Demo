using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : EnemyAIBehaviour
{
    Vector2 currentwaypoint;
    Vector2 lastTargetPos;
    List<Vector2> path = new List<Vector2>();
    public Transform body;

    public override void CombatMovment()
    {
        if (lastTargetPos != new Vector2(Mathf.RoundToInt(player.position.x),Mathf.RoundToInt(player.position.z)))
        {
            lastTargetPos = new Vector2(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z));
            path = GetComponent<Pathfinding>().GetPath(new Vector2(transform.position.x,transform.position.z), new Vector2(player.position.x,player.position.z));
            path.Remove(lastTargetPos);
            move(path);
        }
        else
        {
            move(path);
        }

    }

    void move(List<Vector2> path)
    {
        Debug.Log("Moving");
        if (path.Count < 1)
            return;
        currentwaypoint = path[path.Count - 1];
        Debug.Log(currentwaypoint);
        if (!reachedWaypoint(currentwaypoint))
        {
            //Debug.Log("Not reached" + currentwaypoint);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(currentwaypoint.x, 0f, currentwaypoint.y), Time.deltaTime * speed);
            float angle = Vector3.Angle(Vector3.forward, (new Vector3(currentwaypoint.x, 0f, currentwaypoint.y) - gameObject.transform.position));
            if (currentwaypoint.x - gameObject.transform.position.x < 0)
                angle = -angle;
            //Debug.Log("angle " + angle);
            body.rotation = Quaternion.Euler(0, angle, 0);
        }
        else
        {
            if (path.Count > 0)
            {
                path.Remove(currentwaypoint);
            }
            else
            {
                gameObject.transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), 0, Mathf.RoundToInt(gameObject.transform.position.z));
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

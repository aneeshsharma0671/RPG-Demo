using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Vector2 StartPos;
    public Vector2 TargetPos;
    public ObstacleScriptableObject obstacleinfo;
    public List<Vector2> Path;
    public GameObject pathindicator;
    public Transform pathParent;
    Vector2[] dir = {new Vector2(0,1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0)};
    Queue<int> rq = new Queue<int>();
    Queue<int> cq = new Queue<int>();
    List<List<bool>> visited = new List<List<bool>>();
    List<List<int>> weight = new List<List<int>>();
    int currenrw = 1;
    int nodes_left_in_layer = 1;
    int nodes_in_next_layer = 0;


    public List<Vector2> GetPath(Vector2 _StartPos,Vector2 _TargetPos)
    {
        StartPos = _StartPos;
        TargetPos = _TargetPos;
        SolvePath();
        setPath();
        return Path;
    }

    public void SolvePath()
    {
        StartPos = new Vector2(transform.position.x,transform.position.z);
        int sr = Mathf.RoundToInt(StartPos.x);
        int sc = Mathf.RoundToInt(StartPos.y);
        visited = new List<List<bool>>();
        weight = new List<List<int>>();
        rq = new Queue<int>();
        cq = new Queue<int>();
        nodes_left_in_layer = 1;
        nodes_in_next_layer = 0;

        rq.Enqueue(sr);
        cq.Enqueue(sc);
        for (int i = 0; i < 10; i++)
        {
            List<bool> currentrowNodes = new List<bool>();
            List<int> currentrow = new List<int>();
            for (int j = 0; j < 10; j++)
            {
                currentrowNodes.Add(false);
                currentrow.Add(0);
            }
            visited.Add(currentrowNodes);
            weight.Add(currentrow);
        }
        visited[sr][sc] = true;
        weight[sr][sc] = 0;

        while (rq.Count > 0)
        {
            int r = rq.Dequeue();
            int c = cq.Dequeue();

            if (r == TargetPos.x && c == TargetPos.y)
            {
                break;
            }

            exploreNeighbor(r, c);
            nodes_left_in_layer--;
            if (nodes_left_in_layer == 0)
            {
                nodes_left_in_layer = nodes_in_next_layer;
                nodes_in_next_layer = 0;
                currenrw++;
            }
        }
        

        /*
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                debug += weight[i][j] + " ";
            }
            debug += "\n";
        }

        Debug.Log(debug);
        if (reached_end)
        {
            return move_count;
        }
        return -1;
        */
    }
    void exploreNeighbor(int r, int c)
    {
        for (int i = 0; i < 4; i++)
        {

            int rr = r + (int)dir[i].x;
            int cc = c + (int)dir[i].y;

            if (rr < 0 || cc < 0)
                continue;
            if (rr >= 10 || cc >= 10)
                continue;
            if (visited[rr][cc])
                continue;
            if (obstacleinfo.obstacleData[rr].y[cc])
                continue;
            rq.Enqueue(rr);
            cq.Enqueue(cc);
            visited[rr][cc] = true;
            weight[rr][cc] = currenrw;
            nodes_in_next_layer++;
        }
    }
    public void setPath()
    {
        int sr = (int)TargetPos.x;
        int sc = (int)TargetPos.y;
        Path.Clear();
        Path.Add(new Vector2(sr, sc));
        while (currenrw > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                int rr = sr + (int)dir[i].x;
                int cc = sc + (int)dir[i].y;

                if (rr < 0 || cc < 0)
                    continue;
                if (rr >= 10 || cc >= 10)
                    continue;
                if (obstacleinfo.obstacleData[rr].y[cc])
                    continue;
                if (weight[rr][cc] == (currenrw - 1))
                {
                    Path.Add(new Vector2(rr, cc));
                    sr = rr;
                    sc = cc;
                    break;
                }
            }
            currenrw--;
        }
        Path.Add(StartPos);
    }
  


    

 

    

    /*
        public void FindPath()
        {
            if(Path.Count > 2 && Path[Path.Count-1] == TargetPos)
            {
                return;
            }
            Path.Clear();
            Vector2 currentnode = StartPos;
            Path.Add(StartPos);
            Path.Add(StartPos);
            for (int i = 0; i < 100; i++)
            { 
                if (currentnode == TargetPos)
                {
                    //Debug.Log("Path Found");
                    return;
                }

                currentnode = CheckNeighbor(currentnode);
            }

        }

        Vector2 CheckNeighbor(Vector2 currentnode)
        {
            float mindis = 200f;
            Vector2 nextnode = currentnode;

            for (int i = 0; i < 4; i++)
            {
                Vector2 nodeToCheck = new Vector2(currentnode.x + dir[i].x, currentnode.y + dir[i].y);
                if (Vector2.Distance(nodeToCheck, TargetPos) <= mindis && nodeToCheck.x >= 0 && nodeToCheck.y >= 0 && nodeToCheck.x < 10 && nodeToCheck.y < 10 && nodeToCheck != Path[Path.Count-2])
                {
                    if (!checkObstacle(nodeToCheck))
                    {
                        mindis = Vector2.Distance(nodeToCheck, TargetPos);
                        nextnode = nodeToCheck;
                    }
                }
            }

            Path.Add(nextnode);
            return nextnode;
        }

        bool checkObstacle(Vector2 checknode)
        {
            if(obstacleinfo.obstacleData[(int)checknode.x].y[(int)checknode.y])
                return true;
            else
            {
                return false;
            }
        }



        // BFS method
        private void Start()
        {
            SetDistance(StartPos,TargetPos);
            setPath();
        }

        void setPath()
        {
            Vector2 currentpos = TargetPos;
            while (currentdis > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    int Xpos = (int)currentpos.x + (int)dir[i].x;
                    int Ypos = (int)currentpos.y + (int)dir[i].y;

                    if (Xpos >= 0 && Ypos >= 0 && Xpos < 10 && Ypos < 10 && allNodes[Xpos][Ypos].distance == currentdis)
                    {
                        path2.Add(new Vector2(Xpos, Ypos));
                        currentpos = new Vector2(Xpos, Ypos);

                        break;
                    }
                }
                currentdis -= 1;
            }
        }

        public List<Vector2> path2;
        public List<List<Node>> allNodes = new List<List<Node>>();
        public int currentdis = 1;
        void SetDistance(Vector2 startPos,Vector2 targetPos)
       {
            Node startNode = new Node(startPos,true,false);
            Node targetNode = new Node(targetPos,false,false);


            List<Node> nodesToCheck = new List<Node>();

            string debug = "";

            for (int i = 0; i < 10; i++)
            {
                List<Node> currentrowNodes = new List<Node>();
                for (int j = 0; j < 10; j++)
                {
                    currentrowNodes.Add(new Node(new Vector2(i,j),false,obstacleinfo.obstacleData[i].y[j]));
                }
                allNodes.Add(currentrowNodes);
            }


            allNodes[0][0].visited = true;




            nodesToCheck.Add(startNode);

            while(nodesToCheck.Count > 0)
            {
                for (int j = 0; j < nodesToCheck.Count; j++)
                {
                    Node currentNode = nodesToCheck[j];
                    for (int i = 0; i < 4; i++)
                    {
                        int Xpos = (int)currentNode.pos.x + (int)dir[i].x;
                        int Ypos = (int)currentNode.pos.y + (int)dir[i].y;

                        if (Xpos >= 0 && Ypos >= 0 && Xpos < 10 && Ypos < 10 && !allNodes[Xpos][Ypos].visited && !allNodes[Xpos][Ypos].obstacle)
                        {
                            Debug.Log(Xpos + " " + Ypos);
                            allNodes[Xpos][Ypos].distance = currentdis;
                            allNodes[Xpos][Ypos].visited = true;
                            nodesToCheck.Add(allNodes[Xpos][Ypos]);
                        }

                        if (targetPos == new Vector2(Xpos, Ypos))
                        {
                            Debug.Log("Path Found !!");
                            return;
                        }
                    }
                    currentdis += 1;

                    nodesToCheck.Remove(currentNode);
                    Debug.Log(" iteration" + " " + nodesToCheck.Count + " " + currentNode.pos.x + "," + currentNode.pos.y);
                }
            }


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    debug += " " + allNodes[i][j].distance;
                }
                debug += "\n";
            }

            Debug.Log(debug);
        }

    */


}

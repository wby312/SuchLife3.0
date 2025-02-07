using System;
using UnityEngine;
using System.Collections.Generic;


public class DungeonNode 
{
    //node has 4 potential edges: top, bottom, left, right
    public enum EdgeDirection{top, bottom, left, right};
    public List<Tuple<DungeonNode, string>> edges; //other nodes that this node is connected to 
    public int xPos;
    public int yPos;
    public int depth;
    public string type;
    public DungeonNode()
    {
        //default vals
        depth = 0;
        xPos = 0;
        yPos = 0;
        type = "normal";
        edges = new List<Tuple<DungeonNode, string>>();
    }

    public string getOpenDirection()
    {
        List<string> Available = new List<string> { "top", "bottom", "left", "right"};
        //loop through edges and mark whats not available
        for(int i = 0; i < edges.Count; i++)
        {
            string tempEdgeDir = edges[i].Item2;
            if (Available.Contains(tempEdgeDir))
            {
                Available.Remove(tempEdgeDir);
            }
            
        }
        //get random edge from available directions
        string dir = Available[(int)UnityEngine.Random.Range(0,  Available.Count)];
        return dir;
    }


}

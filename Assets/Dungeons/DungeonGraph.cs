
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;



public class DungeonGraph 
{
    List<DungeonNode> layout = new List<DungeonNode>(); //finalized layout
    List<DungeonNode> availableNodes = new List<DungeonNode>(); //nodes that can be added to the graph
    List<DungeonNode> nodesAwaitingEdges = new List<DungeonNode>(); //nodes that have an entrance edge but are elligible for further branches
    public DungeonGraph(DungeonOptions opts)
    {
        createLayout(opts);
    }

    //algorithm will start at the first node and branch outward
    //every node has four possible locations for an edge: top, bottom, left, right
    //a node can only have one edge in each location
    private void createLayout(DungeonOptions opts)
    {
        //generate amount of nodes between maxnodes and minnodes
        int totalNodes = (int) UnityEngine.Random.Range(opts.minNodes, opts.maxNodes);
        //fill graph with blank nodes
        for (int i = 0; i < totalNodes; i++)
        {
            DungeonNode newNode = new DungeonNode();
            availableNodes.Add(newNode);
        }
        //start from 0th node and branch until maxDepth is hit
        //node will generate 0-4 'away' edges (1-4 for start)
        //creating an edge will also create the node it goes to

        //create initial branch to depth minDepth
        //bool branchIncomplete = true;
        int tempDepth = 0;

        //create first node
        DungeonNode firstNode = availableNodes[0];
        firstNode.depth = tempDepth;
        //remove from available nodes
        availableNodes.RemoveAt(0);
        nodesAwaitingEdges.Add(firstNode);
        //create first edge 
        DungeonNode nodeCreated = createNewEdge(firstNode, opts);
        //add first node to nodes awaiting edges
        

        //finalize branch by creating more nodes until maxdepth is hit
        //now that main branch has been created, start adding branches to 'nodes awaiting edges' starting from index 0
        for (int i = 1; i < opts.maxDepth; i++)
        {
            nodeCreated = createNewEdge(nodeCreated, opts);
        }
        printNodes();
    }

    private DungeonNode createNewEdge(DungeonNode originNode, DungeonOptions opts)
    {
        //pick a direction
        string dir = originNode.getOpenDirection();
        string oppdir = "nowhere";
        int newX = 0;
        int newY = 0;
        //place edge based on direction
        switch (dir)
        {
            case "top":
                newX = originNode.xPos; newY = originNode.yPos - 1;
                oppdir = "bottom";
                break;
            case "bottom":
                newX = originNode.xPos; newY = originNode.yPos + 1;
                oppdir = "top";
                break;
            case "left":
                newX = originNode.xPos - 1; newY = originNode.yPos;
                oppdir = "right";
                break;
            case "right":
                newX = originNode.xPos + 1; newY = originNode.yPos;
                oppdir = "left";
                break;
        }
        //check if node exists in direction
        DungeonNode neighborNode = nodeAtIndex(newX, newY);
        if (neighborNode != null )
        {
            //add an edge between the nodes but do not change available nodes
            Debug.Log("uh oh");
            return originNode;
        } else
        {
            //grab a node from availablenodes, update it, and add an edge between the two
            DungeonNode newNode = availableNodes[0];
            newNode.depth = originNode.depth + 1;
            newNode.xPos = newX; newNode.yPos = newY;
            Tuple<DungeonNode, string> newEdge = new Tuple<DungeonNode, string>(newNode, dir);
            originNode.edges.Add(newEdge);
            Tuple<DungeonNode, string> revEdge = new Tuple<DungeonNode, string>(originNode, oppdir);
            newNode.edges.Add(revEdge);
            //move new node from availablenodes to nodes awaiting edges
            nodesAwaitingEdges.Add(newNode);
            availableNodes.RemoveAt(0);
            return nodesAwaitingEdges[nodesAwaitingEdges.Count-1];
        }
    }
    private DungeonNode nodeAtIndex(int inputX, int inputY)
    {
        for(int i=0; i < nodesAwaitingEdges.Count; i++)
        {
            DungeonNode tempNode = nodesAwaitingEdges[i];
            if(tempNode.xPos == inputX && tempNode.yPos == inputY)
            {
                return tempNode;
            }
        }
        return null;
    }

    private void printNodes()
    {
        for(int i=0;i<nodesAwaitingEdges.Count;i++)
        {
            Debug.Log(i);
            Debug.Log("edges of node");
            for (int j = 0; j < nodesAwaitingEdges[i].edges.Count; j++)
            {
                
                Debug.Log(nodesAwaitingEdges[i].edges[j]);
            }
            
        }
    }
}

using UnityEngine;

public class DungeonOptions 
{
    //class to hold options for dungeon creation
    //has graph (layout) options and generation options

    public int maxNodes; //max amount of 'rooms'
    public int minNodes; //min amount of 'rooms' 
    public int maxEdges; //max edges(connections) per node 
    public int maxDepth; //tree will go this deep before creating branches
    
    public DungeonOptions()
    {
        //initialize default values
        maxNodes = 10;
        minNodes = 9;
        maxEdges = 4;
        maxDepth = 4;
    }
}

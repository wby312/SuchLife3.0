using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DungeonGraph 
{
    List<DungeonNode> layout;
    public DungeonGraph(DungeonOptions opts)
    {
        //initialize starting node
        DungeonNode startNode = new DungeonNode();
        layout.Append(startNode);
        createLayout(opts);
    }


    //algorithm will start at the first node and branch outward
    private void createLayout(DungeonOptions opts)
    {
        int depth = 0;
        for (int i = 0; i < opts.maxNodes; i++)
        {
            DungeonNode newNode = new DungeonNode();

        }
    }
}

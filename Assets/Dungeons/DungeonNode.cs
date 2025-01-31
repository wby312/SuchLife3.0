using NUnit.Framework;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections.Generic;

public class DungeonNode 
{

    public List<Tuple<DungeonNode, DungeonNode>> edges;
    public int depth;
    public DungeonNode()
    {

    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

public class DisplayDungeon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Tilemap dungeonTilemap;
    [SerializeField] Tile grassTile;
    
    void Start(){
        DungeonOptions opts = new DungeonOptions();
        //create dungeon graph
        DungeonGraph nodeGraph = new DungeonGraph(opts);
        //for basic testing
        //for each node in the graph, set the tile with the same index to a grasstile
        for (int i = 0; i < nodeGraph.layout.Count; i++)
        {
            DungeonNode node = nodeGraph.layout[i];
            dungeonTilemap.SetTile(new Vector3Int(node.xPos*3, node.yPos*3, 0), grassTile);
            //draw edges
            for (int j = 0; j < node.edges.Count; j++)
            {
                //draw em somehow
            }
        }
    }
}

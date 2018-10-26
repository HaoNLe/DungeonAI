using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNode {
    /*
    public HashSet<DungeonNode> merges;
    public HashSet<DungeonNode> connections;

    public DungeonNode()
    {
        this.merges = new HashSet<DungeonNode>();
        this.connections = new HashSet<DungeonNode>();
    }

    public void addMergedNode(DungeonNode dungeonNode)
    {
        this.merges.Add(dungeonNode);
    }

    public void addConnectedNode(DungeonNode dungeonNode)
    {
        this.connections.Add(dungeonNode);
    }

    public HashSet<DungeonNode> BFSsearch()
    {
        HashSet<DungeonNode> seen = new HashSet<>();
        Queue<DungeonNode> toExplore = new Queue<>();

        // Initialize queue with starting node
        toExplore.Enqueue(this);

        // While unexplored nodes exist
        while (toExplore.Count != 0)
        {
            // Pop node from front of queue
            DungeonNode currNode = toExplore.Dequeue();

            // Connected nodes need to be explored
            foreach (DungeonNode connectedNode in currNode.connections)
            {
                if (!seen.Contains(connectedNode))
                {
                    toExplore.Enqueue(connectedNode);
                }
            }
            
            // Merged nodes need to be explored
            foreach (DungeonNode mergedNode in currNode.merges)
            {
                if (!seen.Contains(mergedNode))
                {
                    toExplore.Enqueue(mergedNode);
                }
            }

            // Add currNode to seen since we're done adding children to the queue
            seen.add(currNode);

        }

        // return nodes we've seen
        return seen;
    }
    */
}

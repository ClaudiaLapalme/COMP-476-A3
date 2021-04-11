using System;
using Structs;
using UnityEngine;

namespace Utils
{
    public class PathfindingUtils
    {
        public static Tuple<float, float, float> CalculateMovement(CurrentAndNeighbourNodes nodes,
            Vector3 finalDestination)
        {
            // Calculate neighbour's g value
            var g = nodes.CurrentNode.StartingPointToThisNodeMovCost
                    + Vector3.Distance(nodes.CurrentNode.Position,
                        nodes.NeighbourNode.Position);

            // Calculate neighbour's h value
            var h = Mathf.Sqrt(Mathf.Pow(nodes.NeighbourNode.Position.x - finalDestination.x, 2) +
                              Mathf.Pow(nodes.NeighbourNode.Position.z - finalDestination.z, 2));

            // Calculate neighbour's f value
            var f = g + h;

            return new Tuple<float, float, float>(g, h, f);
        }
    }
}
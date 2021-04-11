using System.Collections.Generic;
using System.Linq;
using Graphs;
using Structs;
using UnityEngine;
using Utils;

namespace Ghost
{
    public class Pathfinding
    {
        #region private variables

        private List<Graph.Node> OpenList { get; } = new List<Graph.Node>();
        private List<Graph.Node> ClosedList { get; } = new List<Graph.Node>();

        private Graph.Node _initialNode;

        private Graph.Node _finalNode;

        private bool _isInitialNode;

        private bool _destinationReached;

        #endregion

        /**
         * Reinitialize the variables of the algo to be able to call it multiple times in one run
         */
        private void ReinitializePathfindingAlgo()
        {
            OpenList.Clear();
            ClosedList.Clear();

            _initialNode = null;
            _finalNode = null;
            
            _isInitialNode = false;
            _destinationReached = false;
        }
        
        /**
         * Finds the node closest to the player when the game starts and puts it on the open list
         */
        private void FindStartNode(Vector3 playerPos)
        {
            if (TileGraph.Graph.Nodes.Count != 0)
            {
                _initialNode =
                    TileGraph.Graph.Nodes.Aggregate((x, y) =>
                        Vector3.Distance(playerPos, x.Position) <
                        Vector3.Distance(playerPos, y.Position)
                            ? x
                            : y);
                NodeUtils.OpenNode(_initialNode, null, OpenList);

                _isInitialNode = true;
            }
            else
            {
                Debug.Log("there are no nodes in the graph.");
            }
        }

        /**
         * Handles all of the computation to find the shortest path
         * return the shortest path
         */
        public List<Graph.Node> ComputePathfinding(Vector3 ghostPos, Vector3 finalDestination)
        {
            ReinitializePathfindingAlgo();

            // start with the initial node
            FindStartNode(ghostPos);

            var currentNode = _initialNode;
            

            // if you start at the destination, you don't need to move
            if (currentNode.Position == finalDestination)
            {
                return null;
            }

            // while there are still nodes on the open list
            while (OpenList.Count != 0 && !_destinationReached)
            {
                // select the node on the open list with the smallest total movement cost (f)
                currentNode = CurrentNode();

                // do neighbour stuff
                HandleNeighbourNodes(currentNode, finalDestination);

                // pop the current node (q) off of the open list & add it to the closed list
                NodeUtils.CloseNode(currentNode, OpenList, ClosedList);
            }

            // if you've already been through the closest node, then the last target is the destination itself
            return ShortestPath();
        }

        private List<Graph.Node> ShortestPath()
        {
            var shortestPath = new List<Graph.Node>();
            var currentNode = _finalNode;

            while (currentNode.Parent != null)
            {
                shortestPath.Add(currentNode);
                currentNode.NodeColor = Color.grey;
                currentNode = currentNode.Parent;
            }

            shortestPath.Reverse();

            return shortestPath;
        }

        /**
         * finds the node that the player is currently at.
         * Either the current node is the initial node or the node on the open list with the smallest f.
         * Theoretically, the initial node check is unnecessary since the first node is found in another
         * function, but this check adds robustness
         *
         */
        private Graph.Node CurrentNode()
        {
            var currentNode = new Graph.Node();

            if (_isInitialNode)
            {
                _isInitialNode = false;
                return _initialNode;
            }

            foreach (var node in OpenList)
            {
                if (currentNode.Parent == null) // first node in the open list
                {
                    currentNode = node;
                }
                else if (node.TotalMovementCost < currentNode.TotalMovementCost) // node w/ smallest f --> current node
                {
                    currentNode = node;
                }
            }

            return currentNode;
        }

        /**
         * Compute the new f for all nodes on the open list
         * Adds neighbours to the open list if they aren't already on it
         */
        private void HandleNeighbourNodes(Graph.Node currentNode, Vector3 finalDestination)
        {
            // find q's (up to) 4 neighbours
            var neighbours = NodeUtils.NeighbourNodes(currentNode);

            foreach (var neighbourNode in neighbours)
            {
                var currentAndNeighbourNodes = new CurrentAndNeighbourNodes(currentNode, neighbourNode);

                // 1. if the neighbour is the destination, stop.
                if (HasReachedDestination(currentAndNeighbourNodes, finalDestination))
                {
                    _destinationReached = true;
                    return;
                }

                // 2. For each successor, compute & assign its g, h, and f values
                var ghf = PathfindingUtils.CalculateMovement(currentAndNeighbourNodes, finalDestination);

                var nodeIndexOpen = OpenList.FindIndex(element => element.Position == neighbourNode.Position);
                var nodeIndexClosed = ClosedList.FindIndex(element => element.Position == neighbourNode.Position);

                // --> if node is already in the open list, but with a bigger f, update it
                if (nodeIndexOpen != -1 && neighbourNode.TotalMovementCost > ghf.Item3)
                {
                    OpenList[nodeIndexOpen] = NodeUtils.UpdateNode(OpenList[nodeIndexOpen], ghf, currentNode);
                }
                else if (nodeIndexOpen == -1 && nodeIndexClosed == -1)
                {
                    NodeUtils.OpenNode(neighbourNode, currentNode, OpenList, ghf);
                }
            }
        }

        /**
         * Check if the current node is the destination
         * Return true if it is.
         */
        private bool HasReachedDestination(CurrentAndNeighbourNodes nodes, Vector3 finalDestination)
        {
            if (!(Vector3.Distance(nodes.NeighbourNode.Position, finalDestination) < 1)) return false;

            nodes.NeighbourNode.Parent = nodes.CurrentNode;
            _finalNode = nodes.NeighbourNode;
            return true;
        }
    }
}

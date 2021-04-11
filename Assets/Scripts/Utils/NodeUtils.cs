using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;
using UnityEngine;

// This script provides the utility functions for everything relating to the nodes
namespace Utils
{
    public static class NodeUtils
    {
        public static IEnumerable<Graph.Node> NeighbourNodes(Graph.Node currentNode)
        {
            // Find all of the neighbours of the current node
            var neighbourNodes = new List<Graph.Node>();

            var currentEdges =
                TileGraph.Graph.Edges.FindAll(edge => edge.Node1.Position == currentNode.Position);

            neighbourNodes.AddRange(currentEdges.Select(edge => edge.Node2).ToList());

            currentEdges = TileGraph.Graph.Edges.FindAll(edge => edge.Node2.Position == currentNode.Position);

            neighbourNodes.AddRange(currentEdges.Select(edge => edge.Node1).ToList());

            neighbourNodes = neighbourNodes.Distinct().ToList();

            return neighbourNodes.Distinct().ToList(); // remove duplicates
        }

        public static Graph.Node UpdateNode(Graph.Node nodeToUpdate, Tuple<float, float, float> ghf, Graph.Node parent)
        {
            var (g, _, f) = ghf;
            nodeToUpdate.StartingPointToThisNodeMovCost = g;

            nodeToUpdate.TotalMovementCost = f;

            nodeToUpdate.Parent = parent;

            return nodeToUpdate;
        }

        public static void OpenNode(Graph.Node nodeToOpen, Graph.Node parentNode, ICollection<Graph.Node> openList,
            Tuple<float, float, float> ghf = null)
        {
            if (ghf != null)
            {
                nodeToOpen.StartingPointToThisNodeMovCost = ghf.Item1;
                nodeToOpen.TotalMovementCost = ghf.Item3;
            }

            nodeToOpen.NodeColor = Color.green;
            nodeToOpen.Parent = parentNode;
            openList.Add(nodeToOpen);
        }

        public static void CloseNode(Graph.Node nodeToClose, ICollection<Graph.Node> openList,
            ICollection<Graph.Node> closedList)
        {
            openList.Remove(nodeToClose);
            closedList.Add(nodeToClose);
        }
    }
}
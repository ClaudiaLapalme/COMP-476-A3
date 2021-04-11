using System.Collections.Generic;
using UnityEngine;

// This script just contains the graph data structure. I have separated it from the other two files to reduce 
// dependencies.
namespace Graphs
{
    public class Graph
    {
        #region public variables

        public List<Node> Nodes { get; }
        public List<Edge> Edges { get; }

        #endregion

        public Graph()
        {
            Nodes = new List<Node>();
            Edges = new List<Edge>();
        }

        public Graph(List<Node> nodes, List<Edge> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }

        public class Node
        {
            public Node Cluster { get; set; }
            public Node Parent { get; set; }
            public Color NodeColor { get; set; }
            public Vector3 Position { get; }
            public float StartingPointToThisNodeMovCost { get; set; } // aka "g"
            public float TotalMovementCost { get; set; } // aka "f"

            public Node()
            {
            }

            public Node(Vector3 position)
            {
                NodeColor = Color.red;
                Position = position;
            }

            public Node(Vector3 position, Color colour)
            {
                NodeColor = colour;
                Position = position;
            }
        }

        public class Edge
        {
            public Color EdgeColor { get; }
            public Node Node1 { get; }
            public Node Node2 { get; }

            public Edge(Node node1, Node node2)
            {
                EdgeColor = Color.cyan;
                Node1 = node1;
                Node2 = node2;
            }
        }
    }
}
using UnityEngine;

// In this script I populate the tile graph with equidistant nodes and edges.
// If an edge or node is too close to or collides with an object, it is not added to the graph.
namespace Graphs
{
    public class TileGraph : MonoBehaviour
    {
        #region private variables

        private const int MAXWidth = 31;
        private const int MAXLength = 28;

        private const float IniXPos = 8.5f; // going towards 0 = going to the right
        private const float IniYPos = 22.3f;
        private const float IniZPos = -2.6f; // going towards 0 = going down
        private const float CollisionSphereRadius = 0.4f;

        [SerializeField] private GameObject pacDot;

        #endregion

        public static Graph Graph;

        private void Awake()
        {
            Initialize();
            foreach (var node in Graph.Nodes)
            {
                Instantiate(pacDot, node.Position, Quaternion.identity);
            }
        }

        public static void Initialize()
        {
            Graph = new Graph();
            AddNodes();
            AddEdges();
        }

        private static void AddNodes()
        {
            const int layerMask = 1 << 0; // only check for collisions on the default layer
            var currentPos = new Vector3(IniXPos, IniYPos, IniZPos);

            for (var i = 0; i < MAXLength; i++) // up to down
            {
                for (var j = 0; j < MAXWidth; j++) // left to right
                {
                    // if there are no objects in this space, add the node to the graph
                    if (!Physics.CheckSphere(currentPos, CollisionSphereRadius, layerMask))
                    {
                        Graph.Nodes.Add(new Graph.Node(currentPos));
                    }

                    currentPos.x++;
                }

                currentPos.x = IniXPos;
                currentPos.z++;
            }
        }

        private static void AddEdges()
        {
            for (var i = 0; i < Graph.Nodes.Count - 1; i++)
            {
                var nodePos = Graph.Nodes[i].Position;

                var nodeRight =
                    Graph.Nodes.Find(
                        node => node.Position == new Vector3(nodePos.x + 1, nodePos.y, nodePos.z));

                var nodeBottom =
                    Graph.Nodes.Find(
                        node => node.Position == new Vector3(nodePos.x, nodePos.y, nodePos.z - 1));

                if (nodeRight != null && !Physics.Linecast(nodePos, nodeRight.Position))
                {
                    Graph.Edges.Add(new Graph.Edge(Graph.Nodes[i], nodeRight));
                }

                if (nodeBottom != null && !Physics.Linecast(nodePos, nodeBottom.Position))
                {
                    Graph.Edges.Add(new Graph.Edge(Graph.Nodes[i], nodeBottom));
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (Graph == null)
            {
                Initialize();
            }
            else
            {
                foreach (var node in Graph.Nodes)
                {
                    Gizmos.color = node.NodeColor;
                    Gizmos.DrawSphere(node.Position, 0.2f);
                }
            }
        }
    }
}
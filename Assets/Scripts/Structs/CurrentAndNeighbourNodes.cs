using Graphs;

namespace Structs
{
    public readonly struct CurrentAndNeighbourNodes
    {
        public Graph.Node CurrentNode { get; }
        public Graph.Node NeighbourNode { get; }

        public CurrentAndNeighbourNodes(Graph.Node currentNode, Graph.Node neighbourNode)
        {
            CurrentNode = currentNode;
            NeighbourNode = neighbourNode;
        }
        
    }
}
using System.Collections.Generic;
using Graphs;
using UnityEngine;

namespace Ghost
{
    public class GhostMovementController : MonoBehaviour
    {
        #region serialized variables

        [SerializeField] private float maxSpeed = 5.0f;
        [SerializeField] private float time2Target = 0.25f;
        [SerializeField] private float maxAcceleration = 2.0f;

        #endregion

        #region private variables

        private float _currentAcceleration;
        private Vector3 _destination;
        private GameObject _player;
        private Pathfinding _pathfinding;
        private List<Graph.Node> _shortestPath = new List<Graph.Node>();
        private Graph.Node _currentNode;

        #endregion

        private void Start()
        {
            if (TileGraph.Graph == null)
            {
                TileGraph.Initialize();
            }

            _player = GameObject.Find("PacPerson");
            _pathfinding = new Pathfinding();
            _destination = _player.transform.position;
            
            _shortestPath = _pathfinding.ComputePathfinding(transform.position, _destination);
            TargetNode();
        }

        private void Update()
        {

            if (Vector3.Distance(transform.position, _destination) < 0.05f)
            {
                // TODO: handle the scenario where the ghost has collided with the player
            }
            else if (Vector3.Distance(transform.position, _currentNode.Position) < 0.05f)
            {
                _destination = _player.transform.position;
                _shortestPath = _pathfinding.ComputePathfinding(transform.position, _destination);
                TargetNode();
            }

            SteeringArrive();
        }

        /**
         * Steering is used to navigated from the current position to the next node in the algorithm.
         */
        private void SteeringArrive()
        {
            var distanceFromTarget = (_currentNode.Position - transform.position).magnitude;
            _currentAcceleration = Mathf.Min((maxSpeed - _currentAcceleration) / time2Target, maxAcceleration);

            // if you are not in the slowdown radius, keep approaching at full speed
            if (distanceFromTarget > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, _currentNode.Position,
                    (_currentAcceleration * Time.deltaTime));
            }
            else
            {
                transform.Translate(Vector3.zero);
            }
        }

        private void TargetNode()
        {
            _currentNode = _shortestPath[0];
            _currentNode.NodeColor = Color.cyan;
        }
    }
}
using System;
using UnityEngine;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;
        private const int LayerMask = 1 << 8; // only check collisions on the path layer
        private bool _pathIsAvailable;
        private bool _reachedNextUnit; // the player can only give a direction when the character has reached a unit
        private Vector3 _targetUnit;

        private void Start()
        {
            _targetUnit = gameObject.transform.position;
        }

        private void Update()
        {
            var pos = transform.position;
                       
            GetDirectionInput(pos);

            _reachedNextUnit = Math.Abs(_targetUnit.x - pos.x) < 0.02f && Math.Abs(_targetUnit.z - pos.z) < 0.02f;
            
            Move(pos);

            if (transform.position.y < 22.2f)
            {
                Teleport();
            }
        }

        private void GetDirectionInput(Vector3 pos)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _targetUnit = new Vector3(pos.x - 1, pos.y, pos.z);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _targetUnit = new Vector3(pos.x, pos.y, pos.z + 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _targetUnit = new Vector3(pos.x + 1, pos.y, pos.z);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _targetUnit = new Vector3(pos.x, pos.y, pos.z - 1);
            }
        }

        private void Move(Vector3 pos)
        {
            if (Physics.CheckSphere(_targetUnit, 0.5f, LayerMask) && !_reachedNextUnit)
            {
                transform.Translate((_targetUnit - pos).normalized * (speed * Time.deltaTime));
            }
        }
        
        private void Teleport()
        {
            //transform.Rotate(0, 180, 0);
            _reachedNextUnit = true;

            if (transform.position.z < -2.5f) // fell from the left side
            {
                gameObject.transform.position = new Vector3(transform.position.x, 22.5f, 24.4f);
            }
            else
            {
                gameObject.transform.position = new Vector3(transform.position.x, 22.5f, -2.5f);
            }

            _targetUnit = transform.position;
        }
    }
}
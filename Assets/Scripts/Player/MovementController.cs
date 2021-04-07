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
            var pos = gameObject.transform.position;
            
            _reachedNextUnit = Math.Abs(_targetUnit.x - pos.x) < 0.01f && Math.Abs(_targetUnit.z - pos.z) < 0.01f;

            if (!_reachedNextUnit)  {
                Move(pos, _targetUnit);
                return;
            }

            GetDirectionInput(pos);
        }

        private void GetDirectionInput(Vector3 pos)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(pos, new Vector3(pos.x - 1, pos.y, pos.z));
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(pos, new Vector3(pos.x, pos.y, pos.z + 1));
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(pos, new Vector3(pos.x + 1, pos.y, pos.z));
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(pos, new Vector3(pos.x, pos.y, pos.z - 1));
            }
        }

        private void Move(Vector3 pos, Vector3 unit)
        {
            if (Physics.CheckSphere(unit, 0.5f, LayerMask))
            {
                _targetUnit = unit;
                _reachedNextUnit = false;
                transform.Translate((_targetUnit - pos).normalized * (speed * Time.deltaTime));
            }
        }
    }
}
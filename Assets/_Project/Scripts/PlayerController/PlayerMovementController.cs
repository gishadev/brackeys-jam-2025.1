using System;
using BrackeysJam.PlayerController.InputReaders;
using UnityEngine;

namespace BrackeysJam.PlayerController
{
    public class PlayerMovementController : MonoBehaviour, IEnableable
    {
        private IInputReader<Vector2> _inputReader = new WASDInputReader();
        private Vector2 _movementVector;
        private float _speed;
        private Rigidbody2D _rb;
        
        private bool _enabled = false;

        public void Initialize(float speed, Rigidbody2D rb)
        {
            _speed = speed;
            _rb = rb;

            Enable();
        }
        
        private void Update()
        {
            _movementVector = _inputReader.Read() * _speed;
        }

        private void FixedUpdate()
        {
            _rb.AddForce(_movementVector, ForceMode2D.Impulse);
        }

        #region IEnableable

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        #endregion
    }
}
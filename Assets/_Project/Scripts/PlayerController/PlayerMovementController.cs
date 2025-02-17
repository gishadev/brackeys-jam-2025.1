using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BrackeysJam.PlayerController
{
    public class PlayerMovementController : MonoBehaviour, IEnableable
    {
        public Vector2 MoveInputVector { get; private set; }
        private bool _enabled = false;

        private float _speed;
        private Rigidbody2D _rb;
        private CustomInput _input;

        public void Initialize(float speed, Rigidbody2D rb)
        {
            _speed = speed;
            _rb = rb;

            Enable();
        }

        private void OnEnable()
        {
            _input ??= new CustomInput();
            _input.Enable();
            _input.Player.Movement.performed += OnMovementPerformed;
            _input.Player.Movement.canceled += OnMovementCanceled;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Movement.performed -= OnMovementPerformed;
            _input.Player.Movement.canceled -= OnMovementCanceled;
        }

        private void Update() => HandleMovementAnimation();
        private void FixedUpdate() => HandleBasicMovement();

        private void HandleBasicMovement()
        {
            _rb.linearVelocity = MoveInputVector * (_speed * Time.deltaTime);
        }

        private void HandleMovementAnimation()
        {
            if (MoveInputVector.magnitude > 0f && !DOTween.IsTweening(transform))
                transform.DOScaleY(.9f, .1f).SetEase(Ease.InSine).OnComplete(() =>
                {
                    transform.DOScaleY(1f, .1f).SetEase(Ease.InSine);
                    // STEP AUDIO.
                });
        }

        private void OnMovementPerformed(InputAction.CallbackContext value)
        {
            MoveInputVector = value.ReadValue<Vector2>();
        }

        private void OnMovementCanceled(InputAction.CallbackContext value)
        {
            MoveInputVector = Vector2.zero;
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
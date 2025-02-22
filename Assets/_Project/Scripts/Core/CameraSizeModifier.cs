using BrackeysJam.PlayerController;
using Unity.Cinemachine;
using UnityEngine;

namespace BrackeysJam.Core
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class CameraSizeModifier : MonoBehaviour
    {
        [SerializeField] private float _minSize = 7f;          
        [SerializeField] private float _maxSize = 8.5f;         
        [SerializeField] private float _maxVelocityMagnitude = 5f;          

        private CinemachineCamera _cam;
        private Rigidbody2D _targetRigidbody;

        private void Awake()
        {
            _cam = GetComponent<CinemachineCamera>();
            _targetRigidbody = FindAnyObjectByType<Player>().Rigidbody;
        }

        private void Update()
        {
            if (_targetRigidbody == null || _cam == null)
                return;

            float speed = _targetRigidbody.linearVelocity.magnitude;

            float newSize = Mathf.Lerp(_minSize, _maxSize, speed / _maxVelocityMagnitude);
            _cam.Lens.OrthographicSize = Mathf.Lerp(_cam.Lens.OrthographicSize, newSize, Time.deltaTime);
        }
    }
}
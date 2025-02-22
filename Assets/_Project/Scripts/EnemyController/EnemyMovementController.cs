using System.Collections.Generic;
using Aoiti.Pathfinding;
using BrackeysJam.Core;
using BrackeysJam.PlayerController;
using UnityEngine;

namespace BrackeysJam.EnemyController
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovementController : MonoBehaviourWithMovementEffector, IEnableable
    {
        [SerializeField] private float _gridSize = 0.5f;
        [SerializeField] private bool _drawDebugLines;

        private Pathfinder<Vector2> _pathfinder;
        private List<Vector2> _path = new();
        private List<Vector2> _pathLeftToGo = new();

        public float MoveSpeed { get; private set; }
        public bool IsEnabled => _enabled;

        public Rigidbody2D Rigidbody => _rb;

        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        
        private bool _enabled = false;
        private LayerMask _obstaclesMask;

        
        public void Initialize(Rigidbody2D rb, SpriteRenderer sr)
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _pathfinder = new Pathfinder<Vector2>(GetDistance, GetNeighbourNodes, 1000);

            _obstaclesMask = 1 << LayerMask.NameToLayer(Constants.OBSTACLE_LAYER_NAME);
            
            Enable();
        }
        
        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }


        private void FixedUpdate()
        {
            if (!IsDefaultMovementEnabled)
                return;
            
            if (_pathLeftToGo.Count > 0)
                HandleMovementToTarget();
        }

        private void Update()
        {
            if (_drawDebugLines)
                DrawDebugLines();
        }

        public void SetDestination(Vector2 target)
        {
            Vector2 closestNode = GetClosestNode(transform.position);
            if (_pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out _path))
            {
                _pathLeftToGo = new List<Vector2>(_path);
                _pathLeftToGo.Add(target);
            }
        }

        public void Stop()
        {
            _pathLeftToGo.Clear();
            Rigidbody.linearVelocity = Vector3.zero;
        }

        public void ChangeMoveSpeed(float newSpeed)
        {
            MoveSpeed = newSpeed;
        }

        public void FlipTowardsPosition(Vector2 position)
        {
            var dir = (Vector3) position - transform.position;
            _spriteRenderer.flipX = dir.x < 0;
        }

        private void HandleMovementToTarget()
        {
            var dir = (Vector3) _pathLeftToGo[0] - transform.position;
            FlipTowardsPosition(_pathLeftToGo[0]);
            Rigidbody.linearVelocity = dir.normalized * (MoveSpeed * Time.deltaTime);
            
            if (((Vector2) transform.position - _pathLeftToGo[0]).sqrMagnitude < .2f)
            {
                //transform.position = _pathLeftToGo[0];
                _pathLeftToGo.RemoveAt(0);
            }
        }

        private void DrawDebugLines()
        {
            for (int i = 0; i < _pathLeftToGo.Count - 1; i++)
                Debug.DrawLine(_pathLeftToGo[i], _pathLeftToGo[i + 1]);
        }

        private Dictionary<Vector2, float> GetNeighbourNodes(Vector2 pos)
        {
            Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) continue;

                    Vector2 dir = new Vector2(i, j) * _gridSize;
                    if (!Physics2D.Linecast(pos, pos + dir, _obstaclesMask))
                        neighbours.Add(GetClosestNode(pos + dir), dir.magnitude);
                }
            }

            return neighbours;
        }

        Vector2 GetClosestNode(Vector2 target)
        {
            return new Vector2(Mathf.Round(target.x / _gridSize) * _gridSize,
                Mathf.Round(target.y / _gridSize) * _gridSize);
        }

        private float GetDistance(Vector2 a, Vector2 b)
        {
            return (a - b).sqrMagnitude;
        }

        // private List<Vector2> ShortenPath(List<Vector2> path)
        // {
        //     List<Vector2> newPath = new List<Vector2>();
        //
        //     for (int i = 0; i < path.Count; i++)
        //     {
        //         newPath.Add(path[i]);
        //         for (int j = path.Count - 1; j > i; j--)
        //         {
        //             if (!Physics2D.Linecast(path[i], path[j], _obstaclesMask))
        //             {
        //                 i = j;
        //                 break;
        //             }
        //         }
        //
        //         newPath.Add(path[i]);
        //     }
        //
        //     newPath.Add(path[^1]);
        //     return newPath;
        // }

    }
}
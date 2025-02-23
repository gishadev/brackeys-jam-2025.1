using UnityEngine;

namespace BrackeysJam.EnemyController.Spawning
{
    /// <summary>
    /// Getting positions of rectangle bounds.
    /// </summary>
    public class RectangleBoundsSpawningPositionGetter : ISpawningPositionGetter
    {
        private readonly Vector2 _centerPos;
        private readonly Vector2 _rectangleBounds;

        public RectangleBoundsSpawningPositionGetter(Vector2 centerPos, Vector2 rectangleBounds)
        {
            _centerPos = centerPos;
            _rectangleBounds = rectangleBounds;
        }

        public Vector3 GetPosition()
        {
            Vector2 edgePos;
            int edge = Random.Range(0, 4);

            switch ((RectangleEdges)edge)
            {
                case RectangleEdges.TopEdge:
                    edgePos = new Vector2(
                        Random.Range(_centerPos.x - _rectangleBounds.x, _centerPos.x + _rectangleBounds.x),
                        _centerPos.y + _rectangleBounds.y);
                    break;
                case RectangleEdges.BottomEdge:
                    edgePos = new Vector2(
                        Random.Range(_centerPos.x - _rectangleBounds.x, _centerPos.x + _rectangleBounds.x),
                        _centerPos.y - _rectangleBounds.y);
                    break;
                case RectangleEdges.LeftEdge:
                    edgePos = new Vector2(
                        _centerPos.x - _rectangleBounds.x,
                        Random.Range(_centerPos.y - _rectangleBounds.y, _centerPos.y + _rectangleBounds.y));
                    break;
                case RectangleEdges.RightEdge:
                    edgePos = new Vector2(_centerPos.x + _rectangleBounds.x,
                        Random.Range(_centerPos.y - _rectangleBounds.y, _centerPos.y + _rectangleBounds.y));
                    break;
                default:
                    edgePos = _centerPos;
                    break;
            }

            return edgePos;
        }

        private enum RectangleEdges
        {
            TopEdge = 0,
            BottomEdge = 1,
            LeftEdge = 2,
            RightEdge = 3
        }
    }
}
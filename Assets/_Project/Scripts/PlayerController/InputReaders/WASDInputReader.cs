using System;
using UnityEngine;

namespace BrackeysJam.PlayerController.InputReaders
{
    [Obsolete("Use CustomInput")]
    public class WASDInputReader : IInputReader<Vector2>
    {
        public Vector2 Read()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }
    }
}
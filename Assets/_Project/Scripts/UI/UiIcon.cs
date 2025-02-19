using UnityEngine;
using UnityEngine.UI;

namespace BrackeysJam._Project.Scripts.UI
{
    public abstract class UiIcon<T> : MonoBehaviour where T : IIconParams
    {
        public abstract void SetIconParams(T buttonParams);
    }
}
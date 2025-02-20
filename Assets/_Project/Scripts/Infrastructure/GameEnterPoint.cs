using BrackeysJam.Core;
using gishadev.tools.SceneLoading;
using UnityEngine;
using Zenject;

namespace BrackeysJam.Infrastructure
{
    public class GameEnterPoint : MonoBehaviour
    {
        [Inject] private ISceneLoader _sceneLoader;

        private void Start()
        {
            _sceneLoader.AsyncSceneLoad(Constants.GAME_SCENE_NAME);
        }
    }
}
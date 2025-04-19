using NF.Main.Core;
using NF.Main.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NF.Main.UI
{
    public class UIManager : SingletonPersistent<UIManager>
    {
        [TabGroup("UI Screens")][SerializeField] private GameObject _pauseScreen;
        [TabGroup("UI Screens")][SerializeField] private GameObject _gameplayScreen;
        [TabGroup("UI Screens")][SerializeField] private GameObject _victoryScreen;
        [TabGroup("UI Screens")][SerializeField] private GameObject _gameOverScreen;

        [TabGroup("Pause Menu")][SerializeField] private Button _resumeButton;
        [TabGroup("Pause Menu")][SerializeField] private Button _retryButton;

        [TabGroup("Game Victory Screen")][SerializeField] private Button _retryOnWinButton;

        [TabGroup("Game Over Screen")][SerializeField] private Button _retryOnLoseButton;

        private void Awake()
        {
            Initialize();
        }

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            InitializeScreens();
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            _resumeButton?.onClick.AddListener(GameManager.Instance.OnPauseButtonPress);
            _retryButton?.onClick.AddListener(RestartScene);
            _retryOnWinButton?.onClick.AddListener(RestartScene);
            _retryOnLoseButton?.onClick.AddListener(RestartScene);
        }

        private void InitializeScreens()
        {
            ShowGameplayScreen();
            HidePauseScreen();
            HideVictoryScreen();
            HideGameOverScreen();
        }

        private void OnDisable()
        {
            _resumeButton?.onClick.RemoveAllListeners();
            _retryButton?.onClick.RemoveAllListeners();
            _retryOnWinButton?.onClick?.RemoveAllListeners();
            _retryOnLoseButton?.onClick?.RemoveAllListeners();
        }

        public void ShowPauseScreen()
        {
            _pauseScreen?.SetActive(true);
        }

        public void HidePauseScreen()
        {
            _pauseScreen?.SetActive(false);
        }

        public void ShowGameplayScreen()
        {
            _gameplayScreen?.SetActive(true);
        }

        public void HideGameplayScreen()
        {
            _gameplayScreen?.SetActive(false);
        }

        public void ShowVictoryScreen()
        {
            _victoryScreen?.SetActive(true);
        }

        public void HideVictoryScreen()
        {
            _victoryScreen?.SetActive(false);
        }

        public void ShowGameOverScreen()
        {
            _gameOverScreen?.SetActive(true);
        }

        public void HideGameOverScreen()
        {
            _gameOverScreen.SetActive(false);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
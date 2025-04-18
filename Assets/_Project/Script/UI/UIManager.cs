using NF.Main.Core;
using NF.Main.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
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
    }
}
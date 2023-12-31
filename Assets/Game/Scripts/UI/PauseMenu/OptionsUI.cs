using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsUI : MonoBehaviour {
    public static OptionsUI Instance;
    [SerializeField] private PauseMenuAnimation UI;
    [SerializeField] private PauseMenuUI pauseMenu;
    [SerializeField] private SettingsMenuUI settingsMenu;

    private bool isPauseActive = false;
    private bool isSettingsActive = false;
    public bool IsGamePaused { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        if (UI.gameObject.activeInHierarchy) UI.Hide();

        pauseMenu.continueBtn.onClick.AddListener(() => {
            PauseLogic(this, EventArgs.Empty);
        });

        pauseMenu.settingsBtn.onClick.AddListener(() => {
            isPauseActive = false;
            isSettingsActive = true;
        });

        pauseMenu.mainMenuBtn.onClick.AddListener(() => {
            Loader.LoadMainMenu();
        });

        settingsMenu.goBackButton.onClick.AddListener(()=>{
            PauseLogic(this, EventArgs.Empty);
        });

        GameInput.Instance.OnGamePaused += PauseLogic;
    }

    private void OnDestroy() {
        GameInput.Instance.OnGamePaused -= PauseLogic;
    }

    private void Update() {
        IsGamePaused = isPauseActive || isSettingsActive;
        
        GameManager.Instance.SetIsGamePaused(IsGamePaused);

        if (IsGamePaused) {
            UI.Show();
        } else {
            UI.HideAnimation();
        }

        if (isPauseActive) pauseMenu.Show();
        else pauseMenu.Hide();

        if (isSettingsActive) settingsMenu.Show();
        else settingsMenu.Hide();
    }

    private void PauseLogic(object sender, EventArgs e) {
        if (isSettingsActive) {
            isPauseActive = true;
            isSettingsActive = false;
            return;
        }

        if (!IsGamePaused) {
            isPauseActive = true;
        } else if (isPauseActive) {
            isPauseActive = false;
        }
    }
}

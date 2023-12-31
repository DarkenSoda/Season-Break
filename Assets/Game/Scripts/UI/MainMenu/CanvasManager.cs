using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {
    [SerializeField] private GameObject settingsManager;
    [SerializeField] private SettingsMenuUI settingsMenuUI;
    [SerializeField] private ButtonsHandler buttons;
    [SerializeField] private CreditsController credits;
    [SerializeField] private GameObject background;

    private void Awake() {
        Loader.LoadLast();
    }
    void Start() {
        GameManager.Instance.SetIsGamePaused(true);
        settingsMenuUI.goBackButton.onClick.AddListener(() => {
            settingsManager.SetActive(false);
            buttons.Show();
        });
        buttons.optionsButton.onClick.AddListener(() => {
            buttons.Hide();
            settingsManager.SetActive(true);
        });
        buttons.credits.onClick.AddListener(() => {
            buttons.Hide();
            credits.Show();
        });
        if (credits.gameObject.activeInHierarchy) {
            credits.gameObject.SetActive(false);
        }
        if (settingsManager.activeInHierarchy) {
            settingsManager.SetActive(false);
        }
    }

    public void ShowButtons() {
        buttons.Show();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour {
    [SerializeField] private Button nextLevelBtn;
    [SerializeField] private Button mainMenuBtn;

    private int nextLevelNumber;

    private void Start() {
        nextLevelBtn.onClick.AddListener(() => {
            Loader.LoadLevel(nextLevelNumber);
        });
        mainMenuBtn.onClick.AddListener(() => {
            Loader.LoadMainMenu();
        });
    }

    public void SetLevel(int levelNumber) {
        nextLevelNumber = levelNumber;
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}

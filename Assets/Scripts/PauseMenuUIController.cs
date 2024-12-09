using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUIController : MonoBehaviour
{
    [SerializeField] Sprite pauseSprite;
    [SerializeField] Sprite resumeSprite;

    [SerializeField] RectTransform menu;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    private Image _pauseButtonImage;
    void Awake()
    {
        _pauseButtonImage = gameObject.GetComponent<Image>();
        _pauseButtonImage.sprite = pauseSprite;
        restartButton.onClick.AddListener(() =>
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public void ToggleMenu()
    {
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);

        if (menu.gameObject.activeSelf)
            _pauseButtonImage.sprite = resumeSprite;
        else
            _pauseButtonImage.sprite = pauseSprite;
    }
    
}

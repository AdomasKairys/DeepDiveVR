using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUIController : MonoBehaviour
{
    [SerializeField] Sprite pauseSprite;
    [SerializeField] Sprite resumeSprite;

    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button pauseButton;

    private Image _pauseButtonImage;
    void Awake()
    {
        _pauseButtonImage = pauseButton.GetComponent<Image>();
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

        pauseButton.onClick.AddListener(() => { 
            ToggleMenu(); 
        });
    }

    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
            _pauseButtonImage.sprite = resumeSprite;
        else
            _pauseButtonImage.sprite = pauseSprite;
    }
    
}

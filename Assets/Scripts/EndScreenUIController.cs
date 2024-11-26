using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenUIController : MonoBehaviour
{
    [SerializeField] Button restartButton;
    void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        });
    }
}

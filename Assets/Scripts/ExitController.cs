using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    [SerializeField] RectTransform exitUI;
    [SerializeField] TextMeshProUGUI exitTimeText;
    [SerializeField] TextMeshProUGUI diveTimeText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            exitUI.gameObject.SetActive(true);
            exitTimeText.text = diveTimeText.text;
            gameObject.SetActive(false);
        }
    }
}

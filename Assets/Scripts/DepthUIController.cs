using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI depthText;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform waterLevel;

    private float depth = 0.0f;
    void Update()
    {
        depth = playerTransform.position.y - waterLevel.position.y;
        UpdateDepth();
    }
    void UpdateDepth() => depthText.text = Mathf.RoundToInt(depth).ToString();
}

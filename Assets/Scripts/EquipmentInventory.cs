using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment
{
    public GameObject gameObject;

    [Range(0f, 1f)] 
    public float heightRatio;
}
public class EquipmentInventory : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] Equipment[] equipments;

    private Vector3 _currentCameraPosition;
    private Quaternion _currentCameraRotation;

    void Update()
    {
        _currentCameraPosition = mainCamera.transform.position;
        _currentCameraRotation = mainCamera.transform.rotation;
        foreach(var equipment in equipments)
        {
            UpdateEquipmentHeight(equipment);
        }
        UpdateEquipmentInventory();
    }
    void UpdateEquipmentHeight(Equipment equipment)
    {
        equipment.gameObject.transform.position = new Vector3(equipment.gameObject.transform.position.x, _currentCameraPosition.y * equipment.heightRatio, equipment.gameObject.transform.position.z);
    }
    void UpdateEquipmentInventory()
    {
        transform.position = new Vector3(_currentCameraPosition.x, 0, _currentCameraPosition.z);
        transform.rotation = new Quaternion(transform.rotation.x, _currentCameraRotation.y, transform.rotation.z, _currentCameraRotation.w);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    [SerializeField] private bool isSeleted = false;
    
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCam.ScreenPointToRay(mousePos);

        bool isHit = Physics.Raycast(ray, out RaycastHit hit, 100);

        if (Input.GetMouseButtonDown(0))
            isSeleted = isHit;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0F;
    private Camera player_camera;

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    private GameObject inventoryPanel;

    private void Awake()
    {
        inventoryPanel = GameObject.Find(Names.inventoryPanel);
    }
    // Use this for initialization
    void Start () {
        player_camera = GameObject.Find(Names.playerCamera).GetComponent<Camera>();
        InitializeInventory();
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        Vision();
        PlayerInteract();
        ToggleInventoryPanel();
    }

    void ToggleInventoryPanel()
    {
        if (Input.GetKeyDown("tab"))
        {
            bool inventoryState = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!inventoryState);
        }
    }

    void InitializeInventory()
    {
        Inventory.inventoryInstance.AddItem(new Harpoon());
        Inventory.inventoryInstance.AddItem(new Diary());
    }

    void Movement() {

        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }

    void Vision() {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        player_camera.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        this.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, this.transform.up);
    }

    void PlayerInteract() {
        if (Input.GetMouseButtonDown(1)) {
            Ray myRay = player_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(myRay, out hit, 100)) {
                Debug.Log("We hitted..." + hit.collider.name);
                Interactable myInteract;
                try {
                    myInteract = hit.collider.GetComponent<Interactable>();
                    myInteract.Interact();
                } catch (Exception e) {
                    Debug.Log("Error: " + e.ToString()); //El objeto no tiene Interactable
                }
            }
        }
    }
}

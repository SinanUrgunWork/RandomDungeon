using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camControl : MonoBehaviour
{

    public string playerTag = "Player";
    public float cameraSpeed = 2f;

    private CinemachineFreeLook virtualCamera;
    private GameObject player;

    private void Start()
    {
        // Get the Cinemachine FreeLook component attached to the virtual camera
        virtualCamera = GetComponent<CinemachineFreeLook>();

        // Find the player GameObject using the specified tag
        player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            // Set the player as the Follow target of the virtual camera
            virtualCamera.Follow = player.transform;

            // Set the player as the LookAt target of the virtual camera
            virtualCamera.LookAt = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found with tag: " + playerTag);
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        // Adjust the camera's orbital speed based on user input
        float mouseX = Input.GetAxis("Mouse X") * cameraSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSpeed;

        virtualCamera.m_XAxis.Value += mouseX;
        virtualCamera.m_YAxis.Value += mouseY;
    }
}

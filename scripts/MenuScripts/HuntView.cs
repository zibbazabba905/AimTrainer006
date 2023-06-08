using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MenuScripts;
using PlayerScripts;

public class HuntView : MonoBehaviour
{
    private HudScript HudScript;
    private Camera PlayerCamera;

    private void Awake()
    {
        HudScript = GetComponent<HudScript>();
    }
    void Start()
    {
        LevelManager.Instance.OnPlayerSpawned += HandlePlayerSpawned;

    }

    private void HandlePlayerSpawned(PlayerScript player)
    {
        PlayerCamera = player.transform.Find("CameraRig/Camera").GetComponent<Camera>();
        LowerViewlikeHunt();
    }

    private void LowerViewlikeHunt()
    {
        PlayerCamera.lensShift = new Vector2(0f, 0.085f);
        HudScript.Crosshair.transform.position = new Vector3(Screen.width / 2, 432f, 0f);
    }
}

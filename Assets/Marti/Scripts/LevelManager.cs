using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private PlayerController player; //cambiar por player controller real
    private CameraController camController;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        camController = FindAnyObjectByType<CameraController>();
        SetCamOnPlayer();
    }

    void Update()
    {
        
    }

    public void SetCamOnPlayer()
    {
        camController.SetTarget(player.transform);
    }
}

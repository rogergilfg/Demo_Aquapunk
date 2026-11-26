using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private PlayerController player; //cambiar por player controller real
    private CameraController camController;

    [Header("Enemies")]
    [SerializeField] private EnemyController[] enemiesList;
    [HideInInspector] public float enemiesAttacking;
    public float maxEnemiesAttacking;

    void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        camController = FindAnyObjectByType<CameraController>();
        SetCamOnPlayer();
        enemiesList = FindObjectsByType<EnemyController>();
    }

    void Update()
    {
        
    }

    public void SetCamOnPlayer()
    {
        camController.SetTarget(player.transform);
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private CameraController camController;
    private LevelManager lM;
    private PlayerController player;
    [SerializeField] private Transform enemiesCameraPosition;
    [SerializeField] private float camPanTime;
    

    void Start()
    {
        camController = FindAnyObjectByType<CameraController>();
        lM = FindAnyObjectByType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            StartCombat();
        }
    }

    private void StartCombat()
    {
        StartCoroutine(CameraPan());
    }

    private IEnumerator CameraPan()
    {
        camController.SetTarget(enemiesCameraPosition);
        yield return new WaitForSeconds(camPanTime);
        lM.SetCamOnPlayer();
        gameObject.SetActive(false);
    }
}

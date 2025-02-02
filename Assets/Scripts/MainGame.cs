using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject playerPrefab;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint);
    }
}

using System.Collections;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject asteroidPrefab;
    [SerializeField]
    private int maxAsteroidsAmount;

    private int currentAsteroidsAmount;

    private Camera mainCamera;

  
    void Start()
    {
        mainCamera = Camera.main;

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.5f);
        
        SpawnAsteroid();
        currentAsteroidsAmount++;

        if(currentAsteroidsAmount < maxAsteroidsAmount)
        {
            StartCoroutine(Spawn());
        }
    }


    private void SpawnAsteroid()
    {
        int randomScreenSide = Random.Range(0, 4);

        //Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        Quaternion randomRotation = Quaternion.identity;
        Vector3 spawnPosition = Vector3.zero;
        float randomPoint = Random.Range(0f, 1f);

        switch (randomScreenSide)
        {
            case 0: //Top
                spawnPosition = new Vector3(randomPoint, 1.1f, mainCamera.nearClipPlane + 10f);
                //spawnPosition.y = 1.1f;
                //spawnPosition.x = randomPoint;
                randomRotation = Quaternion.LookRotation(-Vector3.forward);
                break;
            case 1: //Bottom
                spawnPosition = new Vector3(randomPoint, -0.1f, mainCamera.nearClipPlane + 10f);
                //spawnPosition.y = -0.1f;
                //spawnPosition.x = randomPoint;
                randomRotation = Quaternion.LookRotation(Vector3.forward);
                break;
            case 2: //Right
                spawnPosition = new Vector3 (1.1f, randomPoint, mainCamera.nearClipPlane + 10f);
                //spawnPosition.y = randomPoint;
                //spawnPosition.x = 1.1f;
                randomRotation = Quaternion.LookRotation(-Vector3.right);
                break;
            case 3: //Left
                spawnPosition = new Vector3 (-0.1f, randomPoint, mainCamera.nearClipPlane + 10f);
                //spawnPosition.y = randomPoint;
                //spawnPosition.x = -0.1f;
                randomRotation = Quaternion.LookRotation(Vector3.right);
                break;
        }

        Vector3 spawnWorldPosition = mainCamera.ViewportToWorldPoint(spawnPosition);
        //spawnWorldPosition.y = 0f;

        Instantiate(asteroidPrefab, spawnWorldPosition, randomRotation);
        
    }
}

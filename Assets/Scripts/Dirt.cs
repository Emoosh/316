using UnityEngine;

public class Dirt : MonoBehaviour
{
    private float cleanProgress = 0f;
    private bool isCleaning = false;
    private const float cleanTime = 2f;

    public GameObject progressBarPrefab;
    private GameObject progressBarInstance;

    private void Update()
    {
        if (isCleaning)
        {
            cleanProgress += Time.deltaTime;

            if (progressBarInstance != null)
            {
                progressBarInstance.transform.localScale = new Vector3(cleanProgress / cleanTime, 1, 1);
            }

            if (cleanProgress >= cleanTime)
            {
                Destroy(progressBarInstance);
                Destroy(gameObject);
            }
        }
    }

    public void StartCleaning(Transform player)
    {
        if (progressBarInstance == null)
        {
            progressBarInstance = Instantiate(progressBarPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
            progressBarInstance.transform.SetParent(transform);
        }

        isCleaning = true;
    }

    public void StopCleaning()
    {
        isCleaning = false;
        cleanProgress = 0f;
        if (progressBarInstance != null)
        {
            Destroy(progressBarInstance);
        }
    }
}

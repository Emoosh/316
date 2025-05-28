using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        // Bu objeden birden fazla varsa yenisini yok et
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}

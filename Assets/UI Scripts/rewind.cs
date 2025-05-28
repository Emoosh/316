using UnityEngine;
using UnityEngine.SceneManagement;

public class rewind : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); // ← Ana menü sahnenin adını yaz
    }
}

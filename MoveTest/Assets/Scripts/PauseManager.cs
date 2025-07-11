using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    public GameObject volumePanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            volumePanel.SetActive(!volumePanel.activeSelf);
            if (volumePanel.activeSelf)
            {
                Time.timeScale = 0;
                AudioManager.instance.PauseMusic();
            }
            else
            {
                Time.timeScale = 1;
                AudioManager.instance.ResumeMusic();
            }
        }
    }

    public void Retry(){
        AudioManager.instance.GetMusicEventInstance().stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Exit(){
        AudioManager.instance.GetMusicEventInstance().stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SceneManager.LoadScene("MainMenu");
    }
}

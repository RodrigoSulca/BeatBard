using UnityEngine;
using System.Collections;

namespace SlimUI.ModernMenu {
    public class CheckMusicVolume : MonoBehaviour {
        private const string volumeKey = "MusicVolume";
        private const float defaultVolume = 0.021f;

        public void Start () {
            // Si no existe la clave, la crea con valor inicial
            PlayerPrefs.SetFloat(volumeKey, defaultVolume);


            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(volumeKey);
        }

        public void UpdateVolume () {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(volumeKey);
        }
    }
}

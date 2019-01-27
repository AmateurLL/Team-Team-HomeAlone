using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SoundEffects {
    GhostDeath1 = 0,
    GhostDeath2,
    GhostDeath3,
    GhostDeath4,
    GhostExpand,
    WeaponTurnOn,
}

public enum MusicTracks {
    testmusic = 0,
}


namespace DigitalRuby.SoundManagerNamespace
{
    public class SoundMusicPlayer : MonoBehaviour
    {
        public Slider SoundSlider;
        public Slider MusicSlider;
        public InputField SoundCountTextBox;
        public Toggle PersistToggle;

        [SerializeField] List<AudioSource> SoundEffectsList = new List<AudioSource>();
        [SerializeField] List<AudioSource> MusicTracksList = new List<AudioSource>();


        public void PlaySound(SoundEffects _SoundEffect)
        {
            SoundEffectsList[(int)_SoundEffect].PlayOneShotSoundManaged(SoundEffectsList[(int)_SoundEffect].clip);
        }

        private void PlayMusic(MusicTracks _MusicTrack)
        {
            MusicTracksList[(int)_MusicTrack].PlayLoopingMusicManaged(1.0f, 1.0f, PersistToggle.isOn);
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                PlaySound(SoundEffects.GhostDeath1);
            }

        }


        public void SoundVolumeChanged()
        {
            SoundManager.SoundVolume = SoundSlider.value;
        }

        public void MusicVolumeChanged()
        {
            SoundManager.MusicVolume = MusicSlider.value;
        }
    }
}

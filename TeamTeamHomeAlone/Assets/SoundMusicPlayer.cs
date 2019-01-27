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
    FootStep1,
    FootStep2,
    FootStep3,
    FootStep4,
    FootStep5,
    FootStep6,
    FootStep7,
    FootStep8,
    OutsideStep1,
    OutsideStep2,
    OutsideStep3,
    OutsideStep4,
    OutsideStep5,
    OutsideStep6,
    OutsideStep7,
    OutsideStep8,
    PickUp,
    Throw,
}

public enum MusicTracks {
    testmusic = 0,
}


namespace DigitalRuby.SoundManagerNamespace
{
    public class SoundMusicPlayer : MonoBehaviour
    {
        public static SoundMusicPlayer Instance;

        public Slider SoundSlider;
        public Slider MusicSlider;
        public InputField SoundCountTextBox;
        public Toggle PersistToggle;

        [SerializeField] List<AudioSource> SoundEffectsList = new List<AudioSource>();
        [SerializeField] List<AudioSource> MusicTracksList = new List<AudioSource>();


        void Awake() {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        void OnDestroy() {
            if (Instance == this)
                Instance = null;

        }

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
            //if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //    PlaySound(SoundEffects.GhostDeath1);
            //}

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

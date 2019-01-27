using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour {

    [SerializeField] float m_MovementSpeed = 1f;
    [SerializeField] float m_StepEffectIntervals = 0.2f;
    [SerializeField]
    bool IsOutside = false;
    private float timer = 0f;
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        //If there is an input
        if (h > 0.2f || h < -0.2f || y > 0.2f || y < -0.2f || timer > 0) {
            timer += Time.deltaTime;

            if (timer > m_StepEffectIntervals) {
                timer = 0f;
                PlayRandomStep();
            }
        }

        this.transform.position += new Vector3(h, 0, y) * Time.deltaTime * m_MovementSpeed;


	}


    private void PlayRandomStep() {
        string SoundEffect = "";
        if (!IsOutside)
        {
            int rand = Random.Range(1, 9);
            SoundEffect = "FootStep" + rand;
        }
        else {
            int rand = Random.Range(1, 9);
            SoundEffect = "OutsideStep" + rand;
        }


        SoundEffects EffectToPlay = (SoundEffects) System.Enum.Parse(typeof(SoundEffects), SoundEffect);
        DigitalRuby.SoundManagerNamespace.SoundMusicPlayer.Instance.PlaySound(EffectToPlay);
    }
}

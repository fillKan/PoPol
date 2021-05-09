using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _AudioSource;

    private Coroutine _VoulmeCotrol;

    public void Play()
    {
        _AudioSource.Play();
    }
    public void VoulmeCotrol(float rateTime, float volume)
    {
        if (_VoulmeCotrol == null)
        {
            _VoulmeCotrol = new Coroutine(this);
        }
        _VoulmeCotrol.StartRoutine(EVoulmeCotrol(rateTime, volume));
    }
    private IEnumerator EVoulmeCotrol(float rateTime, float volume)
    {
        float current = _AudioSource.volume;

        for (float i = 0; i < rateTime; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i / rateTime, 1f);
            _AudioSource.volume = Mathf.Lerp(current, volume, ratio);

            yield return null;
        }
        _VoulmeCotrol.FinshRoutine();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterWarning : MonoBehaviour
{
    public InformationManager im;
    private AudioSource warningSource;
    private bool audioFinished = true;

    // Start is called before the first frame update
    void Start()
    {
        warningSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (im.GetDepth() < 0f && audioFinished)
        {
            // Play warning only upon entering from land
            StartCoroutine(SoundWarning());
        }

        if (im.GetDepth() > 0f)
        {
            warningSource.Stop();
            audioFinished = true;
        }
    }

    IEnumerator SoundWarning ()
    {
        audioFinished = false;
        warningSource.Play();
        yield return new WaitForSeconds(warningSource.clip.length);
        // audioFinished = true;
    }
}

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class SlowDownTime : MonoBehaviour {
    private Camera fpsCam;    
    private static PostProcessVolume postProcessingVolume;
    private static Vignette vignette;    
    private static float defaultVignetteIntensity = 0.25f, aimedIntensityVignette = 0.48f;

    // The audio that is affected by the slowmotion
    [Header ("Audio")]
    public AudioSource backgroundSound;
    private float fixedDeltaTime;
    
    void Awake () {
        fpsCam = GetComponent<Camera> ();
        postProcessingVolume = fpsCam.GetComponent<PostProcessVolume> ();
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update () {
        if (Input.GetKeyDown (KeyCode.Q)) {
            Time.timeScale = 0.4f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;

            //sound pitch adjustments
            backgroundSound.pitch = Mathf.Lerp (1.0f, 0.4f, Time.deltaTime * 500);

            //vignette adjustments
            postProcessingVolume.profile.RemoveSettings<Vignette> ();
            postProcessingVolume.profile.AddSettings<Vignette> ();
            
            postProcessingVolume.profile.TryGetSettings<Vignette> (out vignette);
            vignette.intensity.Override (Mathf.Lerp (defaultVignetteIntensity, aimedIntensityVignette, Time.deltaTime * 500));

        }

        if (Input.GetKeyUp (KeyCode.Q)) {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;

            //sound pitch adjustments
            backgroundSound.pitch = Mathf.Lerp (0.4f, 1.0f, Time.deltaTime * 500);

            //vignette adjustments
            postProcessingVolume.profile.RemoveSettings<Vignette> ();
            postProcessingVolume.profile.AddSettings<Vignette> ();

            postProcessingVolume.profile.TryGetSettings<Vignette> (out vignette);
            vignette.intensity.Override (Mathf.Lerp (aimedIntensityVignette, defaultVignetteIntensity, Time.deltaTime * 500));

        }

    }
}
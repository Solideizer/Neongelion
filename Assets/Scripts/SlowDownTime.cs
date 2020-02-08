using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class SlowDownTime : MonoBehaviour {
    private Camera fpsCam;    
    public static PostProcessVolume postProcessingVolume;
    public static Vignette vignette;
    public static int aimedIntensityMBlur = 20, defaultMBlurIntensity, currentMBlurIntensity;
    public static float defaultVignetteIntensity = 0.25f, aimedIntensityVignette = 0.45f, currentVignetteIntensity;

    // The audio that is affected by the slowmotion
    [Header ("Audio")]
    public AudioSource backgroundSound;
    //public AudioSource shot;
    //public AudioSource noAmmo;

    private float fixedDeltaTime;
    // Start is called before the first frame update
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
            vignette = postProcessingVolume.profile.AddSettings<Vignette> ();
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
            vignette = postProcessingVolume.profile.AddSettings<Vignette> ();
            postProcessingVolume.profile.TryGetSettings<Vignette> (out vignette);
            vignette.intensity.Override (Mathf.Lerp (aimedIntensityVignette, defaultVignetteIntensity, Time.deltaTime * 500));

        }

    }
}
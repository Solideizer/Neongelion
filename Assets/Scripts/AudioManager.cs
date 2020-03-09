using UnityEngine;

public class AudioManager : MonoBehaviour
{
  
    public static AudioClip gunShootSound;    
    public static AudioClip revRelaodSound;   
    public static AudioClip dashSound;   
    public static AudioClip arrowSound; 
    public static AudioSource audioSrc;

    // Start is called before the first frame update
    private void Start()
    {        
        gunShootSound = Resources.Load<AudioClip>("laser0");     
        revRelaodSound = Resources.Load<AudioClip>("revreload");   
        dashSound = Resources.Load<AudioClip>("dash"); 
        arrowSound = Resources.Load<AudioClip>("arrow");    

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {            
            case "gunshoot":
                audioSrc.PlayOneShot(gunShootSound, 1);
                break;
            case "reload":
                audioSrc.PlayOneShot(revRelaodSound, 1);
                break;
            case "dash":
                audioSrc.PlayOneShot(dashSound, 1);
              break;
            case "crossbowShoot":
                audioSrc.PlayOneShot(arrowSound, 1);
              break;
           
        }
    }
}
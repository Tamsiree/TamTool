using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance = null;

    private AudioSource actionSound;
    private AudioSource weaponAttackSound;

    public void Init()
    {
        Instance = this;
    }

    private Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        actionSound = GameObject.FindGameObjectWithTag(Tags.actionSound).GetComponent<AudioSource>();
        weaponAttackSound = GameObject.FindGameObjectWithTag(Tags.weaponAttackSound).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public AudioClip LoadAudioClip(string audioClipPath, bool cache = false)
    {
        AudioClip audioClip = null;
        if (!audioClipDict.TryGetValue(audioClipPath, out audioClip))
        {
            audioClip = Resources.Load<AudioClip>(audioClipPath);
            if (cache)
            {
                audioClipDict.Add(audioClipPath, audioClip);
            }
        }
        return audioClip;
    }

    public static void SwordNormalAttack()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/sword_swipe", true);
        Instance.weaponAttackSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.weaponAttackSound.Play();
    }

    public static void PlayerDeath()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/man_dead", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PickUpDrug()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/pick_up_drug", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PickUpWeapon()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/pick_up_weapon", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PickUpClothes()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/pick_up_clothes", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PickUpAccessory()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/pick_up_accessory", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PickUpRing()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/pick_up_ring", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PickUpHeadgear()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/pick_up_headgear", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PickUpShoe()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/pick_up_shoe", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PickUpWaistband()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/pick_up_waistband", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 1f;
        Instance.actionSound.Play();
    }

    public static void PlayerAttacked()
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/man_attacked", true);
        Instance.actionSound.clip = audioClip;
        Instance.actionSound.volume = 0.1f;
        Instance.actionSound.Play();
    }

    //----------------------------------------以下为敌人声音-----------------------------------------------

    public static void WolfNormalAttack(AudioSource audioSource)
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/Sword Swing", true);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public static void WolfCriticalAttack(AudioSource audioSource)
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/weapon-swing", true);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public static void WolfMiss(AudioSource audioSource)
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/Attack-Miss", true);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public static void WolfAttacked(AudioSource audioSource)
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/slime-hit", true);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public static void WolfDeath(AudioSource audioSource)
    {
        AudioClip audioClip = AudioManager.Instance.LoadAudioClip("Sound/wolf_death", true);
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}

using UnityEngine;
using System.Collections;

static public class Globals
{

    static public GameObject MainCamera;
    static public GameObject Player;
    
    static public bool paused;

    static public CharacterControllerScript playerScript;
    static public LevelMaster theLevelMaster;
    static public Serializer serializer;
    static public string currentMap;
    static public DebugMenu theDebugMenu;
    
    static public bool debug = false;

    static public TransitionScript transition;

    //sound stuff
    static public int musicVolume;
    static public int soundEffectVolume;
    static public AudioSource musicSource;
    static public AudioSource soundEffectSource;
    static public SoundHandler soundHandler;
}

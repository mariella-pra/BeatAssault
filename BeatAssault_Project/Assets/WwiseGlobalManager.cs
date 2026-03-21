using UnityEngine;
public class WwiseGlobalManager : MonoBehaviour
{
    private static WwiseGlobalManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
}
//using UnityEngine;

//public class CameraSequenceController : MonoBehaviour
//{
//    public Cinemachine.CinemachineVirtualCamera[] cameras; 
//    public float[] switchIntervals; 

//    private int currentCameraIndex = 0; 
//    private float timer = 0f; 

//    void Start()
//    {

//        ActivateCamera(0); 
//    }

//    void Update()
//    {
//        if (cameras.Length == 0) return; 

//        timer += Time.deltaTime;

       
//        if (timer >= switchIntervals[currentCameraIndex])
//        {
//            timer = 0f;
//            SwitchToNextCamera();
//        }
//    }

//    void SwitchToNextCamera()
//    {
        
//        currentCameraIndex++;
//        if (currentCameraIndex >= cameras.Length)
//        {
//            currentCameraIndex = 0; 
//        }

//        ActivateCamera(currentCameraIndex);
//    }

//    void ActivateCamera(int index)
//    {
//        for (int i = 0; i < cameras.Length; i++)
//        {
//            cameras[i].gameObject.SetActive(i == index); 
//        }
//    }
//}
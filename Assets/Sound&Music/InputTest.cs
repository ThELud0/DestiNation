using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            SoundManager.Instance.PlayArrivedToDestination();
            Debug.Log("PlayArrivedToDestination");
        }if (Input.GetKeyDown(KeyCode.B)){
            SoundManager.Instance.PlayPopUpBaby();
            Debug.Log("PlayPopUpBaby");
        }if (Input.GetKeyDown(KeyCode.Z)){
            SoundManager.Instance.PlayBabyEnd();
            Debug.Log("PlayBabyEnd");
        }if (Input.GetKeyDown(KeyCode.E)){
            SoundManager.Instance.PlayPopUpStation();
            Debug.Log("PlayPopUpStation");
        }if (Input.GetKeyDown(KeyCode.R)){
            SoundManager.Instance.PlayTrainAccident();
            Debug.Log("PlayTrainAccident");
        }if (Input.GetKeyDown(KeyCode.T)){
            SoundManager.Instance.PlayRailPose();
            Debug.Log("PlayRailPose");
        }if (Input.GetKeyDown(KeyCode.Y)){
            Debug.Log("PlayRailUnPose");
            SoundManager.Instance.PlayRailUnPose();
        }if (Input.GetKeyDown(KeyCode.U)){
            Debug.Log("PlayEndRailPose");
            SoundManager.Instance.PlayEndRailPose();
        }if (Input.GetKeyDown(KeyCode.I)){
            Debug.Log("PlayButton0");
            SoundManager.Instance.PlayButton0();
        }
    }
}

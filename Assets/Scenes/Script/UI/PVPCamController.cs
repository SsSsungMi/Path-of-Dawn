using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPCamController : MonoBehaviour
{
    public CinemachineVirtualCamera cam;

    private void Start()
    {
        Invoke("SetActiveFalse", 8.5f);
    }

    void SetActiveFalse()
    {
        cam.gameObject.SetActive(false);
    }
}

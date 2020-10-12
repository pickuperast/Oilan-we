using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class ZLimitCameraView : MonoBehaviour
{
    [Serializable]
    public class CameraLimit
    {
        public bool isActive = false;
        public float topLimitC;
        public float bottomLimitC;
        public float leftLimitC;
        public float rightLimitC;
    }

    [Serializable]
    public class PlayerLimit
    {
        public bool isActive = false;
        public float topLimitP;
        public float bottomLimitP;
        public float leftLimitP;
        public float rightLimitP;
    }

    public Transform player;
    public Transform cameraTransform;
    private AutoCam autoCam;
    private ProtectCameraFromWallClip protectCameraFromWall;

    [Header("Camera limit")]
    public CameraLimit[] cameraLimits;

    [Header("Palyer limit")]
    public PlayerLimit[] playerLimits;

    private void Start()
    {
        autoCam = GetComponent<AutoCam>();
        protectCameraFromWall = GetComponent<ProtectCameraFromWallClip>();
    }

    void Update()
    {
        if (cameraLimits.Length == playerLimits.Length)
        {
            for (int i = 0; i < cameraLimits.Length; i++)
            {
                if (cameraLimits[i].isActive)
                {
                   transform.position = new Vector3
                    (

                        Mathf.Clamp(transform.position.x, cameraLimits[i].leftLimitC, cameraLimits[i].rightLimitC),
                        Mathf.Clamp(transform.position.y, cameraLimits[i].bottomLimitC, cameraLimits[i].topLimitC),
                        transform.position.z
                    );

                    if (transform.position.x == cameraLimits[i].leftLimitC && player.transform.position.x < cameraLimits[i].leftLimitC ||
                        transform.position.x == cameraLimits[i].rightLimitC && player.transform.position.x > cameraLimits[i].rightLimitC ||
                        transform.position.y == cameraLimits[i].bottomLimitC && player.transform.position.y < cameraLimits[i].bottomLimitC ||
                        transform.position.y == cameraLimits[i].topLimitC && player.transform.position.y > cameraLimits[i].topLimitC)
                    {           
                        autoCam.enabled = false;
                    }
                    else
                    {                    
                        autoCam.enabled = true;
                    }
                }

                if (playerLimits[i].isActive)
                {
                    player.transform.position = new Vector3
                    (
                        Mathf.Clamp(player.transform.position.x, playerLimits[i].leftLimitP, playerLimits[i].rightLimitP),
                        Mathf.Clamp(player.transform.position.y, playerLimits[i].bottomLimitP, playerLimits[i].topLimitP),
                        player.transform.position.z
                    );
                }
            }
        }
        else
        {
            Debug.Log("CameraLimits and PlayerLimits must be equal to each other");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < cameraLimits.Length; i++)
        {
            Gizmos.DrawLine(new Vector2(cameraLimits[i].leftLimitC, cameraLimits[i].topLimitC),
                new Vector2(cameraLimits[i].rightLimitC, cameraLimits[i].topLimitC));
            Gizmos.DrawLine(new Vector2(cameraLimits[i].rightLimitC, cameraLimits[i].topLimitC),
                new Vector2(cameraLimits[i].rightLimitC, cameraLimits[i].bottomLimitC));
            Gizmos.DrawLine(new Vector2(cameraLimits[i].rightLimitC, cameraLimits[i].bottomLimitC),
                new Vector2(cameraLimits[i].leftLimitC, cameraLimits[i].bottomLimitC));
            Gizmos.DrawLine(new Vector2(cameraLimits[i].leftLimitC, cameraLimits[i].bottomLimitC),
                new Vector2(cameraLimits[i].leftLimitC, cameraLimits[i].topLimitC));


            Gizmos.DrawLine(new Vector2(playerLimits[i].leftLimitP, playerLimits[i].topLimitP),
                new Vector2(playerLimits[i].rightLimitP, playerLimits[i].topLimitP));
            Gizmos.DrawLine(new Vector2(playerLimits[i].rightLimitP, playerLimits[i].topLimitP),
                new Vector2(playerLimits[i].rightLimitP, playerLimits[i].bottomLimitP));
            Gizmos.DrawLine(new Vector2(playerLimits[i].rightLimitP, playerLimits[i].bottomLimitP),
                new Vector2(playerLimits[i].leftLimitP, playerLimits[i].bottomLimitP));
            Gizmos.DrawLine(new Vector2(playerLimits[i].leftLimitP, playerLimits[i].bottomLimitP),
                new Vector2(playerLimits[i].leftLimitP, playerLimits[i].topLimitP));
        }
    }
    IEnumerator ChangeActiveBorderCoroutine(int borderIndex)
    {     
        protectCameraFromWall.enabled = true;
     // autoCam.enabled = false;

        for (int i = 0; i < cameraLimits.Length; i++)
        {
            cameraLimits[i].isActive = false;
            playerLimits[i].isActive = false;
        }

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < cameraLimits.Length; i++)
        {
            if (i == borderIndex)
            {
                cameraLimits[i].isActive = true;
                playerLimits[i].isActive = true;
            }

        }

        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

        yield return new WaitForSeconds(0.1f);

        protectCameraFromWall.enabled = false;
       // cameraTransform.transform.position = new Vector3(0, cameraTransform.transform.position.y, cameraTransform.transform.position.z);
    }

    public void ChangeActiveBorder(int borderIndex)
    {
        StartCoroutine(ChangeActiveBorderCoroutine(borderIndex));
    }
}


using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;
using Unity.VideoHelper;
using System.IO;
using Oilan;

public class TVideoManager : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonRepeat;
    public Button buttonClose;

    public VideoController videoController;

    public bool linkToStreamingAssets;
    public string videoURL;
    // Start is called before the first frame update
    void Start()
    {
        //videoURL = "";
        ChangeURL(videoURL);
        buttonPlay.onClick.AddListener(TaskOnClick);
        buttonRepeat.onClick.AddListener(TaskOnRepeatClick);
        buttonClose.onClick.AddListener(TaskOnCloseClick);
    }

    public void ChangeURLFromWeb(string newURL)
    {
        linkToStreamingAssets = false;

        ChangeURL(newURL);
    }

    public void ChangeURLFromStreamingAssets(string newURL)
    {
        linkToStreamingAssets = true;
        ChangeURL(newURL);
    }

    public void ChangeURL(string newURL)
    {

        string filePath;
        if (linkToStreamingAssets) {
            filePath = Path.Combine(Application.streamingAssetsPath, newURL);
        } else {
            filePath = newURL;
        }

        videoURL = filePath;
    }

    private void TaskOnClick()
    {
        if (videoURL != videoController._url) {
            if (videoController.IsPlaying) {
                videoController.Pause();
            }
            videoController.PrepareForUrl(videoURL);
        } else {
            videoController.Play();
        }
    }

    public void TaskOnRepeatClick()
    {
        if (videoController.IsPlaying) {
            videoController.Pause();
        }

        videoController.Seek(0f);
        videoController.Play();
    }

    public void StartVideoPlayer()
    {
        videoController.StartNewVideo(videoURL);
    }

    private void TaskOnCloseClick()
    {
        if (videoController.IsPlaying) {
            videoController.Pause();
        }

        GameplayTimelineManager.Instance.PlayNextTimeline();
    }
}

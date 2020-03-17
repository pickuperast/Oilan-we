using Infinity;
using MainLogic;
using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VideoHelper;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Oilan
{
    public class MainScript: MonoBehaviour
    {
        public Button buttonPlay;
        
        public VideoController videoController;

        public string videoURL;
        public bool videoURL_isFound = false;
        public bool videoURL_isSet = false;


        // Start is called before the first frame update
        void Start()
        {
            videoURL = "";
            videoURL_isFound = false;
            videoURL_isSet = false;

            buttonPlay.onClick.AddListener(TaskOnClick);
        }

        public void ChangeURL(string newURL)
        {
            videoURL = newURL;
        }


        private void TaskOnClick()
        {
            //videoPlayer.Stop();

            GetURLAsync();

            StartCoroutine(SetVideoURL());

        }


        private async void GetURLAsync()
        {
            var log = new SuperLog(new UnityLog(), false);
            log.Send(true, Hi.AutoTestNumber);

            var youVideos = await new YoutubeHelper().GetVideos("mMyhMBsw65Y", log).ConfigureAwait(false);

            videoURL = youVideos[0].Url;

            videoURL_isFound = true;
        }

        private IEnumerator SetVideoURL()
        {
            while (!videoURL_isFound)
            {
                yield return new WaitForEndOfFrame();
            }

            videoURL_isSet = true;

            videoController.PrepareForUrl(videoURL);
            //videoController.PrepareForUrl("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");

            yield return null;
        }

        //private IEnumerator GetURL()
        //{
        //    var log = new SuperLog(new UnityLog(), false);
        //    log.Send(true, Hi.AutoTestNumber);

        //    Task youVideosTask = new YoutubeHelper().GetVideos("mMyhMBsw65Y", log);

        //    while (!youVideosTask.IsCompleted || !youVideosTask.IsFaulted || !youVideosTask.IsCanceled)
        //    {
        //        yield return new WaitForEndOfFrame();
        //    }

        //    if (youVideosTask.IsCompleted)
        //    {
        //        var youVideos = youVideosTask.Result;

        //        Debug.Log("URL found");
        //        Debug.Log(youVideos[0].Url);
        //        //videoPlayer.url = youVideos[0].Url;

        //        //videoPlayer.Play();

        //        videoController.PrepareForUrl(youVideos[0].Url);
        //    }
        //    else
        //    {
        //        Debug.Log("URL not found!");
        //    }

        //    yield return null;
        //}

    }
}
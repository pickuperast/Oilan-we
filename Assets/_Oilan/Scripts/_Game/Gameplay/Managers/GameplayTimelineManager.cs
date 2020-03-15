using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Oilan
{
    [Serializable]
    public class TimelineItem
    {
        public string name;
        public PlayableDirector timeline;
    }

    public class GameplayTimelineManager : MonoBehaviour
    {
        public static GameplayTimelineManager Instance;

        public List<TimelineItem> timelineItems;

        private PlayableDirector currentTimeline = null;

        private int lastTimelineID = 0;

        private void Awake()
        {
            Instance = this;
            lastTimelineID = 0;
        }

        public void PlayFirstTimeline()
        {
            if(timelineItems.Count > 0)
            {
                PlayTimeline(timelineItems[0].name);
            }
        }
        public void PlayNextTimeline()
        {
            if (timelineItems.Count > lastTimelineID + 1)
            {
                PlayTimeline(timelineItems[lastTimelineID + 1].name);
            }
        }

        public void PlayTimeline(string mName)
        {
            foreach (var item in timelineItems)
            {
                item.timeline.gameObject.SetActive(false);
            }

            currentTimeline = null;
            for (int i = 0; i < timelineItems.Count; i++)
            {
                TimelineItem item = timelineItems[i];

                if (item.name == mName)
                {
                    currentTimeline = item.timeline;

                    lastTimelineID = i;

                    currentTimeline.gameObject.SetActive(true);
                    currentTimeline.Play();
                    break;
                }
            }
        }

        public void StopAllTimelines()
        {
            if (currentTimeline != null)
            {
                currentTimeline.Stop();
            }

            foreach (var item in timelineItems)
            {
                item.timeline.gameObject.SetActive(false);
            }

        }
        
    }
}
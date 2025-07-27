using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class StoryPage
{
    public GameObject panel;       //E: the page/panel GameObject
    public GameObject[] frames;    //E: the frames/images on that panel
}

public class FrameManager : MonoBehaviour
{
    [Header("Pages")]
    public List<StoryPage> pages = new List<StoryPage>(); //E: list of all pages

    private int currentPageIndex = 0;                     //E: tracks current page
    private int[] currentFrameIndices;                    //E: track frame index per page

    void Start()
    {
        //E: disable all panels and all their frames at the start
        foreach (var page in pages)
        {
            page.panel.SetActive(false);
            foreach (var frame in page.frames)
                frame.SetActive(false);
        }

        //E: initialise frame index tracking per page
        currentFrameIndices = new int[pages.Count];

        //E: show first panel and its first frame
        if (pages.Count > 0 && pages[0].frames.Length > 0)
        {
            pages[0].panel.SetActive(true);
            pages[0].frames[0].SetActive(true);
        }
    }

    //E: called when next is triggered 
    public void ShowNextFrame()
    {
        //E: get the current page and frame index
        var page = pages[currentPageIndex];
        int currentFrame = currentFrameIndices[currentPageIndex];

        //E: if more frames remain on this page show next frame
        if (currentFrame < page.frames.Length - 1)
        {
            currentFrame++;
            currentFrameIndices[currentPageIndex] = currentFrame;
            page.frames[currentFrame].SetActive(true);
        }
        else
        {
            //E: if there are more pages go to the next one
            if (currentPageIndex < pages.Count - 1)
            {
                page.panel.SetActive(false); //E: hide current page
                currentPageIndex++;          //E: go to next page
                var nextPage = pages[currentPageIndex];
                nextPage.panel.SetActive(true); //E: show new page

                //E: show first frame of new page
                int frameIndex = currentFrameIndices[currentPageIndex];
                if (nextPage.frames.Length > 0)
                    nextPage.frames[frameIndex].SetActive(true);
            }
            else
            {
                Debug.Log("End of story"); //E: last page reached
            }
        }
    }

    //E: called when back is triggered 
    public void ShowPreviousPage()
    {
        if (currentPageIndex > 0)
        {
            //E: hide current page
            pages[currentPageIndex].panel.SetActive(false);

            //E: go back to previous page
            currentPageIndex--;

            //E: show previous page
            var prevPage = pages[currentPageIndex];
            prevPage.panel.SetActive(true);

            //E: show current frame on that page and track progress
            int frameIndex = currentFrameIndices[currentPageIndex];
            if (prevPage.frames.Length > 0)
                prevPage.frames[frameIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Already on first page"); //E: can't go back further
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class FrameManager : MonoBehaviour
{
    [Header("Page 1")]
    public GameObject panel1;
    public GameObject[] storyFrames1;

    [Header("Page 2")]
    public GameObject panel2;
    public GameObject[] storyFrames2;

    private int currentIndex = 0;
    private int currentPage = 1;

    void Start()
    {
        //E: disable all frames at start (frame 1 will just always be active)
        foreach (GameObject img in storyFrames1) img.SetActive(false);
        foreach (GameObject img in storyFrames2) img.SetActive(false);

        //E: setting panel states
        panel1.SetActive(true);
        panel2.SetActive(false);

        //E: show the first frame of page 1
        if (storyFrames1.Length > 0)
        {
            storyFrames1[0].SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) //E: will change the button to our controller
        {
            ShowNextFrame();
        }
    }

    void ShowNextFrame()
    {
        GameObject[] currentFrames = currentPage == 1 ? storyFrames1 : storyFrames2;
        GameObject currentPanel = currentPage == 1 ? panel1 : panel2;

        if (currentIndex < currentFrames.Length - 1)
        {
            currentIndex++;
            currentFrames[currentIndex].SetActive(true);
        }
        else
        {
            if (currentPage == 1)
            {
                //E: Go to page 2
                panel1.SetActive(false);
                panel2.SetActive(true);

                currentPage = 2;
                currentIndex = 0;

                if (storyFrames2.Length > 0)
                    storyFrames2[0].SetActive(true);
            }
            else
            {
                Debug.Log("End of story"); //E: we can add logic 
            }
        }
    }
}

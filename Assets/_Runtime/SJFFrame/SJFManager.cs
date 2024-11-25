using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SJFManager : MonoBehaviour
{
    public Button BtnStartProgress;
    public TMP_InputField InfNhapProgress;
    public Transform ContentInputBarProgress;
    public InputBarProgress InputBarProgressPrefab;

    public SJF SJF;

    [Header("ProgressBar")]
    public Transform ContentProgressBar;
    public ProgressBar ProgressBarPrefab;

    private void Start()
    {
        BtnStartProgress.onClick.AddListener(StartProgress);
        InfNhapProgress.onValueChanged.AddListener(OnInputProgressChanged);
    }

    public void ClearProgressBar()
    {
        foreach (Transform child in ContentProgressBar)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateProgressBar(string name, int arrivalTime, int brustTime, int currentTime)
    {
        ProgressBar newProgressBar = Instantiate(ProgressBarPrefab, ContentProgressBar);
        newProgressBar.TextName.text = name;
        newProgressBar.TextTime1.text = currentTime.ToString();
        // newProgressBar.TextTime2.text = (brustTime + startTime).ToString();
        newProgressBar.duration = brustTime;
        newProgressBar.targetWidth = 100 + (brustTime * 100);
        newProgressBar.isScaling = true;
    }

    private void StartProgress()
    {
        ClearProgressBar();
        SJF.processes.Clear();
        SJF.processesDone.Clear();
        SJF.processes = GetAllProcess();
        SJF.StartSJF();
    }

    private List<SJF.Process> GetAllProcess()
    {
        List<InputBarProgress> inputBarProgressList = FindObjectsOfType<InputBarProgress>().ToList();
        List<SJF.Process> processes = new List<SJF.Process>();

        foreach (InputBarProgress inputBarProgress in inputBarProgressList)
        {
            SJF.Process process = new SJF.Process();
            process.name = inputBarProgress.TxtName.text;
            process.arrivalTime = int.Parse(inputBarProgress.InfNhapAT.text);
            process.brustTime = int.Parse(inputBarProgress.InfNhapBT.text);
            processes.Add(process);
        }

        return processes;
    }

    private void OnInputProgressChanged(string value)
    {
        int inputValue = int.Parse(value);
        if (inputValue > 0)
        {
            // Xóa tất cả các InputBarProgress hiện tại
            foreach (Transform child in ContentInputBarProgress)
            {
                Destroy(child.gameObject);
            }

            // Tạo các InputBarProgressPrefab mới với số lượng inputValue, giới hạn số lượng là 100
            int limitedInputValue = Mathf.Min(inputValue, 100);
            for (int i = 0; i < limitedInputValue; i++)
            {
                InputBarProgress newInputBarProgress = Instantiate(InputBarProgressPrefab, ContentInputBarProgress);
                newInputBarProgress.TxtName.text = "P" + (i + 1);
            }
        }
    }
}


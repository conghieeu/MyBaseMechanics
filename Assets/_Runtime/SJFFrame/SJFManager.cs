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

    [Header("KhachHangP")]
    public KhachHangP khachHangPPrefabs;
    public Transform SpawnPoint;
    public Transform WaitingPoint;
    public Transform OutPoint;

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

    public void ClearKhachHangP()
    {
        foreach (Transform child in SpawnPoint)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnNextProcess(SJF.Process nextProcess, int startTime)
    {
        SpawnKhachHangP(nextProcess);
        CreateProgressBar(nextProcess.name, nextProcess.arrivalTime, nextProcess.brustTime, startTime);
    }

    private void SpawnKhachHangP(SJF.Process nextProcess)
    {
        KhachHangP khachHangP = Instantiate(khachHangPPrefabs, GetRandomSpawnPoint(SpawnPoint.position), Quaternion.identity);
        khachHangP.brustTime = nextProcess.brustTime;
        khachHangP.waitingPoint = WaitingPoint;
        khachHangP.outPoint = OutPoint;
        khachHangP.TextName.text = nextProcess.name;
    }

    private Vector3 GetRandomSpawnPoint(Vector3 spawnPoint)
    {
        float randomX = Random.Range(spawnPoint.x - 3, spawnPoint.x + 3); 
        float randomZ = Random.Range(spawnPoint.z - 3, spawnPoint.z + 3);
        return new Vector3(randomX, 0, randomZ);
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
        ClearKhachHangP();
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


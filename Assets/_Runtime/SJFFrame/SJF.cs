using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SJF : MonoBehaviour
{
    public List<Process> processes = new List<Process>();
    public List<Process> processesDone = new List<Process>();
    public SJFManager SJFManager;
    public TextMeshProUGUI TextResult;

    void Start()
    {
        SJFManager = FindObjectOfType<SJFManager>();

        if (processes.Count > 0)
        {
            DuLieuGiaDinh();
            StartSJF();
        }
    }

    // Dữ liệu giả định, mỗi chuỗi tương ứng với một dòng trong file input
    private void DuLieuGiaDinh()
    {
        string[] lines = new string[] {
            "P1\t2\t1",
            "P2\t1\t5",
            "P3\t4\t1",
            "P4\t0\t6",
            "P5\t2\t3"
        };

        foreach (string line in lines)
        {
            string[] tabs = line.Split('\t');
            Process x = new Process(tabs[0], int.Parse(tabs[1]), int.Parse(tabs[2]));
            processes.Add(x);
        }
    }

    public void StartSJF()
    {
        StartCoroutine(SJFRecursive(0));
    }

    private IEnumerator SJFRecursive(int currentTime)
    {
        // Tìm quá trình có thời gian thực hiện ngắn nhất mà thời gian đến <= thời gian hiện tại
        Process nextProcess = processes.Where(p => p.arrivalTime <= currentTime).OrderBy(p => p.brustTime).FirstOrDefault();

        if (nextProcess != null)
        {
            int startTime = currentTime;
            int endTime = startTime + nextProcess.brustTime;
            int waitTime = startTime - nextProcess.arrivalTime;
            int turnaroundTime = endTime - nextProcess.arrivalTime;

            // Cập nhật thời gian cho quá trình này
            nextProcess.completionTime = endTime;
            nextProcess.turnaroundTime = turnaroundTime;
            nextProcess.waitingTime = waitTime;
            nextProcess.responseTime = waitTime; // Giả sử thời gian phản hồi bằng thời gian chờ

            // In thông tin quá trình
            Debug.Log(string.Format($"Name {nextProcess.name}   Arrival {nextProcess.arrivalTime}   Brust {nextProcess.brustTime}   Start {startTime}   End {endTime}   Wait {waitTime}   Turnaround {turnaroundTime}"));
            TextResult.text += $"Name {nextProcess.name}   Arrival {nextProcess.arrivalTime}   Brust {nextProcess.brustTime}   Start {startTime}   End {endTime}   Wait {waitTime}   Turnaround {turnaroundTime} \n";

            // Loại bỏ quá trình này khỏi danh sách và tiếp tục với quá trình tiếp theo
            processes.Remove(nextProcess);
            processesDone.Add(nextProcess);
 
            SJFManager.CreateProgressBar(nextProcess.name, nextProcess.arrivalTime, nextProcess.brustTime, startTime);

            // Đợi cho đến khi quá trình này hoàn thành, trừ khi đây là quá trình cuối cùng
            if (processes.Count > 0)
            {
                yield return new WaitForSeconds(nextProcess.brustTime);
            }

            // Tiếp tục đệ quy
            yield return StartCoroutine(SJFRecursive(endTime));
        }
        else
        {
            // Tính toán và in ra thời gian chờ trung bình và thời gian hoàn thành trung bình
            float averageWaitTime = (float)processesDone.Average(p => p.waitingTime);
            float averageTurnaroundTime = (float)processesDone.Average(p => p.turnaroundTime);
            Debug.Log($"Average Waiting Time: {averageWaitTime}");
            Debug.Log($"Average Turnaround Time: {averageTurnaroundTime}");

            TextResult.text += $"Average Waiting Time: {averageWaitTime}\nAverage Turnaround Time: {averageTurnaroundTime}";
        }
    }

    [Serializable]
    public class Process
    {
        public Process(string name, int arrivalTime, int brust)
        {
            this.name = name;
            this.arrivalTime = arrivalTime;
            this.brustTime = brust;
        }
        public Process()
        {

        }

        public string name;
        public int arrivalTime; // thời gian đến AT
        public int brustTime; // thời gian thực hiện BT 
        public int completionTime; // thời gian hoàn thành CT
        public int waitingTime; // thời gian chờ WT
        public int responseTime; // thời gian phản hồi RT
        public int turnaroundTime; // thời gian hoàn thành TT
    }
}
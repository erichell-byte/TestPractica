using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

public class LoadTextFromServer : MonoBehaviour
{
    [Header("Server URL")] [SerializeField]
    private string _url;

    [Header("Name of the game")] [SerializeField]
    public string _fileName;

    public bool isComplete = false;

    private void Start() => DownloadFile();


    public void DownloadFile()
    {
        WebClient client = new WebClient();

        client.DownloadProgressChanged += DownloadProgressChanged;
        client.DownloadFileCompleted += DownloadComplete;
        
        client.DownloadFileAsync(new Uri(_url), Application.dataPath + $"/Resources/{_fileName}.txt");
    }

    private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        Debug.Log("Download Progress = " + e.ProgressPercentage + "%");
    }

    private void DownloadComplete(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            Debug.Log("Загрузка завершена");
            isComplete = true;
        }
        else
        {
            Debug.Log($"Error :  {e.Error}") ;
        }
    }
    
    


}

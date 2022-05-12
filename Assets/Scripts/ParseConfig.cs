using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ParseConfig : MonoBehaviour
{
    private LoadTextFromServer loadTextFromServer;
    public Vector3[] filePoints;
    public bool isLoop;
    public int walkingTime;
    public bool _isParse = false;
    
    private void Awake()
    {
        loadTextFromServer = GetComponent<LoadTextFromServer>();
        filePoints = new Vector3[1];
    }

    void Update()
    {
        if (loadTextFromServer.isComplete && !_isParse)
        {
            ParseFileConfig();
            _isParse = true;
        }
    }
        
    
    void ParseFileConfig()
    {
        string[] readedLines =
            File.ReadAllLines(Application.dataPath + "/Resources/" + loadTextFromServer._fileName + ".txt");

        foreach (string line in readedLines)
        {
            string tmpLine;
            tmpLine = line.Replace(" ", "");
            int i;
            if (tmpLine.Contains("map"))
            {
                for (i = 0; i < tmpLine.Length; i++)
                {
                    string x = "";
                    string y = "";
                    string z = "";
                    while (tmpLine[i] != '{')
                        i++;
                    while (tmpLine[++i] != ',')
                        x += tmpLine[i];
                    while (tmpLine[++i] != ',')
                        y += tmpLine[i];
                    while (tmpLine[++i] != '}')
                        z += tmpLine[i];
                    Array.Resize(ref filePoints, filePoints.Length + 1);
                    filePoints[filePoints.Length - 1] = new Vector3(Int16.Parse(x), Int16.Parse(y), Int16.Parse(z));
                    Debug.Log(filePoints[filePoints.Length - 1]);
                }
            }

            if (tmpLine.Contains("loop"))
            {
                i = tmpLine.IndexOf('=');
                if (i != -1)
                {
                    i++;
                    if (tmpLine[i] == '1' && !Char.IsNumber(tmpLine[i + 1]))
                        isLoop = true;
                    else
                        isLoop = false;

                }
                Debug.Log(isLoop);
            }

            if (tmpLine.Contains("time"))
            {
                string tmpTime = "";
                i = tmpLine.IndexOf('=');
                if (i != -1)
                {
                    i++;
                    for (; i < tmpLine.Length; i++)
                    {
                        tmpTime += tmpLine[i];
                    }

                    walkingTime = Int16.Parse(tmpTime);
                    Debug.Log(walkingTime);
                }
            }
        }
    }
}

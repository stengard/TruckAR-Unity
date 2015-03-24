using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Debugga {

    public bool doDebug = false;

    public List<string> stringOfLogs;

    private static Debugga instance;

    private Debugga() {
        stringOfLogs = new List<string>();
    }

    public static Debugga Instance {
        get {
            if (instance == null) {
                instance = new Debugga();
            }
            return instance;
        }
    }

    public static void Logga(string s) {
        if (Instance.doDebug) Debug.Log(s);
    }

    public static void LoggaVarning(string s) {
        if (Instance.doDebug) Debug.LogWarning(s);
    }

    public static void LoggaFel(string s) {
        if (Instance.doDebug) Debug.LogError(s);
    }

    public static void LoggaLive(string s) {

        if (!Instance.stringOfLogs.Contains(s)) {
            if (Instance.stringOfLogs.Count >= 5)
                Instance.stringOfLogs.Clear();
            
            Instance.stringOfLogs.Insert(0,s);
        }
            

        
    }

    public static List<string> getLogs() {
        return Instance.stringOfLogs;
    }

    public static void LoggaUndantag(System.Exception s) { }


}

using UnityEngine;
using System.Collections;

public class DebugLog {

    public bool doDebug = false;

    public static DebugLog DL = new DebugLog();

    public DebugLog() {
        
    }

    public static void Debugga(string s) {
        if(DL.doDebug) Debug.Log(s);
    }

    public static void DebuggaWarning(string s) {
        if (DL.doDebug) Debug.Log(s);
    }

    public static void DebuggaError(string s) {
        if (DL.doDebug) Debug.Log(s);
    }

    public static void DebuggaException(System.Exception s) { }


}

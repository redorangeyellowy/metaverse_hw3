using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Python.Runtime;

using UnityEngine;

public class GesticulatorDemo : MonoBehaviour
{
    void Start()
    {
        Main();  
    }

    void Update()
    {
      
    }

    public static void AddEnvPath(params string[] paths)
        {
            var envPaths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).ToList();
            envPaths.InsertRange(  0,   paths.Where( x => x.Length > 0 && !envPaths.Contains(x) ).ToArray()  );
            Environment.SetEnvironmentVariable("PATH", string.Join(Path.PathSeparator.ToString(), envPaths), EnvironmentVariableTarget.Process);
        }

    static void Main()

        {
            Runtime.PythonDLL = @"C:\Users\VDSL\anaconda3\envs\gesti\python37.dll";
            var PYTHON_HOME = Environment.ExpandEnvironmentVariables(@"C:\Users\VDSL\anaconda3\envs\gesti");
            AddEnvPath(PYTHON_HOME, Path.Combine(PYTHON_HOME, @"Library\bin")); 
            PythonEngine.PythonHome = PYTHON_HOME;
            PythonEngine.PythonPath = string.Join
            (
                Path.PathSeparator.ToString(),
                new string[]
                {
                    PythonEngine.PythonPath,
                    Path.Combine(PYTHON_HOME, @"Lib\site-packages"),
                    @"C:\Users\VDSL\Desktop\github\metaverse_hw3\Assets"
                    
                }
            );
            PythonEngine.Initialize();
            using (Py.GIL())
            {
                dynamic demo = Py.Import("demo.demo");
            }
            PythonEngine.Shutdown();
        }    
}

using UnityEngine;

using System.Collections.Generic;
using System.Reflection;
using System;

namespace Common.Utils {

	public sealed class DevelopmentConsole : MonoBehaviour {
#if DEVELOPMENT

        private sealed class ConsoleCommand
        {
            public sealed class Parameter
            {
                public string parameterName;
                public Type parameterType;
            }
            public MethodInfo mi;
            public string methodName;
            public List<Parameter> parameters;
        }

        private List<ConsoleCommand> commandList = new List<ConsoleCommand>();
        private string lastCommand = string.Empty;

        private bool consoleEnabled = false;
        private float timeScale;
        private string command = string.Empty;
        private string textArea = string.Empty;
        private Vector2 scrollPosition = Vector2.zero;
        [SerializeField]
        private const int width = 450;
        [SerializeField]
        private const int height = 201;
        private Rect windowRect;

        [SerializeField]
        private KeyCode keyToOpenConsole = KeyCode.Backslash;

		// Use this for initialization
		private void Start () {
            windowRect = new Rect(5, 5, width, height);


            System.Type ourType = typeof(ProjectName.Console.ConsoleCommands);

            MethodInfo[] mi = ourType.GetMethods(BindingFlags.Static | BindingFlags.Public);
            ConsoleCommand cc;
            for (int i = 0; i < mi.Length; ++i)
            {
                cc = new ConsoleCommand();
                cc.methodName = mi[i].Name;
                cc.mi = mi[i];
                cc.parameters = new List<ConsoleCommand.Parameter>();
                //Debug.Log(mi[i].Name);
                ParameterInfo [] pi = mi[i].GetParameters();
                for (int j = 0; j < pi.Length; ++j )
                {
                    ConsoleCommand.Parameter parameter = new ConsoleCommand.Parameter();
                    parameter.parameterName = pi[j].Name;
                    parameter.parameterType = pi[j].ParameterType;
                    //parameter.parameterType = type.Substring(type.IndexOf(".")+1);
                    cc.parameters.Add(parameter);
                    //Debug.Log(parameter.parameterType);
                    //Debug.Log(pi[j].DefaultValue);
                    //Debug.Log(parameter.parameterName);
                }
                commandList.Add(cc);
            }
		}
		
		// Update is called once per frame
		private void Update () {
            if (Input.GetKeyDown(keyToOpenConsole))
            {
                consoleEnabled = !consoleEnabled;
                if (consoleEnabled == true)
                {
                    timeScale = Time.timeScale;
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = timeScale;
                }
            }
		}

        private void ExecuteCommand()
        {
            if (command.Equals("º"))
            {
                consoleEnabled = false;
            }
            else
            {
                textArea += "\n" + command;
                bool find = false;
                string [] commandAndParameters = command.Split();
                for (int i = 0; i < commandList.Count && !find; ++i)
                {
                    if (commandList[i].methodName.Equals(commandAndParameters[0]))
                    {
                        find = true;
                        int parameteres = commandAndParameters.Length - 1;
                        if (parameteres > 0)
                        {
                            object[] parameters = new object[parameteres];
                            for (int j = 0; j < parameteres; ++j)
                            {
                                parameters[j] = ConvertTo(commandList[i].parameters[j].parameterType, commandAndParameters[j + 1]);
                                commandList[i].mi.Invoke(this, parameters);
                            }
                        }
                        else
                        {
                            commandList[i].mi.Invoke(this, new object[] { });
                        }
                        
                    }
                }
                if (find)
                {
                    lastCommand = command;
                }
                else
                {
                    textArea += " Command not found";
                }
            }
            command = string.Empty;
        }

        private object ConvertTo(Type type, string parameter)
        {
            object result = null;
            if (type == typeof(bool))
            {
                result = Convert.ToBoolean(parameter);
            }
            else if (type == typeof(float))
            {
                result = (float)Convert.ToDouble(parameter);
            }
            return result;
        }

        private void OnGUI()
        {
            if (consoleEnabled == true)
            {
                windowRect = GUI.Window(0, windowRect, DrawConsole, "DevelopmentConsole");
                GUI.FocusWindow(0);
            }
        }

        private void DrawConsole(int windowID)
        {
            DrawTextField();
            DrawTextArea();
            DrawMacros();
        }

        private void DrawTextField()
        {
            GUI.SetNextControlName("TextField");
            command = GUI.TextField(new Rect(6, height-25, width - 20, 20), command);

            if (Event.current.isKey)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.Return:
                    case KeyCode.KeypadEnter:
                        ExecuteCommand();
                        break;
                    case KeyCode.UpArrow:
                        command = lastCommand;
                        break;
                }
            }
            GUI.FocusControl("TextField");
        }

        private void DrawTextArea()
        {
            scrollPosition = GUI.BeginScrollView(new Rect(6, 45, width - 10, height - 75), scrollPosition, new Rect(0, 0, width - 28, height - 50));
            GUI.TextArea(new Rect(0, 0, width -28, height - 50), textArea);
            GUI.EndScrollView();
        }

        private void DrawMacros()
        {
            if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
                print("Got a click");
        }
#endif
	}
}
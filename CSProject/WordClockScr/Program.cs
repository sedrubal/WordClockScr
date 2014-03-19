﻿using System;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace WordClockScr
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            if (args.Length > 0)
            {
                // Get the command line arguments

                string firstArgument = args[0].ToLower().Trim();
                string secondArgument = null;

                if (firstArgument.Length > 2)
                {
                    secondArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else if (args.Length > 1)
                    secondArgument = args[1];

                // analize command line arguments
                switch (firstArgument)
                {
                    case "/c":
                        // Show the options dialog
                        ShowOptions();
                        break;
                    case "/p":
                        // Preview
                        ShowPreview(secondArgument);
                        break;
                    case "/s":
                        // Show screensaver form
                        //ShowOptions();
                        ShowScreenSaver();
                        break;
                    case "/d":
                        // Show screensver in debug mode
                        ShowProgram();
                        break;
                    default:
                        MessageBox.Show("Invalid command line argument: " + firstArgument, "Invalid Command Line Argument", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                // If no arguments were passed in, show the program
                ShowProgram();
            }
        }        

        #region different start options

        static void ShowOptions()
        {
            optionsForm = new SettingsDialog(new WordClockScr.Elements.SettingsMachine(), true);
            Application.Run(optionsForm);
        }

        static void ShowScreenSaver()
        {
            screenSaver = new OutPutWindow(true);
            Application.Run(screenSaver);
        }

        static void ShowProgram()
        {
            screenSaver = new OutPutWindow(false);
            Application.Run(screenSaver);
        }

        static void ShowPreview(string secondArgument)
        {
            if (secondArgument == null)
            {
                MessageBox.Show("Sorry, but the expected window handle was not provided.",
                    "ScreenSaver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            IntPtr previewWndHandle = new IntPtr(long.Parse(secondArgument));
            screenSaver = new OutPutWindow(true, previewWndHandle);
            Application.Run(screenSaver);
        }

        #endregion different start options

        #region Windows

        public static OutPutWindow screenSaver;

        public static SettingsDialog optionsForm;

        #endregion Windows
    }
}
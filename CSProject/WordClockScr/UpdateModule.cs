using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace WordClockScr
{
    /// <summary>
    /// functions to check for updates
    /// </summary>
    static class UpdateModule
    {
        #region "Urls"
        /// <summary>
        /// returns the url to the version number of the newest program
        /// </summary>
        private static string VersionURL
        {
            get
            {
                return "http://phoneserver.no-ip.biz/software/updatecenter/versions/" + Assembly.GetExecutingAssembly().GetName().Name + ".txt?ssl=off";
            }
        }

        /// <summary>
        /// returns the URL to the newest version of this program
        /// </summary>
        private static string UpdateURL
        {
            get
            {
                return "http://phoneserver.no-ip.biz/software/updatecenter/downloads/" + Assembly.GetExecutingAssembly().GetName().Name + ".php?ssl=off";
            }
        }
        #endregion "Urls"

        /// <summary>
        /// Starts the check-procedure
        /// </summary>
        public static void CheckForUpdates()
        {
            Version NewestVersion = UpdateModule.GetNewestVersion();
            if (NewestVersion != null)
            {
                Version CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                short newerVersion = UpdateModule.NewerVersion(CurrentVersion, NewestVersion);
                if(newerVersion == 2)
                {
                    DialogResult res = MessageBox.Show("Es ist eine neuere Version des Programms verfügbar. Möchten Sie zur Downloadseite wechseln?" + Environment.NewLine + "Aktuell: " + CurrentVersion.ToString() + ", Neu: " + NewestVersion.ToString(), 
                        "Update verfügbar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (res == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(UpdateModule.UpdateURL);
                    }
                }
                else if(newerVersion == 1)
                    MessageBox.Show("Sie benutzen eine neuere Version als die offizielle aktuelle Version." + Environment.NewLine + "Aktuell: " + CurrentVersion.ToString() + ", Offiziell: " + NewestVersion.ToString(), 
                        "Kein Update verfügbar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if(newerVersion == 0)
                    MessageBox.Show("Herzlichen Glückwunsch! Diese Version ist die aktuelle." + Environment.NewLine + "Aktuell: " + CurrentVersion.ToString(), "Programm aktuell", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region "Tools"
        /// <summary>
        /// Gets the number of the newest version from the server
        /// </summary>
        /// <returns>Version</returns>
        private static Version GetNewestVersion()
        {
            string NVersion = "";
            try
            {
                WebClient Webclient1 = new WebClient();
                NVersion = Webclient1.DownloadString(VersionURL).Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim überprüfen der Version. Testen Sie die Internetverbindung und probieren Sie es erneut. Fehler-Details:" + Environment.NewLine + ex.Message, 
                    "Update Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                return new Version(NVersion);
            }
            catch (System.ArgumentException ex)
            {
                MessageBox.Show("Fehler beim überprüfen der Version. Ungültige Antwort vom Server. Fehler-Details:" + Environment.NewLine + ex.Message,
                    "Update Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Checks, which version is the bigger one
        /// </summary>
        /// <param name="v1">Version 1 to comare</param>
        /// <param name="v2">Version 2 to compare</param>
        /// <returns>1, 2, or 0 (both are the same)</returns>
        private static short NewerVersion(Version v1, Version v2)
        {
            int result = v1.CompareTo(v2);
            if (result > 0)
                return 1;
            else if (result < 0)
                return 2;
            else
                return 0;
        }
        #endregion "Tools"
    }
}
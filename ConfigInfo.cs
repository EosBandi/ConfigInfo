// Mission Planner plugin for checking config file name and add description for the given config
// Don't do anything if the config filename is config.xml (default configuration)

using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace MissionPlanner.ConfigInfo
{
    public class ConfigInfoPlugin : MissionPlanner.Plugin.Plugin
    {
        private string MPConfigDescription;

        public override string Name
        {
            get { return "ConfigInfo"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override string Author
        {
            get { return "EOSBandi"; }
        }

        //[DebuggerHidden]
        public override bool Init()
        {
            loopratehz = 0; //we do not use the loop
            return true;
        }

        public override bool Loaded()
        {
            //Check if we have other config than config.xml
            if (String.Compare(Settings.FileName, "config.xml", true) != 0)
            {
                //Hook kwyboard command for change description
                MainV2.instance.ProcessCmdKeyCallback += this.Instance_ProcessCmdKeyCallback;

                MPConfigDescription = Host.config["MPConfigDesc", ""];
                if (MPConfigDescription.Length == 0)    //opps no description yet
                    GetDescription();
                UpdateTitleBar();
            }

            return true;
        }

        public override bool Loop()
        {
            return true;
        }

        public override bool Exit()
        {
            return true;
        }

        private void GetDescription()
        {
            var result = InputBox.Show("Description for Config file : " + Settings.FileName, "Enter description", ref MPConfigDescription);
            if (result != DialogResult.Cancel)
            {
                Host.config["MPConfigDesc"] = MPConfigDescription;
            }
        }

        private void UpdateTitleBar()
        {
            string strVersion = File.Exists("version.txt")
                                 ? File.ReadAllText("version.txt")
                                 : System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string MPversion = "Mission Planner " + Application.ProductVersion + " build " + strVersion;
            if (MPConfigDescription.Length > 0)
                MainV2.titlebar = MPversion + " (" + MPConfigDescription + ") ";
            else
                MainV2.titlebar = MPversion;

            MainV2.instance.Text = MainV2.titlebar + Host.comPort.MAV?.VersionString; //Do not add FW verion to titlebar, unless it duplicated at reconnect
        }

        private bool Instance_ProcessCmdKeyCallback(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            //Add our shortcut

            if (keyData == (Keys.Alt | Keys.D))
            {
                GetDescription();
                UpdateTitleBar();
            }
            return true;
        }
    }
}
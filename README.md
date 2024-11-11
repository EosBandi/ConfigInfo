# ConfigInfo
Mission Planner can be started with a command line option to specify a configuration file

 > <code> MissionPlanner.exe -config filename.xml </code>

Where the filename must ends with .XML
If the file does not exists Mission Planner will create with the default settings.

This plugin adds an entry to the actual config file with a description (for example vehicle name) and displays it at the top of the main Mission Planner window. At the first run it asks the user for the description.
The description can be changes by pressing ALT+D 

# Installation:

Copy the ConfigInfo.cs to the plugins folder in the Mission Planner installation folder and restart Mission Planner

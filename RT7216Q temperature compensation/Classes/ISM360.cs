using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

namespace RT7216Q_temperature_compensation.Classes
{
    public class ISM360
    {

        public Dictionary<string, string> TriggerContent;
        public Dictionary<string, string> DataOutContent;

        public Dictionary<string, string> lastWriteTime;

        public string TriggerPath = @"C:\Isuzu Optics\ISM-360\data\Trigger.ini";
        public string DataOutPath = @"C:\Isuzu Optics\ISM-360\data\DataOut.ini";

        public FileSystemWatcher watcher;
        public Status state;
        public bool isConnected = false;

        public ISM360()
        {
            TriggerContent = new Dictionary<string, string>();
            DataOutContent = new Dictionary<string, string>();

            lastWriteTime = new Dictionary<string, string>();
            state = Status.Idle;

           watch(@"C:\Isuzu Optics\ISM-360\data");
        }

        public enum Status
        {
            Idle = 0,
            Measuring = 1
        }

        public void watch(string DataOut)
        {
            watcher = new FileSystemWatcher();
            DataOutPath = Path.Combine(DataOut, "DataOut.ini");
            if (!File.Exists(DataOutPath)) { MessageBox.Show("File does not exist!"); return; }
           

            lastWriteTime.Add(DataOutPath, File.GetLastWriteTime(DataOutPath).ToString("hh.mm.ss.fff"));

            watcher.Path = DataOut;
            watcher.NotifyFilter = //NotifyFilters.Attributes
                                  NotifyFilters.CreationTime
                //| NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                //| NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                //| NotifyFilters.Security
                //| NotifyFilters.Size
                                 ;

            watcher.Changed += HandleChanged;
            //watcher.Created += OnCreated;
            //watcher.Deleted += OnDeleted;
            //watcher.Renamed += OnRenamed;
            //watcher.Error += OnError;

            watcher.Filter = "DataOut.ini";
            watcher.EnableRaisingEvents = true;

            isConnected = true;
        }

        private void HandleChanged(object sender, FileSystemEventArgs e)
        {
            string newWriteTime = File.GetLastWriteTime(DataOutPath).ToString("hh.mm.ss.fff");
            if (newWriteTime != lastWriteTime[DataOutPath]) { lastWriteTime[DataOutPath] = newWriteTime; return; }

            lastWriteTime[DataOutPath] = newWriteTime;
            
            parseData();
            state = Status.Idle;
        }

        public void parseData()
        {
            var fs = new FileStream(DataOutPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default))
            {
                string line;
                while (sr.Peek() >= 0)
                {
                    line = sr.ReadLine();
                    //throw away title and empty lines
                    //if ( line == "[Data]" || line == string.Empty) { continue; }

                    string[] words = line.Split('=');
                    if (words.Length != 2) { continue; }

                    string key = words[0].Trim(' ');
                    string value = words[1].Trim(' ');

                    if(!DataOutContent.ContainsKey(key)){ DataOutContent.Add(key,value);}
                    else{DataOutContent[key] = value;}
                }
                sr.Close();
            }
            fs.Close();
        }

        public void triggerMeasure()
        {
            if (!File.Exists(TriggerPath)) { MessageBox.Show("Trigger file does not exist!"); return; }

            if (state != Status.Idle) {  return; }
            state = Status.Measuring;

            FileStream fs;
            try
            {
                fs = new FileStream(TriggerPath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            }
            catch (Exception err)
            {
                switch (err.GetType().ToString())
                {
                    case "System.UnauthorizedAccessException": MessageBox.Show("Unauthorized access to port");   return;
                    case "System.InvalidOperationException": MessageBox.Show("InvalidOperationException");       return;
                    case "System.IO.FileNotFoundException": MessageBox.Show(TriggerPath + " does not exist!");   return;
                    default: MessageBox.Show("Some error has occurred when trying to trigger spectromeasure");   return;
                }
            }

            StreamWriter writer = new StreamWriter(fs, Encoding.Default);
            writer.Write("[Measure]\r\n" +
                         "Trig = True\r\n" +
                         "Delete = FALSE\r\n" +
                         "Measure = FALSE    ");
            writer.Close();
        }


    }
}

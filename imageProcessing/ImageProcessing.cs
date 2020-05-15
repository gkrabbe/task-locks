using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imageProcessing
{
    public class ImageProcessing
    {
        readonly Dictionary<string, Task<string>> Processes;

        public ImageProcessing()
        {
            Processes = new Dictionary<string, Task<string>>();
        }
         
        public string Start(string name)
        {
            var uuid = Guid.NewGuid().ToString();
            var task = Process(name);
            Processes.Add(uuid, task);
            return uuid;
        }

        public IEnumerable<string> List()
        {
            return Processes.Keys.ToArray();
        }

        public string Status(string uuid)
        {
            if (Processes.TryGetValue(uuid, out Task<string> process))
                return process.Status.ToString();
            return "Not Exist";
        }

        public string Result(string uuid)
        {
            if (Processes.TryGetValue(uuid, out Task<string> process) && process.IsCompleted)
                return process.Result;            
            return null;
        }

        private async Task<string> Process(string name)
        {
            await Task.Delay(10000);
            var uuid = Guid.NewGuid().ToString();
            return $"{uuid}-{name}";

        }  


    }
}

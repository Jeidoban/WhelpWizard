using System;
using PCLStorage;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;

namespace WhelpWizard
{

    //This class took me way to long to figure out. The file saving system I used (PCLStorang)
	//uses asynchronous programming which super threw me off guard. Putting await in front of 
    //a method apparently suspends the method until that line of code is complete. However this
    //really messed me up as the view was not loading, and the debugger was getting stuck on
    //the lines that are prefixed with "await". THIS CAN BE FIXED BY APPENDING RESULT TO THE END
    //OF THE STATMENT. Also revome the await.
	public class SaveAndLoad
    {
        private int fileNumber;

		public SaveAndLoad()
        {
            fileNumber = 1;
        }

        public async Task WriteToFile(Dog dog)
        {
            string json = JsonConvert.SerializeObject(dog); // Convert Dog C# object to json.
            IFolder rootFolder = FileSystem.Current.LocalStorage; // get device storage.
            IFile file = rootFolder.CreateFileAsync("dog" + fileNumber, CreationCollisionOption.ReplaceExisting).Result; // create folder.
            await file.WriteAllTextAsync(json); // write json to file.
            fileNumber++;
        }

        public async Task<ObservableCollection<Dog>> LoadFromfile(ObservableCollection<Dog> dog)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;// Get device storage.
            while (rootFolder.CheckExistsAsync("dog" + fileNumber).Result == ExistenceCheckResult.FileExists) // while the file exists...
			{
                IFile file =  rootFolder.GetFileAsync("dog" + fileNumber).Result; // get the file.
                string json = file.ReadAllTextAsync().Result; //read contents...
				Dog thing = JsonConvert.DeserializeObject<Dog>(json); // Parse to Dog C# object.
				dog.Add(thing); // add to list.
                fileNumber++;
			}
            return dog;
        }
    }
}

using System;
using PCLStorage;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;

namespace WhelpWizard
{

    //This class took me way to long to figure out. The file saving system I used (PCLStorage)
	//uses asynchronous programming which super threw me off guard. Putting await in front of 
    //a method apparently suspends the method until that line of code is complete. However this
    //really messed me up as the view was not loading, and the debugger was getting stuck on
    //the lines that are prefixed with "await". THIS CAN BE FIXED BY APPENDING RESULT TO THE END
    //OF THE STATMENT. Also remove the await.
	public class SaveAndLoad
    {
        public static int fileNumber = 0;
        public static int notificationId = 0;

        public static async Task WriteToFile(Dog dog)
        {
            string json = JsonConvert.SerializeObject(dog); // Convert Dog C# object to json.
            IFolder rootFolder = FileSystem.Current.LocalStorage; // get device storage.
            IFile file = rootFolder.CreateFileAsync("dog" + fileNumber, CreationCollisionOption.ReplaceExisting).Result; // create folder.
            await file.WriteAllTextAsync(json); // write json to file.
            fileNumber++;
        }

		public static async Task OverwriteFile(Dog dog)
		{
			string json = JsonConvert.SerializeObject(dog); // Convert Dog C# object to json.
			IFolder rootFolder = FileSystem.Current.LocalStorage; // get device storage.
            IFile file = rootFolder.CreateFileAsync("dog" + dog.PlaceInList, CreationCollisionOption.ReplaceExisting).Result; // create folder.
			await file.WriteAllTextAsync(json); // write json to file.
		}

        public static async Task<ObservableCollection<Dog>> LoadFromfile(ObservableCollection<Dog> dog)
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

        // Ok this was pretty fun to brainstorm. This function will delete a dog from the list.
        // I'll go through it step by step. 
        public static async Task DeleteCell(ObservableCollection<Dog> dog, int index) // This passes in the dog list as well as the index of the cell being deleted.
        {                                                                             // The index is determined where this method is called from.
            Dog currentDog; // Create dog object.
			IFolder rootFolder = FileSystem.Current.LocalStorage;// Get device storage.
            IFile file = rootFolder.GetFileAsync("dog" + index).Result; // Gets the file that is getting deleted (whatever the index was that was passed in).
            dog.RemoveAt(index);// Remove the dog from the list.
            await file.DeleteAsync(); // Remove the file saved associated with the dog in the device storage.
            fileNumber--; // decrement the file number.
            index++; // increment the index.
            while (rootFolder.CheckExistsAsync("dog" + index).Result == ExistenceCheckResult.FileExists) // while the file exists...
			{
                file = rootFolder.GetFileAsync("dog" + index).Result; // update file to reflect the new index.
                currentDog = dog[index - 1]; // get the dog object in list. It says minus one because observable automatically updates the index when an item is removed.
                currentDog.PlaceInList = index - 1; // Update the PlaceInList variable in the dog class to the index - 1 (essentially subtracting the variable in the dog file by one).
                await file.WriteAllTextAsync(JsonConvert.SerializeObject(currentDog)); // Rewrite the file.
                await file.RenameAsync("dog" + (index - 1)); // rename the file with the new index.
                index++; // increment the index.
			}
        }

        public static void SaveNotificationId()
        {
            notificationId += 6;
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFile file = rootFolder.CreateFileAsync("notificationIdCounter", CreationCollisionOption.ReplaceExisting).Result;
            file.WriteAllTextAsync(notificationId.ToString());
        }

        public static void LoadNotificationId()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            if (rootFolder.CheckExistsAsync("notificationIdCounter").Result == ExistenceCheckResult.FileExists) // while the file exists...
			{
				IFile file = rootFolder.GetFileAsync("notificationIdCounter").Result; // get the file.
                notificationId = Int32.Parse(file.ReadAllTextAsync().Result);
			}
        }
    }
}

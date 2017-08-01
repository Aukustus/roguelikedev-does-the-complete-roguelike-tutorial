using RogueTutorial;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpRogueTutorial
{
    class File
    {
        public static World LoadGame()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream worldStream = new FileStream("World.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            World loadedWorld = (World)formatter.Deserialize(worldStream);
            worldStream.Close();

            return loadedWorld;
        }

        public static void SaveGame()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream worldStream = new FileStream("World.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(worldStream, Rogue.GameWorld);
            worldStream.Close();
        }
    }
}

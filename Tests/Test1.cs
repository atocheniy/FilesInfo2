using FilesInfoNamespace;

namespace Tests
{
    [TestClass]
    public sealed class Test1
    {
        FilesInfo fi = null;

        string DirectoryPath;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // This method is called once for the test assembly, before any tests are run.
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // This method is called once for the test assembly, after all tests are run.
        }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // This method is called once for the test class, before any tests of the class are run.
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // This method is called once for the test class, after all tests of the class are run.
        }

        [TestInitialize]
        public void TestInit()
        {
            // This method is called before each test method.

            DirectoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(DirectoryPath);

            fi = new FilesInfo();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // This method is called after each test method.

            if (Directory.Exists(DirectoryPath)) Directory.Delete(DirectoryPath, true);
        }



        [TestMethod]
        public void TestMethod1()
        {
            File.WriteAllText(Path.Combine(DirectoryPath, "a.txt"), ""); 
            File.WriteAllText(Path.Combine(DirectoryPath, "a2.txt"), "");
            File.WriteAllText(Path.Combine(DirectoryPath, "a3.txt"), "");

            File.WriteAllText(Path.Combine(DirectoryPath, "b.img"), ""); 
            File.WriteAllText(Path.Combine(DirectoryPath, "b2.img"), "");
            File.WriteAllText(Path.Combine(DirectoryPath, "b3.img"), ""); 

            fi.Load(DirectoryPath);
            fi.Save("save.txt");

            Assert.AreEqual(3, fi.info[".txt"]);
            Assert.AreEqual(3, fi.info[".img"]);
        }

        [TestMethod]
        public void TestMethod2()
        {
            fi.Load(DirectoryPath);
            Assert.AreEqual(0, fi.info.Count);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string subFolderPath = Path.Combine(DirectoryPath, "SubFolder");
            Directory.CreateDirectory(subFolderPath);

            File.WriteAllText(Path.Combine(DirectoryPath, "root.txt"), "");
            File.WriteAllText(Path.Combine(subFolderPath, "sub.txt"), "");
            File.WriteAllText(Path.Combine(subFolderPath, "another.log"), "");

            fi.Load(DirectoryPath);

            Assert.AreEqual(2, fi.info.Count);
            Assert.AreEqual(2, fi.info[".txt"]);
            Assert.AreEqual(1, fi.info[".log"]);
        }

        [TestMethod]
        public void TestMethod4()
        {
            string nonExistentPath = "Q:\\NonExistentFolder12345";
            Assert.Throws<DirectoryNotFoundException>(() => fi.Load(nonExistentPath));
        }

        [TestMethod]
        public void TestMethod5()
        {
            string saveFilePath = Path.Combine(DirectoryPath, "save.txt");

            fi.info.Add(".txt", 2);
            fi.info.Add(".dll", 5);
            fi.info.Add(".jpg", 3);

            string expectedContent = ".dll: 5 \n.jpg: 3 \n.txt: 2 \n";

            fi.Save(saveFilePath);

            string actualContent = File.ReadAllText(saveFilePath);
            Assert.AreEqual(expectedContent, actualContent);
        }
    }
}

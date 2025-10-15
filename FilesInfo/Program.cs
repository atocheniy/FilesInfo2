namespace FilesInfoNamespace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FilesInfo fi = new FilesInfo();
            fi.Load("F:\\Projects");
            fi.Save("info.txt");
        }
    }
}

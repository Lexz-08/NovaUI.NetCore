namespace NovaUI.NetCore.Events
{
    public class FolderSelectedEventArgs(string FolderPath) : EventArgs
    {
        private readonly string fullPath = FolderPath;
        private readonly string parentFolder = Directory.GetParent(FolderPath)!.Name;
        private readonly string folderName = new DirectoryInfo(FolderPath).Name;

        public string FullPath => fullPath;

        public string ParentFolder => parentFolder;

        public string FolderName => folderName;
    }
}

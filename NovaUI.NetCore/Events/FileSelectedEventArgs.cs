namespace NovaUI.NetCore.Events
{
    public class FileSelectedEventArgs(string FileName) : EventArgs
    {
        private readonly string fullPath = FileName;
        private readonly string fileName = Path.GetFileName(FileName);
        private readonly string fileExt = Path.GetExtension(FileName);

        public string FullPath => fullPath;

        public string FileName => fileName;

        public string FileExtension => fileExt;
    }
}

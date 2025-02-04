namespace WaveProxy.src.FileHandling {

    public enum DirectoryName {
        Data,
        Config
    }

    internal class PathHandler {
        public string InputFilePath { get; private set; }
        public string OutputFilePath { get; private set; }
        private string _currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public PathHandler(DirectoryName directory, string inputFileName) {
            InputFilePath = GetPath(directory, inputFileName);
        }

        public PathHandler(DirectoryName directory, string inputFileName, string outputFileName) {
            InputFilePath = GetPath(directory, inputFileName);
            OutputFilePath = GetPath(directory, outputFileName);
        }

        public string GetPath(DirectoryName directory, string fileName) {
            string folderName = directory switch {
                DirectoryName.Data => "data",
                DirectoryName.Config => "config",
                _ => throw new ArgumentException("Invalid directory name")
            };

            string fullPath = Path.Combine(_currentDirectory, folderName, fileName);

            ValidatePath(fullPath);

            return Path.Combine(_currentDirectory, folderName, fileName);
        }

        private void ValidatePath(string path) {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Invalid Path input");
            if (!Path.Exists(path))
                throw new DirectoryNotFoundException("Path does not exist!");
        }
    }
}
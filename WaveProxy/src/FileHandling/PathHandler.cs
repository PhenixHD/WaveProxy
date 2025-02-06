namespace WaveProxy.src.FileHandling {

    public enum DirectoryName {
        Data,
        Config
    }

    public enum FileName {
        appsettings,
        scraped_proxies,
        urls,
        output_https,
        output_socks4_5
    }

    internal class PathHandler {
        public string InputFilePath { get; private set; }
        public string OutputFilePath { get; private set; } = string.Empty;
        private readonly string _currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public PathHandler(DirectoryName inputDirectory, FileName inputFileName) {
            string inputFolder = GetFolderName(inputDirectory);
            ValidateDirectoryExists(inputFolder);

            InputFilePath = GetPath(inputDirectory, inputFileName);
        }

        public PathHandler(DirectoryName inputDirectory, FileName inputFileName, DirectoryName outputDirectory, FileName outputFileName) {
            string inputFolder = GetFolderName(inputDirectory);
            string outputFolder = GetFolderName(outputDirectory);

            ValidateDirectoryExists(inputFolder);
            ValidateDirectoryExists(outputFolder);

            InputFilePath = GetPath(inputDirectory, inputFileName);
            OutputFilePath = GetPath(outputDirectory, outputFileName);
        }

        private string GetFolderName(DirectoryName directory) {
            return directory switch {
                DirectoryName.Data => Path.Combine(_currentDirectory, "data"),
                DirectoryName.Config => Path.Combine(_currentDirectory, "config"),
                _ => throw new ArgumentException("Invalid directory name")
            };
        }

        public string GetPath(DirectoryName directory, FileName filename) {

            string directoryPath = GetFolderName(directory);

            string fileName = filename switch {
                FileName.appsettings => "appsettings.json",
                FileName.scraped_proxies => "proxies.txt",
                FileName.urls => "urls.txt",
                FileName.output_https => "https.txt",
                FileName.output_socks4_5 => "socks4_5.txt",
                _ => throw new ArgumentException("Invalid file name")

            };

            return Path.Combine(directoryPath, fileName);
        }

        private void ValidateDirectoryExists(string path) {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Directory not found: {path}");
        }

        private void ValidateFileExists(string path) {
            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");
        }
    }
}
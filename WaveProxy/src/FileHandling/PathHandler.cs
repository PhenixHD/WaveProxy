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
        public string OutputFilePath { get; private set; }
        private string _currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public PathHandler(DirectoryName inputDirectory, FileName inputFileName) {
            InputFilePath = GetPath(inputDirectory, inputFileName);
            ValidateFileExists(InputFilePath);
        }

        public PathHandler(DirectoryName inputDirectory, FileName inputFileName, DirectoryName outputDirectory, FileName outputFileName) {
            InputFilePath = GetPath(inputDirectory, inputFileName);
            ValidateFileExists(InputFilePath);

            OutputFilePath = GetPath(outputDirectory, outputFileName);
            ValidateFileExists(OutputFilePath);
        }

        public string GetPath(DirectoryName directory, FileName filename) {
            string folderName = directory switch {
                DirectoryName.Data => "data",
                DirectoryName.Config => "config",
                _ => throw new ArgumentException("Invalid directory name")
            };

            string fileName = filename switch {
                FileName.appsettings => "appsettings.json",
                FileName.scraped_proxies => "proxies.txt",
                FileName.urls => "urls.txt",
                FileName.output_https => "https.txt",
                FileName.output_socks4_5 => "socks4_5.txt",
                _ => throw new ArgumentException("Invalid file name")

            };

            string directoryPath = Path.Combine(_currentDirectory, folderName);
            ValidateDirectoryExists(directoryPath);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ShapeExtractor
{
    public class clsZipFileInfo:IDisposable
    {
        private bool disposed = false;
        private FileInfo _ZipInformation = null;
        private ZipArchive _Archive;
        private Dictionary<string, clsFileInfo> _Contents;
        private bool _IsExtracted;
        private DirectoryInfo _ExtractionDirectory;

        public FileInfo ZipInformation
        {
            get
            {
                return _ZipInformation;
            }
        }

        public ZipArchive Archive
        {
            get
            {
                return _Archive;
            }
        }

        public Dictionary<string, clsFileInfo> Contents
        {
            get
            {
                return _Contents;
            }
        }


        public bool IsExtracted
        {
            get
            {
                return _IsExtracted; ;
            }
        }




        public DirectoryInfo ExtractionDirectory
        {
            get
            {
                return _ExtractionDirectory;
            }
        }



        public clsZipFileInfo(string ZipFilePath)
        {
            _ZipInformation = new FileInfo(ZipFilePath);
            _Archive = ZipFile.OpenRead(ZipFilePath);
            _Contents = new Dictionary<string, clsFileInfo>();
            foreach (ZipArchiveEntry oEntry in Archive.Entries)
            {
                clsFileInfo oFileInfo = new clsFileInfo(oEntry);
                oFileInfo.ExtractedFile = null;
                Contents.Add(oEntry.Name, oFileInfo);
            }
            _IsExtracted = false;
            _ExtractionDirectory = null;
        }

        public void Extract()
        {
            string strTempDir =  Path.GetFullPath(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
            Directory.CreateDirectory(strTempDir);
            _ExtractionDirectory = new DirectoryInfo(strTempDir);
            foreach (KeyValuePair<string, clsFileInfo> kvp in Contents)
            {
                string strDestinationPath = Path.GetFullPath(Path.Combine(ExtractionDirectory.FullName, kvp.Value.Entry.Name));

                kvp.Value.Entry.ExtractToFile(strDestinationPath);

                kvp.Value.ExtractedFile = new FileInfo(strDestinationPath);
                kvp.Value.IsExtracted = true;
            }

            _IsExtracted = true;
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //




            }

            // Free any unmanaged objects here.
            //
            if (_IsExtracted)
            {
                if (_ExtractionDirectory.Exists)
                {
                    _ExtractionDirectory.Delete(true);
                }
            }
            disposed = true;
        }

        ~clsZipFileInfo()
        {
            Dispose(false);
        }

    }





}

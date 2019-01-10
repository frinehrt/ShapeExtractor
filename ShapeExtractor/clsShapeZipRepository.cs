using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ShapeExtractor
{
    public class clsShapeRepository : IDisposable
    {
        private bool disposed = false;
        public string RespositoryDirectory;
        public Dictionary<string, clsZipFileInfo> Contents;


        public clsShapeRepository(string RepositoryDirectory)
        {
            DirectoryInfo oDIRoot = new DirectoryInfo(RepositoryDirectory);
            Contents = new Dictionary<string, clsZipFileInfo>();

            foreach (FileInfo oFI in oDIRoot.EnumerateFiles("*.zip"))
            {
                clsZipFileInfo oZipFileInfo = new clsZipFileInfo(oFI.FullName);
                Contents.Add(oFI.Name, oZipFileInfo);
            }
        }

        public clsZipFileInfo FindZipFile(string Filter)
        {
            clsZipFileInfo oZipFileInfo = Contents.FirstOrDefault(x => x.Key.ToUpper() == Filter).Value;
            return oZipFileInfo;
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

            foreach (KeyValuePair<string, clsZipFileInfo> kvp in Contents)
            {
                if (kvp.Value.IsExtracted)
                {
                    if (kvp.Value.ExtractionDirectory.Exists)
                    {
                        kvp.Value.ExtractionDirectory.Delete(true);
                    }
                }
            }
            disposed = true;
        }

        ~clsShapeRepository()
        {
            Dispose(false);
        }

    }
}

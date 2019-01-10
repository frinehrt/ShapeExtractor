using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

using System.Threading.Tasks;
using Shared;

namespace ShapeExtractor
{


    public class ShapeExtractor : IDisposable
    {
        private bool disposed = false;
        private clsShapeRepository _Repository = null;
        private clsZipFileInfo _ZipFileInfo = null;

        public clsShapeRepository Repository
        {
            get
            {
                return _Repository;
            }
        }

        public clsZipFileInfo ShapeZipFile
        {
            get
            {
                return _ZipFileInfo;
            }
        }

        public ShapeExtractor(string PathName)
        {
            if (File.Exists(PathName))
            {
                _ZipFileInfo = new clsZipFileInfo(PathName);
            }
            else
            {
                if (Directory.Exists(PathName))
                {
                    _Repository = new clsShapeRepository(PathName);
                }
                else
                {
                    return;
                }
            }
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

            if (_Repository != null)
            {
                _Repository.Dispose();
            }
            if (_ZipFileInfo != null)
            {
                _ZipFileInfo.Dispose(); 
            }
            disposed = true;
        }

        ~ShapeExtractor()
        {
            Dispose(false);
        }



    }


}

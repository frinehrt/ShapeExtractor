using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ShapeExtractor
{
    public class clsFileInfo
    {
        private ZipArchiveEntry _Entry;
        private bool _IsExtracted;
        private FileInfo _ExtractedFile;
        private bool _IsShapeFile;
        private bool _IsDBFFile;

        public ZipArchiveEntry Entry
        {
            get
            {
                return _Entry;
            }
        }

        public bool IsExtracted
        {
            get
            {
                return _IsExtracted;
            }
            set
            {
                _IsExtracted = value;
            }
        }

        public bool IsShapeFile
        {
            get
            {
                return _IsShapeFile;
            }
        }

        public bool IsDBFFile
        {
            get
            {
                return _IsDBFFile;
            }
        }

        public FileInfo ExtractedFile
        {
            get
            {
                return _ExtractedFile;
            }
            set
            {
                _ExtractedFile = value;
            }
        }



        public clsFileInfo(ZipArchiveEntry Entry)
        {
            this._Entry = Entry;
            this._IsExtracted = false;
            this._ExtractedFile = null;
            this._IsShapeFile = Path.GetExtension(this.Entry.FullName).ToLower() == ".shp";
            this._IsDBFFile = Path.GetExtension(this.Entry.FullName).ToLower() == ".dbf";
        }
    }
}

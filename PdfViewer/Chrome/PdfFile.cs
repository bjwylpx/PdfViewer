using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfViewer.Chrome
{
    /// <summary>
    /// PdfFile的抽象类，定义了要在子类中实现的各个接口
    /// </summary>
    internal abstract class PdfFile : IDisposable
    {
        /// <summary>
        /// 一个工厂方法，用于实例化各个具体的PdfFile实例
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static PdfFile Create(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (stream is MemoryStream)
                return new PdfMemoryStreamFile((MemoryStream)stream);
            else if (stream is FileStream)
                return new PdfFileStreamFile((FileStream)stream);
            else
                return new PdfBufferFile(StreamExtensions.ToByteArray(stream));
        }

        public abstract bool RenderPDFPageToDC(int pageNumber, IntPtr dc, int dpiX, int dpiY, int boundsOriginX, int boundsOriginY, int boundsWidth, int boundsHeight, bool fitToBounds, bool stretchToBounds, bool keepAspectRation, bool centerInBounds, bool autoRotate);

        public abstract bool GetPDFDocInfo(out int pageCount, out double maxPageWidth);

        public abstract void Save(Stream stream);

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}

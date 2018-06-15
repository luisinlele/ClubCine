using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProyectoFinal
{
    public class PDFGeneration
    {

        public static void createPdfFromImage(string imageFile, string pdfFile)
        {
            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER.Rotate(), 0, 0, 0, 0);
                PdfWriter.GetInstance(document, new FileStream(pdfFile, FileMode.Create));
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();
                FileStream fs = new FileStream(imageFile, FileMode.Open);
                var image = iTextSharp.text.Image.GetInstance(fs);
                image.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                document.Add(image);
                document.Close();
                //open pdf file
                Process.Start("explorer.exe", pdfFile);
            }

        }


        public static void SaveAsPng(RenderTargetBitmap src, string targetFile)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(src));
            using (var stm = System.IO.File.Create(targetFile))
            {
                encoder.Save(stm);
            }
        }


        public static RenderTargetBitmap GetImage(UIElement view)
        {
            Size size = new Size(view.RenderSize.Width, view.RenderSize.Height);
            if (size.IsEmpty)
                return null;
            RenderTargetBitmap result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            DrawingVisual drawingvisual = new DrawingVisual();

            using (DrawingContext context = drawingvisual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(view), null, new Rect(0, 0, (int)size.Width, (int)size.Height));
                context.Close();
            }
            result.Render(drawingvisual);
            return result;
        }

    }
}

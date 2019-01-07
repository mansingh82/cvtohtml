using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using System.IO;
using System.Xml.Linq;
using System.Drawing.Imaging;

namespace cvtohtml.Models {
    public class ParseService: IParseService
    {
        public bool parse(string fileLocation) {

            var fileName = Path.GetFileName(fileLocation);
            var fileExtension = Path.GetExtension(fileLocation);
            if (!(fileExtension == ".doc" || fileExtension == ".docx")) return false;

            var fileNameWithHTMLExtension = fileName.Replace(fileExtension, ".html");

            var outputPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "output");
            DirectoryInfo outputDirInfo = new DirectoryInfo(outputPath);
            if (!outputDirInfo.Exists)
                outputDirInfo.Create();

            byte[] byteArray = File.ReadAllBytes(fileLocaiton);

            using (var memoryStream = new MemoryStream()) {
                memoryStream.Write(byteArray, 0, byteArray.Length);
                using (var doc = WordprocessingDocument.Open(memoryStream, true)) {
                    int imageCounter = 0;
                    var settings = new HtmlConverterSettings() {
                        PageTitle = "My HTML",
                        ImageHandler = imageInfo => {
                            DirectoryInfo localDirInfo = new DirectoryInfo(Path.Combine(outputPath, "img"));
                            if (!localDirInfo.Exists)
                                localDirInfo.Create();
                            ++imageCounter;
                            string extension = imageInfo.ContentType.Split('/')[1].ToLower();
                            ImageFormat imageFormat = null;
                            if (extension == "png") {
                                extension = "gif";
                                imageFormat = ImageFormat.Gif;
                            } else if (extension == "gif")
                                imageFormat = ImageFormat.Gif;
                            else if (extension == "bmp")
                                imageFormat = ImageFormat.Bmp;
                            else if (extension == "jpeg")
                                imageFormat = ImageFormat.Jpeg;
                            else if (extension == "tiff") {
                                extension = "gif";
                                imageFormat = ImageFormat.Gif;
                            } else if (extension == "x-wmf") {
                                extension = "wmf";
                                imageFormat = ImageFormat.Wmf;
                            }
                            if (imageFormat == null)
                                return null;

                            string imageFilePath = "img/image" +
                                imageCounter.ToString() + "." + extension;
                            string imageFileName = Path.Combine(outputPath, imageFilePath);
                            try {
                                imageInfo.Bitmap.Save(imageFileName, imageFormat);
                            } catch (System.Runtime.InteropServices.ExternalException) {
                                return null;
                            }
                            var img = new XElement(Xhtml.img,
                                new XAttribute(NoNamespace.src, imageFilePath),
                                imageInfo.ImgStyleAttribute,
                                imageInfo.AltText != null ?
                                    new XAttribute(NoNamespace.alt, imageInfo.AltText) : null);
                            return img;
                        }
                    };
                    var html = HtmlConverter.ConvertToHtml(doc, settings);
                    File.WriteAllText(Path.Combine(outputPath, fileNameWithHTMLExtension), html.ToStringNewLineOnAttributes());
                    return true;
                };
            }
        }
    }

}

using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.StatusUpdater;
using IRL_Common_Library.Utils;
using Svg;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Xml;

namespace IRL_Bitmap_Converter_Tools.Converters
{
    public static class IconConverter
    {
        public static bool ConvertSvgToBitmaps(
            SvgFileInfo svgFileInfo, List<IconStyle> iconStyles, ConverterStatusUpdater statusUpdater, string outputFolder)
        {
            bool result = true;

            foreach (int styleId in svgFileInfo.ImageBitmapStyleIds)
            {
                IconStyle style = IconStyle.GetImageStyleById(styleId, iconStyles);

                if (style != null)
                {
                    string fileName = GetFileName(svgFileInfo.IconName, style);
                    string fileOutputFolder = CreateOutputFolder(outputFolder, style.Prefix);
                    string filePath = $"{fileOutputFolder}\\{fileName}.png";

                    statusUpdater.UpdateStatusAndInstructionsConverted($"Image: {fileName}", 1);

                    if (File.Exists(filePath))
                    {
                        Log.WriteLine($"File already exists {fileName} - filepath: {filePath}");
                        result = false;
                    }
                    else
                    {
                        try
                        {
                            XmlDocument xmlDoc = new();
                            xmlDoc.LoadXml(svgFileInfo.SvgString);

                            foreach (ImageSvgColorId svgColor in style.SvgColors)
                            {
                                SetAttributesColorWithId(xmlDoc, svgColor.Id, svgColor.Color);
                            }

                            SvgDocument svgDocument = SvgDocument.FromSvg<SvgDocument>(xmlDoc.InnerXml);
                            Bitmap bitmap = svgDocument.Draw();

                            SaveBitmapWithMargin(bitmap, style.Margin, style.BackColor, filePath);
                        }
                        catch (Exception e)
                        {
                            Log.WriteLine($"Error creating bitmap {fileName}, {e.Message}");

                            return false;
                        }
                    }
                }
                else
                {
                    Log.WriteLine($"Error creating bitmap {svgFileInfo.Filename}, style not found");

                    result = false;
                }
            }

            return result;
        }

        private static string CreateFileNameWithProperties(string fileName, List<BitmapProperty> properties)
        {
            string newFileName = fileName;

            bool firstPropertyAdded = false;
            foreach (BitmapProperty property in properties)
            {
                if (!string.IsNullOrEmpty(property.Name))
                {
                    if (firstPropertyAdded)
                    {
                        newFileName = $"{newFileName}_{property.Name}{property.Value}";
                    }
                    else
                    {
                        newFileName = $"{newFileName}@{property.Name}{property.Value}";
                        firstPropertyAdded = true;
                    }
                }
            }

            return newFileName;
        }

        public static void SetAttributesColorWithId(XmlDocument xmsDoc, string id, Color color)
        {
            string colorStr = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

            XmlNodeList nodes = xmsDoc.SelectNodes($"//*[@id='{id}']");

            foreach (XmlNode? node in nodes)
            {
                XmlAttribute element = node.Attributes["fill"];

                if (element == null)
                {
                    element = node.Attributes["stroke"];
                }

                element.Value = colorStr;
            }
        }

        public static void SetColorsBackground(XmlDocument xmlDoc, Color color)
        {
            string colorStr = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");


            XmlNodeList nodes = xmlDoc.SelectNodes("//*[name()='rect']");
            if (nodes == null || nodes.Count == 0)
            {
                return;
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                try
                {
                    if (nodes[i].Attributes["fill"] != null)
                    {
                        nodes[i].Attributes["fill"].Value = colorStr;
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                if (i == 1)
                {
                    break;
                }
            }
        }

        public static Color GetColorsBackground(XmlDocument xmlDoc)
        {
            Color color = Color.White;

            XmlNodeList nodes = xmlDoc.SelectNodes("//*[name()='rect']");
            foreach (XmlNode node in nodes)
            {
                string colorStr = node.Attributes["fill"].Value;
                color = ColorTranslator.FromHtml(colorStr);
            }

            return color;
        }

        private static void SaveBitmapWithMargin(Bitmap bitmap, BitmapMargin margin, Color backColor, string filePath)
        {
            Bitmap bitmapMargin = new(bitmap.Width + margin.Left + margin.Right,
                               bitmap.Height + margin.Top + margin.Bottom);

            using Graphics g = Graphics.FromImage(bitmapMargin);
            g.Clear(backColor);
            g.DrawImage(bitmap, margin.Left, margin.Top);
            g.Save();

            bitmapMargin.Save(filePath, ImageFormat.Png);
        }

        private static string GetFileName(string iconName, IconStyle style)
        {
            string fileName;

            if (string.IsNullOrEmpty(style.Prefix))
            {
                fileName = iconName;
            }
            else
            {
                fileName = $"{style.Prefix}_{iconName}";
            }

            if (style.BitmapProperties.Count > 0)
            {
                fileName = CreateFileNameWithProperties(fileName, style.BitmapProperties);
            }

            return fileName;
        }

        private static string CreateOutputFolder(string outputFolder, string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return outputFolder;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory($"{outputFolder}\\_{prefix}");
                    return $"{outputFolder}\\_{prefix}";
                }
                catch (Exception exception)
                {
                    Log.WriteLine($"Error creating output folder, {exception.Message}");
                    return outputFolder;
                }
            }
        }
    }
}

using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using System.Xml.Serialization;
using IRL_Common_Library.Consts;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;

namespace IRL_Image_Creator.Projects
{
    [Serializable]
    public class Project
    {
        [XmlElement]
        public string Name { get; set; } = "";

        [XmlElement]
        public string UserSourceFolder { get; set; } = "";

        [XmlArray("")]
        public List<ConverterInstruction> Instructions { get; set; } = new();

        [XmlArray("")]
        public List<TextStyle> TextStyles { get; set; } = new();

        [XmlArray("")]
        public List<FontBitmapStyle> FontBitmapStyles { get; set; } = new();

        [XmlArray("")]
        public List<IconStyle> IconStyles { get; set; } = new();

        [XmlArray("")]
        public List<ConverterColor> Colors { get; set; } = new();

        [XmlArray("")]
        public List<ConverterFont> Fonts { get; set; } = new();

        [XmlElement]
        public ImageBuilderSettings ImageBuilderSettings { get; set; } = new();

        public static Project Create(string name, string folder)
        {
            Project project = new()
            {
                Name = name,
            };

            Environment.CurrentDirectory = folder ?? Environment.CurrentDirectory;

            project.ImageBuilderSettings.DataLocations.Add(new DataLocation(0, DataLocationType.File_1, CompressionType.None));
            Save(project);

            CreateProjectFolders();

            return project;
        }

        public static Project Open(FileInfo fileInfo)
        {
            Project project = new();

            Environment.CurrentDirectory = fileInfo.DirectoryName ?? Environment.CurrentDirectory;

            using FileStream stream = new(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            XmlSerializer serializer = new(project.GetType());

            project = (Project)serializer.Deserialize(stream);
            stream.Close();

            CreateProjectFolders();

            return project;
        }

        public static void Save(Project project)
        {
            if (string.IsNullOrEmpty(project.Name))
            {
                return;
            }

            using FileStream stream = new(GetFullFilePath(project), FileMode.Create, FileAccess.Write);
            XmlSerializer serializer = new(project.GetType());
            serializer.Serialize(stream, project);
            stream.Close();
        }

        public static string GetFullFilePath(Project project)
        {
            return Path.Combine(Environment.CurrentDirectory, project.Name + FileConstants.PROJECT_FILE_EXTENSION);
        }

        private static void CreateProjectFolders()
        {
            Directory.CreateDirectory(FileConstants.GetLogFolder());
            Directory.CreateDirectory(FileConstants.GetBmpImportFolder());
            Directory.CreateDirectory(FileConstants.GetFontImportFolder());
            Directory.CreateDirectory(FileConstants.GetConverterOutputFolder());
            Directory.CreateDirectory(FileConstants.GetSourceFolder());
        }

        public static void UpdateTextStyleFonts(Project project)
        {
            foreach (TextStyle textStyle in project.TextStyles)
            {
                int fontId = textStyle.FontConverterId;

                if (fontId != -1)
                {
                    ConverterFont font = ConverterFont.GetFontById(fontId, project.Fonts);

                    if (font != null)
                    {
                        textStyle.FontName = font.FontName;
                        textStyle.FontSize = font.FontSize;
                        textStyle.FontStyle = font.FontStyle;
                    }
                    else
                    {
                        textStyle.FontConverterId = -1;
                    }
                }
            }
        }

        public static void UpdateTextStyleColors(Project project)
        {
            foreach (TextStyle textStyle in project.TextStyles)
            {
                int textColorId = textStyle.TextConverterColorId;
                int backColorId = textStyle.BackConverterColorId;

                if (textColorId != -1)
                {
                    ConverterColor textColor = ConverterColor.GetColorById(textColorId, project.Colors);
                    if (textColor != null)
                    {
                        textStyle.TextColor = textColor.Color;
                    }
                    else
                    {
                        textStyle.TextConverterColorId = -1;
                    }
                }

                if (backColorId != -1)
                {
                    ConverterColor backColor = ConverterColor.GetColorById(backColorId, project.Colors);
                    if (backColor != null)
                    {
                        textStyle.BackColor = backColor.Color;
                    }
                    else
                    {
                        textStyle.BackConverterColorId = -1;
                    }
                }
            }
        }

        public static void UpdateFontInstructionFonts(Project project)
        {
            foreach (ConverterInstruction converterInstruction in project.Instructions)
            {
                if (converterInstruction is FontInstruction fontInstruction)
                {
                    foreach (FontBitmap fontBitmap in fontInstruction.FontBitmaps)
                    {
                        int fontId = fontBitmap.FontConverterId;

                        if (fontId != -1)
                        {
                            ConverterFont font = ConverterFont.GetFontById(fontId, project.Fonts);

                            if (font != null)
                            {
                                fontBitmap.FontName = font.FontName;
                                fontBitmap.FontSize = font.FontSize;
                                fontBitmap.FontStyle = font.FontStyle;
                            }
                            else
                            {
                                fontBitmap.FontConverterId = -1;
                            }
                        }
                    }
                }
            }
        }

        public static void UpdateFontStyleColors(Project project)
        {
            foreach (FontBitmapStyle fontBitmapStyle in project.FontBitmapStyles)
            {
                int textColorId = fontBitmapStyle.TextConverterColorId;
                int backColorId = fontBitmapStyle.BackConverterColorId;

                if (textColorId != -1)
                {
                    ConverterColor textColor = ConverterColor.GetColorById(textColorId, project.Colors);
                    if (textColor != null)
                    {
                        fontBitmapStyle.TextColor = textColor.Color;
                    }
                    else
                    {
                        fontBitmapStyle.TextConverterColorId = -1;
                    }
                }

                if (backColorId != -1)
                {
                    ConverterColor backColor = ConverterColor.GetColorById(backColorId, project.Colors);
                    if (backColor != null)
                    {
                        fontBitmapStyle.BackColor = backColor.Color;
                    }
                    else
                    {
                        fontBitmapStyle.BackConverterColorId = -1;
                    }
                }
            }
        }

        public static void UpdateIconStyleColors(Project project)
        {
            foreach (IconStyle iconStyle in project.IconStyles)
            {
                int colorId = iconStyle.BackConverterColorId;

                if (colorId != -1)
                {
                    ConverterColor color = ConverterColor.GetColorById(colorId, project.Colors);
                    if (color != null)
                    {
                        iconStyle.BackColor = color.Color;
                    }
                    else
                    {
                        iconStyle.BackConverterColorId = -1;
                    }
                }

                foreach (ImageSvgColorId svgColorId in iconStyle.SvgColors)
                {
                    colorId = svgColorId.ConverterColorId;

                    if (colorId != -1)
                    {
                        ConverterColor color = ConverterColor.GetColorById(colorId, project.Colors);
                        if (color != null)
                        {
                            svgColorId.Color = color.Color;
                        }
                        else
                        {
                            svgColorId.ConverterColorId = -1;
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaletteExtractor.Backend
{
  public class BitmapFile
  {
    public string Path { get; private set; }

    public BitmapFile(string path)
    {
      EnsurePathLeadsToImage(path);
      EnsureFileExists(path);
      this.Path = path;
    }

    public Bitmap ExtractPalette(int imageWidth)
    {
      var bitmap = new Bitmap(this.Path);
      List<Color> colorList = this.GetColorList(bitmap);
      Bitmap output = this.GenerateImage(colorList, imageWidth);
      return output;
    }

    private void EnsurePathLeadsToImage(string path)
    {
      string extension = System.IO.Path.GetExtension(path);
      switch (extension)
      {
        case ".png":
        case ".bmp":
        case ".gif":
        case ".jpg":
        case ".jpeg":
          return;
        default:
          throw new ArgumentException("Invalid file path provided. File must be an image.");
      }
    }

    private void EnsureFileExists(string path)
    {
      bool exists = File.Exists(path);
      if (!exists)
      {
        throw new ArgumentException("Specified file does not exist.");
      }
    }

    private List<Color> GetColorList(Bitmap bitmapToProcess)
    {
      List<Color> colors   = new List<System.Drawing.Color>();
      for (int y = 0; y < bitmapToProcess.Height; y++)
      {
        for (int x = 0; x < bitmapToProcess.Width; x++)
        {
          Color currentPixel = bitmapToProcess.GetPixel(x, y);
          Int32 colorMatchCount = (from c in colors
                                   where c.A == currentPixel.A
                                             &&    c.R == currentPixel.R
                                             &&    c.G == currentPixel.G
                                             &&    c.B == currentPixel.B
                                   select c).Count();
          if (colorMatchCount == 0)
          {
            colors.Add(currentPixel);
          }
        }
      }
      return colors;
    }

    private Bitmap GenerateImage(List<Color> colorList, int imageWidth)
    {
      Int32 imageHeight     = Convert.ToInt32(Math.Ceiling((Double)colorList.Count / imageWidth));
      Int32 colorIndex      = 0;
      Bitmap generatedImage = new Bitmap(imageWidth, imageHeight);
      for (int y = 0; y < imageHeight; y++)
      {
        for (int x = 0; x < imageWidth; x++)
        {
          if (colorIndex <= colorList.Count() - 1)
          {
            generatedImage.SetPixel(x, y, colorList[colorIndex]);
            colorIndex++;
          }
        }
      }
      return generatedImage;
    }
  }
}

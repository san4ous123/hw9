using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using System.IO;

namespace laba9.Models
{
    public class ImageNode
    {
        public ObservableCollection<ImageNode> Subfolders { get; set; }
        public string Name { get; }
        public string StrPath { get; set; }
        public Bitmap Image { get; set; }
        public ImageNode Parent { get; set; }
        public ImageNode(string strPath, bool isImage = false, ImageNode parent = null)
        {
            Subfolders = new ObservableCollection<ImageNode>();
            StrPath = strPath;
            Name = Path.GetFileName(strPath);
            if (Name == "")
            {
                Name = strPath;
            }
            if (isImage)
            {
                Image = new Bitmap(strPath);
                Parent = parent;
            }
        }
        public void LoadSubfolders()
        {
            try
            {
                string[] subdirs = Directory.GetDirectories(StrPath, "*", SearchOption.TopDirectoryOnly);

                foreach (string dir in subdirs)
                {
                    ImageNode currentNode = new ImageNode(dir);
                    Subfolders.Add(currentNode);
                }
                LoadImages();
            }
            catch
            {

            }
        }
        public void LoadImages()
        {
            List<string> images = new List<string>();
            images.AddRange(Directory.GetFiles(StrPath, "*.*", SearchOption.TopDirectoryOnly)
                             .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png")).ToArray());
            if (images.Count > 0)
            {
                foreach (string image in images)
                {
                    ImageNode imageNode = new ImageNode(image, true, this);
                    Subfolders.Add(imageNode);
                }
            }
        }
    }
}
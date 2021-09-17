using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Drawing.Configuration;

namespace Project_2
{
    public partial class Form1 : Form
    {
        /// <summary>
        ///  Create a list to hold all file information and create temporary variables for all file types
        /// </summary>
        GenericFile tempFile = new GenericFile();
        Document tempDocument = new Document();
        Image tempImage = new Image();
        Audio tempAudio = new Audio();
        Video tempVideo = new Video();

        List<GenericFile> FileList = new List<GenericFile>();

        /// <summary>
        /// Builds form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// opens FolderBrowserDialogue and collects applicable files by file extension and displays in listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileSearchButton_Click(object sender, EventArgs e)
        {
            // Exception Handling
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog1.SelectedPath;
                fileListBox.Items.Clear();
                adressBar.Clear();
                adressBar.Text = folderPath;

                DirectoryInfo directory = new DirectoryInfo(folderPath);
                // Clear files
                FileList.Clear();

                FileInfo[] Files = directory.GetFiles();
                // Collects images by file extension
                foreach (FileInfo file in Files)
                {
                    if (file.Extension == ".png" || file.Extension == ".jpg" || file.Extension == ".bmp"
                        || file.Extension == ".PNG" || file.Extension == ".JPG" || file.Extension == ".BMP")
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(folderPath + "/" + file.Name);
                        tempImage = new Image
                        {
                            Name = file.Name,
                            Extension = file.Extension,
                            Size = (int)file.Length,
                            CreationDate = file.CreationTime.ToString(),
                            LastModified = file.LastWriteTime.ToString(),
                            Width = img.Width,
                            Height = img.Height,
                            HRes = (int)img.HorizontalResolution,
                            VRes = (int)img.VerticalResolution
                        };

                        FileList.Add(tempImage);
                    }
                    // Collects documents by file extension
                    else if (file.Extension == ".pdf" || file.Extension == ".docx" || file.Extension == ".txt"
                       || file.Extension == ".PDF" || file.Extension == ".DOCX" || file.Extension == ".TXT")
                    {
                        tempDocument = new Document
                        {
                            Name = file.Name,
                            Extension = file.Extension,
                            Size = (int)file.Length,
                            CreationDate = file.CreationTime.ToString(),
                            LastModified = file.LastWriteTime.ToString(),
                        };

                        FileList.Add(tempDocument);
                    }
                    // Collects video by file extension
                    else if (file.Extension == ".mp4" || file.Extension == ".avi" || file.Extension == ".mkv"
                        || file.Extension == ".MP4" || file.Extension == ".AVI" || file.Extension == ".MKV")
                    {
                        // uses NuGet package TagLib to get length data from video file
                        var timeFile = TagLib.File.Create(folderPath + "/" + file.Name);

                        tempVideo = new Video()
                        {
                            Name = file.Name,
                            Extension = file.Extension,
                            Size = (int)file.Length,
                            CreationDate = file.CreationTime.ToString(),
                            LastModified = file.LastWriteTime.ToString(),
                            Length = timeFile.Properties.Duration
                        };

                        FileList.Add(tempVideo);
                    }
                    // Collects audio by file extension
                    else if (file.Extension == ".mp3" || file.Extension == ".aac" || file.Extension == ".MP3" || file.Extension == ".AAC")
                    {
                        // uses NuGet package TagLib to get length data from Audio file
                        var timeFile = TagLib.File.Create(folderPath + "/" + file.Name);

                        tempAudio = new Audio()
                        {
                            Name = file.Name,
                            Extension = file.Extension,
                            Size = (int)file.Length,
                            CreationDate = file.CreationTime.ToString(),
                            LastModified = file.LastWriteTime.ToString(),
                            Length = timeFile.Properties.Duration
                        };

                        FileList.Add(tempAudio);
                    }
                    else
                    {
                        tempFile = new GenericFile
                        {
                            Name = file.Name,
                            Extension = file.Extension,
                            Size = (int)file.Length,
                            CreationDate = file.CreationTime.ToString(),
                            LastModified = file.LastWriteTime.ToString()
                        };

                        FileList.Add(tempFile);
                    }
                    // Adds file infromation to list box
                    fileListBox.Items.Add(file.Name + "\t" + "\t" + file.Extension + "\t" + "\t" + file.Length + "\t" + "\t" + file.LastWriteTime);
                }
            }
        }

    }
}

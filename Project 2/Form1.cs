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

        List<Audio> audioList = new List<Audio>();
        List<Video> videoList = new List<Video>();
        List<Image> imageList = new List<Image>();
        List<Document> documentList = new List<Document>();

        //  Declare int variables as 0


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
            //  Set integer values for file counters
            int numAudio = 0;
            int numImage = 0;
            int numDocument = 0;
            int numVideo = 0;
            int numFiles = 0;
            int numMedia = 0;
            int numOther = 0;

            // Exception Handling
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
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
                        numImage += 1;
                        FileList.Add(tempFile);
                        imageList.Add(tempImage);
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
                        numDocument += 1;
                        FileList.Add(tempFile);
                        //documentList.Add(tempDocument);
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
                        numVideo += 1;
                        //FileList.Add(tempVideo);
                        videoList.Add(tempVideo);
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
                        numAudio += 1;
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
                        numOther += 1;
                        FileList.Add(tempFile);
                    }
                    // Adds file infromation to list box
                    fileListBox.Items.Add(file.Name + "\t" + "\t" + file.Extension + "\t" + "\t" + file.Length + "\t" + "\t" + file.LastWriteTime);
                }
            }

            //  Assign TextBoxes to file counters
            numFiles = (numImage + numVideo + numDocument + numAudio + numOther);
            numberOfImagesTextBox.Text = numImage.ToString();
            numberOfVideoTextBox.Text = numVideo.ToString();
            numberOfAudioTextBox.Text = numAudio.ToString();
            numberOfDocumentsTextBox.Text = numDocument.ToString();
            numberOfMediaTextBox.Text = numMedia.ToString();
            numberofOtherFilesTextBox.Text = numOther.ToString();
            totalNumberOfFilesTextBox.Text = numFiles.ToString();
        }

        private void fileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                if (fileListBox.SelectedIndex != -1)
                {
                    int selected = fileListBox.SelectedIndex;
                    /*if (FileList[selected] is Image)
                    {
                        //  Assigned TextBoxes to selected index in ListBox with respective properties
                        nametTextBox.Text = FileList[selected].Name;
                        extTextBox.Text = FileList[selected].Extension;
                        fileSize.Text = FileList[selected].Size.ToString();
                        creationDate.Text = FileList[selected].CreationDate;
                        modDate.Text = FileList[selected].LastModified;
                        userComment.Text = FileList[selected].UserComment;
                    }*/
                    if (imageList[selected] is Image)
                    {
                        nametTextBox.Text = imageList[selected].Name;
                        extTextBox.Text = imageList[selected].Extension;
                        fileSize.Text = imageList[selected].Size.ToString();
                        creationDate.Text = imageList[selected].CreationDate;
                        modDate.Text = imageList[selected].LastModified;
                        userComment.Text = imageList[selected].UserComment;
                        //  Image properties from selected index show
                        imgWidth.Text = imageList[selected].Width.ToString();
                        imgHeight.Text = imageList[selected].Height.ToString();
                        imgHRes.Text = imageList[selected].HRes.ToString();
                        imgVRes.Text = imageList[selected].VRes.ToString();

                    }
                    else if (videoList[selected] is Video)
                    {
                        //  Video properties from selected index show
                        nametTextBox.Text = videoList[selected].Name;
                        extTextBox.Text = videoList[selected].Extension;
                        fileSize.Text = videoList[selected].Size.ToString();
                        creationDate.Text = videoList[selected].CreationDate;
                        modDate.Text = videoList[selected].LastModified;
                        userComment.Text = videoList[selected].UserComment;

                        //videoDirector.Text = videoList[selected].Director;
                        //videoProducer.Text = videoList[selected].Producer;
                        //mediaLength.Text = videoList[selected].Length.ToString();

                    }
                    else if (FileList[selected] is Audio)
                    {
                        nametTextBox.Text = FileList[selected].Name;
                        extTextBox.Text = FileList[selected].Extension;
                        fileSize.Text = FileList[selected].Size.ToString();
                        creationDate.Text = FileList[selected].CreationDate;
                        modDate.Text = FileList[selected].LastModified;
                        userComment.Text = FileList[selected].UserComment;

                    }
                    else if (FileList[selected] is Document)
                    {
                        nametTextBox.Text = FileList[selected].Name;
                        extTextBox.Text = FileList[selected].Extension;
                        fileSize.Text = FileList[selected].Size.ToString();
                        creationDate.Text = FileList[selected].CreationDate;
                        modDate.Text = FileList[selected].LastModified;
                        userComment.Text = FileList[selected].UserComment;


                    }
                }
            }

            catch (Exception ex)
            {
                // Display an error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}

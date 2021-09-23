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
                        FileList.Add(tempImage);
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
                        FileList.Add(tempDocument);
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
                        FileList.Add(tempVideo);
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
                        audioList.Add(tempAudio);
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
            numberofOtherFilesTextBox.Text = numOther.ToString();
            totalNumberOfFilesTextBox.Text = numFiles.ToString();
        }

        private void fileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                if (fileListBox.SelectedIndex != -1)
                {
                    //int selected = fileListBox.SelectedIndex;
                    if (FileList[fileListBox.SelectedIndex] is Image)
                    {
                        Image SelectedImage = (Image)FileList[fileListBox.SelectedIndex];
                        //  Assigned TextBoxes to selected index in ListBox with respective properties
                        nametTextBox.Text = SelectedImage.Name;
                        extTextBox.Text = SelectedImage.Extension;
                        fileSize.Text = SelectedImage.Size.ToString();
                        creationDate.Text = SelectedImage.CreationDate;
                        modDate.Text = SelectedImage.LastModified;
                        userComment.Text = SelectedImage.UserComment;

                        imgWidth.Text = SelectedImage.Width.ToString();
                        imgHeight.Text = SelectedImage.Height.ToString();
                        imgHRes.Text = SelectedImage.HRes.ToString();
                        imgVRes.Text = SelectedImage.VRes.ToString();
                    }
                    /*if (imageList[selected] is Image)
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

                    }*/
                    else if (FileList[fileListBox.SelectedIndex] is Video)
                    {
                        Video SelectedVideo = (Video)FileList[fileListBox.SelectedIndex];
                        //  Video properties from selected index show
                        nametTextBox.Text = SelectedVideo.Name;
                        extTextBox.Text = SelectedVideo.Extension;
                        fileSize.Text = SelectedVideo.Size.ToString();
                        creationDate.Text = SelectedVideo.CreationDate;
                        modDate.Text = SelectedVideo.LastModified;
                        userComment.Text = SelectedVideo.UserComment;

                        //videoDirector.Text = videoList[selected].Director;
                        //videoProducer.Text = videoList[selected].Producer;
                        mediaLength.Text = SelectedVideo.Length.ToString();

                    }
                    else if (FileList[fileListBox.SelectedIndex] is Audio)
                    {
                        Audio SelectedAudio = (Audio)FileList[fileListBox.SelectedIndex];
                        nametTextBox.Text = SelectedAudio.Name;
                        extTextBox.Text = SelectedAudio.Extension;
                        fileSize.Text = SelectedAudio.Size.ToString();
                        creationDate.Text = SelectedAudio.CreationDate;
                        modDate.Text = SelectedAudio.LastModified;
                        userComment.Text = SelectedAudio.UserComment;

                        mediaLength.Text = SelectedAudio.Length.ToString();

                    }
                    else if (FileList[fileListBox.SelectedIndex] is Document)
                    {
                        Document SelectedDocument = (Document)FileList[fileListBox.SelectedIndex];
                        nametTextBox.Text = SelectedDocument.Name;
                        extTextBox.Text = SelectedDocument.Extension;
                        fileSize.Text = SelectedDocument.Size.ToString();
                        creationDate.Text = SelectedDocument.CreationDate;
                        modDate.Text = SelectedDocument.LastModified;
                        userComment.Text = SelectedDocument.UserComment;
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
        private void UserCommentCheck()
        {
            if (userComment.Text == string.Empty)
            {
                MessageBox.Show("You must enter a comment.");
            }
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            if (fileListBox.SelectedIndex != -1)
            {

                /*else if (numOfPages.Text == string.Empty)
                {
                    MessageBox.Show("You must enter a page amount.");
                }*/

                //int selected = fileListBox.SelectedIndex;
                if (FileList[fileListBox.SelectedIndex] is Image)
                {
                    Image newImage = (Image)FileList[fileListBox.SelectedIndex];
                    //  Assigned TextBoxes to selected index in ListBox with respective properties
                    newImage.UserComment = userComment.Text;
                    //  Check if the TextBoxes are empty

                    UserCommentCheck();
                }
                else if (FileList[fileListBox.SelectedIndex] is Document)
                {
                    //  Method to check if comment TextBox is filled out
                    UserCommentCheck();

                    //  Check entry boxes
                    if (numOfPages.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a page amount.");
                    }
                    if (numOfWords.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a word amount.");
                    }
                    if (docSubject.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a subject.");
                    }
                    else
                    {
                        Document newDocument = (Document)FileList[fileListBox.SelectedIndex];

                        //  Assigned TextBoxes to selected index in ListBox with respective properties
                        newDocument.UserComment = userComment.Text;
                        newDocument.NumPages = int.Parse(numOfPages.Text);
                        newDocument.NumWords = int.Parse(numOfWords.Text);
                        newDocument.DocSubject = docSubject.Text;
                    }
                }
                else if (FileList[fileListBox.SelectedIndex] is Video)
                {
                    //  Method to check if comment TextBox is filled out
                    UserCommentCheck();

                    //  Check entry boxes
                    if (videoDirector.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a director.");
                    }
                    if (videoProducer.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a producer.");
                    }
                    if (mediaTitle.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a title.");
                    }
                    if (mediaRating.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a rating.");
                    }
                    else
                    {
                        Video newVideo = (Video)FileList[fileListBox.SelectedIndex];
                        //  Assigned TextBoxes to selected index in ListBox with respective properties
                        newVideo.UserComment = userComment.Text;
                        newVideo.Director = videoDirector.Text;
                        newVideo.Producer = videoProducer.Text;
                        newVideo.Title = mediaTitle.Text;
                        newVideo.Rating = int.Parse(mediaRating.Text);
                    }

                }
                else if (FileList[fileListBox.SelectedIndex] is Audio)
                {
                    //  Method to check if comment TextBox is filled out
                    UserCommentCheck();

                    //  Check entry boxes
                    if (audioArtist.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter an artist.");
                    }
                    if (audioBitrate.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a bitrate value.");
                    }
                    if (mediaTitle.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a title.");
                    }
                    if (mediaRating.Text == string.Empty)
                    {
                        MessageBox.Show("You must enter a rating.");
                    }
                    else
                    {
                        Audio newAudio = (Audio)FileList[fileListBox.SelectedIndex];
                        //  Assigned TextBoxes to selected index in ListBox with respective properties
                        newAudio.UserComment = userComment.Text;
                        newAudio.Artist = audioArtist.Text;
                        newAudio.BitRate = int.Parse(audioBitrate.Text);
                        newAudio.Title = mediaTitle.Text;
                        newAudio.Rating = int.Parse(mediaRating.Text);
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (fileListBox.SelectedIndex != -1)
            {
                //int selected = fileListBox.SelectedIndex;
                if (FileList[fileListBox.SelectedIndex] is Image)
                {
                    Image newImage = (Image)FileList[fileListBox.SelectedIndex];
                    //  Assigned TextBoxes to selected index in ListBox with respective properties
                    newImage.UserComment = "";
                }
                else if (FileList[fileListBox.SelectedIndex] is Document)
                {
                    Document newDocument = (Document)FileList[fileListBox.SelectedIndex];
                    //  Assigned TextBoxes to selected index in ListBox with respective properties
                    newDocument.UserComment = "";
                    newDocument.NumPages = 0;
                    newDocument.NumWords = 0;
                    newDocument.DocSubject = "";
                }
                else if (FileList[fileListBox.SelectedIndex] is Video)
                {
                    Video newVideo = (Video)FileList[fileListBox.SelectedIndex];
                    //  Assigned TextBoxes to selected index in ListBox with respective properties
                    newVideo.UserComment = "";
                    newVideo.Director = "";
                    newVideo.Producer = "";
                    newVideo.Title = "";
                    newVideo.Rating = 0;
                }
                else if (FileList[fileListBox.SelectedIndex] is Audio)
                {
                    Audio newAudio = (Audio)FileList[fileListBox.SelectedIndex];
                    //  Assigned TextBoxes to selected index in ListBox with respective properties
                    newAudio.UserComment = "";
                    newAudio.Artist = "";
                    newAudio.BitRate = 0;
                    newAudio.Title = "";
                    newAudio.Rating = 0;
                }
            }
        }

        private void NameSortButton_Click(object sender, EventArgs e)
        {
            //  Sort alphabetically
            fileListBox.Sorted = true;
        }
    }
}

using System.Runtime.InteropServices;

namespace Instagram_Poster
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();  //allocates a new console for the calling process

        private string fileDir;

        public Form1()
        {
            InitializeComponent();
            AllocConsole();  //open the console when the form starts
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = imgur.uploadToImgur(fileDir);
            if (url == null) return;
            string id = InstagramAPI.uploadPhotoToInstagram(url, textBox1.Text);
            if (id == null) return;
            InstagramAPI.publishPhoto(id);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*"; //allow all file types

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileDir = openFileDialog.FileName;
                }
            }
            button1.Enabled = true;
        }
    }
}

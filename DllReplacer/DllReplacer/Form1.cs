using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DllReplacer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.RestoreDirectory = false;
            openFileDialog1.Title = "Browse DLL";
            openFileDialog1.DefaultExt = "dll";
            openFileDialog1.Filter = "dll files (*.dll)|*.dll";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Multiselect = false;

            
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFileName.Text == string.Empty || txtDirectory.Text == string.Empty)
                {
                    btnReplace.Enabled = false;
                    return;
                }

                FileVersionInfo vinfo = FileVersionInfo.GetVersionInfo(txtFileName.Text);
                string[] filesToBeRepalced = Directory.GetFiles(txtDirectory.Text, "*.dll", SearchOption.AllDirectories);

                foreach (var item in filesToBeRepalced)
                {
                    FileVersionInfo dinfo = FileVersionInfo.GetVersionInfo(item);
                    if (dinfo.OriginalFilename == vinfo.OriginalFilename && dinfo.FileVersion == vinfo.FileVersion)
                    {
                        File.Copy(vinfo.FileName, dinfo.FileName, true);

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            // Clear previous select
            txtFileName.Text = string.Empty;
            btnReplace.Enabled = false;

            DialogResult dr = openFileDialog1.ShowDialog();
           if(dr == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
                btnReplace.Enabled = true;
            }

        }

        private void txtDirectorySearch_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                txtDirectory.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}

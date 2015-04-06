using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Orleans.EventSourcing;

namespace Dotpay.EventTypeCodeRegisterTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            var code = "var typeCodeDic = new Dictionary<string, uint>();" + Environment.NewLine;
            if (result == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
                var fName = openFileDialog1.FileName;

                if (File.Exists(fName))
                {
                    txtCode.Text = string.Empty;
                    var assembly = Assembly.LoadFrom(fName);
                    uint seedBase = 1000;
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.IsClass && !type.IsAbstract && !type.IsInterface && typeof(GrainEvent).IsAssignableFrom(type))
                        {
                            ++seedBase;
                            code += "typeCodeDic.Add(\"" + type.FullName + "\"," + seedBase + ");" + Environment.NewLine;
                        }
                    }
                    txtCode.Text = code;
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var code = "var typeCodeDic = new Dictionary<string, uint>();" + Environment.NewLine;
            var fName = openFileDialog1.FileName;

            if (File.Exists(fName))
            {
                txtCode.Text = string.Empty;
                var assembly = Assembly.LoadFrom(fName);
                uint seedBase = 1000;
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass && !type.IsAbstract && !type.IsInterface &&
                        typeof (GrainEvent).IsAssignableFrom(type))
                    {
                        ++seedBase;
                        code += "typeCodeDic.Add(\"" + type.FullName + "\"," + seedBase + ");" + Environment.NewLine;
                    }
                }
                txtCode.Text = code;
            }
            else
            {
                MessageBox.Show("file not exist");
            }
        }

    }
}

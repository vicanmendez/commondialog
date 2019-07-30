using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonDialog
{
    public partial class Form1 : Form
    {
        // Objetos PrintDocument para utilizar en el PrintDialog
        private System.Drawing.Printing.PrintDocument docToPrint =
            new System.Drawing.Printing.PrintDocument();

        public Form1()
        {
            InitializeComponent();
        }

        //ColorDialog
        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = label.ForeColor;
           
            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                label.ForeColor = MyDialog.Color;
        }

        //FileDialog
        private void button2_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            MessageBox.Show(fileContent, "Contenido del archivo: " + filePath, MessageBoxButtons.OK);
        }

        //FolderBrowserDialog
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialogExampleForm fbd = new FolderBrowserDialogExampleForm();
            fbd.Visible = true;
        }


        //FontDialog
        private void button4_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog(); 
            fontDialog1.ShowColor = true;
            fontDialog1.Font = label.Font;
            fontDialog1.Color = label.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                label.Font = fontDialog1.Font;
                label.ForeColor = fontDialog1.Color;
            }

        }

        //PageSetupDialog
        private void button5_Click(object sender, EventArgs e)
        {
            //Creamos el objeto
            PageSetupDialog PageSetupDialog1 = new PageSetupDialog();
            // Initialize the dialog's PrinterSettings property to hold user
            // defined printer settings.
            PageSetupDialog1.PageSettings =
                new System.Drawing.Printing.PageSettings();

            // Initialize dialog's PrinterSettings property to hold user
            // set printer settings.
            PageSetupDialog1.PrinterSettings =
                new System.Drawing.Printing.PrinterSettings();

            //Do not show the network in the printer dialog.
            PageSetupDialog1.ShowNetwork = false;

            //Show the dialog storing the result.
            DialogResult result = PageSetupDialog1.ShowDialog();

            // If the result is OK, display selected settings in
            // ListBox1. These values can be used when printing the
            // document.
            if (result == DialogResult.OK)
            {
                object[] results = new object[]{
            PageSetupDialog1.PageSettings.Margins,
            PageSetupDialog1.PageSettings.PaperSize,
            PageSetupDialog1.PageSettings.Landscape,
            PageSetupDialog1.PrinterSettings.PrinterName,
            PageSetupDialog1.PrinterSettings.PrintRange};
            listBox.Items.AddRange(results);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Crear objeto PrintDialog
            PrintDialog PrintDialog1 = new PrintDialog();
            // Allow the user to choose the page range he or she would
            // like to print.
            PrintDialog1.AllowSomePages = true;

            // Show the help button.
            PrintDialog1.ShowHelp = true;

            // Set the Document property to the PrintDocument for 
            // which the PrintPage Event has been handled. To display the
            // dialog, either this property or the PrinterSettings property 
            // must be set 

            
            PrintDialog1.Document = docToPrint;

            DialogResult result = PrintDialog1.ShowDialog();

            // If the result is OK then print the document.
            if (result == DialogResult.OK)
            {
                docToPrint.Print();
            }
        }


        /* 
         El objeto PrintDialog imprimirá el documento a través de un evento PrintPage
         */
        private void document_PrintPage(object sender,
            System.Drawing.Printing.PrintPageEventArgs e)
        {

            // Insert code to render the page here.
            // This code will be called when the control is drawn.

            // The following code will render a simple
            // message on the printed document.
            string text = "Texto a imprimir";
            System.Drawing.Font printFont = new System.Drawing.Font
                ("Arial", 35, System.Drawing.FontStyle.Regular);

            // Draw the content.
            e.Graphics.DrawString(text, printFont,
                System.Drawing.Brushes.Black, 10, 10);
        }

    }
}

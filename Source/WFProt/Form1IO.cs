using System.Windows.Forms;
using System;


namespace WFProt
{

    public partial class Form1 : Form
    {
        // Acciones 
        public Action Out_NewFile { get; set; }
        public Action Out_CloseFile { get; set; }
        public Action<string> Out_LoadFile { get; set; }
        public Action<string> Out_SaveFile { get; set; }


        // Metodos Concretos
        private void NewFile()
        {
            Out_NewFile();
        }

        private void SaveFile(string file)
        {
            Out_SaveFile(file);
        }

        private void LoadFile(string file)
        {
            Out_LoadFile(file);
        }

        private void CloseFile()
        {
            Out_CloseFile();
        }

    }
}
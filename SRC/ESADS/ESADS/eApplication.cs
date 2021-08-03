using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using ESADS.GUI;
using ESADS.GUI.MainForm;

namespace ESADS
{
    /// <summary>
    /// Contains all necessary information related to the current application.
    /// </summary>
    public class eApplication
    {
        #region Methods

        /// <summary>
        /// Closes the application that is running currently.
        /// </summary>
        public static  void Close()
        {
            Application.Exit();
        }

        /// <summary>
        /// Runs ESADS application.
        /// </summary>
        public static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new eStartUpPictureForm());
            eMainForm mainForm = new eMainForm();
            try
            {
                FileStream stream = new FileStream(Environment.GetCommandLineArgs()[1], FileMode.Open, FileAccess.Read);
                BinaryFormatter formater = new BinaryFormatter();
                eDocument doc = (eDocument)formater.Deserialize(stream);
                doc.ModelForm = new eModelForm(doc);
                doc.IsSaved = true;
                stream.Dispose();
                Application.Run(new eMainForm(doc));
            }
            catch (Exception)
            { 
                Application.Run(new eMainForm());
            }          
        }
        #endregion
    }
}

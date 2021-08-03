using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using ESADS.Mechanics.Design.Column;
using ESADS;

namespace ESADS.Export.ToAutoCAD
{
    public static class eExportColumn
    {
        private static eDColumn col;
        private static AcadDocument doc;
        private static string[] lyrs;
        public static void Export(eDColumn column, AcadDocument acDoc, string[] layers)
        {
            col = column;
            doc = acDoc;
            lyrs = layers;
            AddColumnRect();
            AddDimensions();
            AddMainBars();
            AddStirrup();
            AddLables();
        }
        private static void InitializeComponents()
        {
            if (lyrs[1] != null)
                doc.Layers.Add(lyrs[1]).color = AcColor.acMagenta;
            if (lyrs[3] != null)
                doc.Layers.Add(lyrs[3]).color = AcColor.acWhite;
            if (lyrs[5] != null)
                doc.Layers.Add(lyrs[5]).color = AcColor.acCyan;
            if (lyrs[7] != null)
            {
                AcadLayer l = doc.Layers.Add(lyrs[7]);
                l.color = AcColor.acRed;
                l.Lineweight = ACAD_LWEIGHT.acLnWt030;
            }

        }
        private static void AddColumnRect()
        {
            if (lyrs[GetLayer("Column") + 1] != null)
            {

            }
        }

        private static void AddDimensions()
        {

        }

        private static void AddLables()
        {

        }

        private static void AddStirrup()
        {
        }

        private static void AddMainBars()
        {
        }
        private static int GetLayer(string name)
        {
            for (int i = 0; i < lyrs.Length; i++)
            {
                if (lyrs[i] == name)
                    return i;
            }
            return 0;
        }
    }
}

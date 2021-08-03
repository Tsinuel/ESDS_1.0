using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD;
using ESADS.Mechanics.Design.Column;
using ESADS.Mechanics.Design.Beam;

namespace ESADS.Export.ToAutoCAD
{
    public static class eAcExport
    {

        static AcadApplication apl;
        static AcadDocument doc;
        public static void ExportColumn(eDColumn column, string[] layerNames, eLengthUnits lengthUnit)
        {
            InitializeAutoCAD2007();
            eExportColumn.Export(column, doc, layerNames);
            apl.ZoomExtents();
        }

        public static void ExportBeam(eDBeam beam, string[] layerNames, eLengthUnits lengthUnits)
        {
            InitializeAutoCAD2007();
            eExportBeam.Export(beam, doc, layerNames);
            apl.ZoomExtents();
        }

        public static void ExportSlab()
        {
        }

        public static void ExportFooting()
        {
        }

        internal static void InitializeAutoCAD2007()
        {
            //Add aplication and document
            apl = new AcadApplication();           
            apl.Visible = true; 
            apl.WindowState = AcWindowState.acMax;
            doc = apl.ActiveDocument;
            doc.ActiveSpace = AcActiveSpace.acModelSpace;
            //

            //Add text styles
            doc.TextStyles.Item("Standard").SetFont("Arial", false, false, 1, 1);
            doc.TextStyles.Item("Standard").Height = 20;
            //
            
        }

        internal static void AddLine(double x1, double y1, double x2, double y2)
        {
            doc.ModelSpace.AddLine(GetPoint(x1, y1), GetPoint(x2, y2));
        }
        internal static void AddCircle(double x, double y, double diam)
        {
            doc.ModelSpace.AddCircle(GetPoint(x, y), diam / 2);
        }

        internal static void AddRectangle(double x, double y, double w, double h)
        {
            double[] pts = new double[5 * 3] { x, y, 0, x + w, y, 0, x + w, y + h, 0, x, y + h, 0, x, y, 0 };
            doc.ModelSpace.AddPolyline(pts);
        }

        internal static double[] GetPoint(double x, double y)
        {
            return new double[3] { x, y, 0 };
        }

        internal static void AddDim(double x1, double y1, double x2, double y2)
        {
            AcadDimAligned dim = doc.ModelSpace.AddDimAligned(GetPoint(x1, y1), GetPoint(x2, y2), GetPoint((x1 + x2) / 2, (y1 + y2) / 2));
            dim.PrimaryUnitsPrecision = AcDimPrecision.acDimPrecisionZero;
            dim.TextRotation = GetAgle(x1, y1, x2, y2);
        }

        internal static double GetAgle(double x1, double y1, double x2, double y2)
        {
           return Math.Atan((y2 - y1) / (x2 - x1));
        }

    }
}

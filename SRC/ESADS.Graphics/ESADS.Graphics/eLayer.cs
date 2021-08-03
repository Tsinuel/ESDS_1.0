using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESADS.EGraphics.Beam;
using ESADS.GUI;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Column;
using ESADS.EGraphics.Column;

namespace ESADS.EGraphics
{
    /// <summary>
    /// Represents layer of drawing used to place drawing objects with similar property and function.
    /// </summary>
    [Serializable]
    public class eLayer
    {
        #region Fields
        /// <summary>
        /// Accesses the public property 'Name'.
        /// </summary>
        private string name;
        /// <summary>
        /// Accesses the public property 'Color'.
        /// </summary>
        private eLineType lineType;
        /// <summary>
        /// Accesses the public property 'LayerOn'.
        /// </summary>
        private bool layerOn;
        /// <summary>
        /// Accesses the public property 'LineWeight'.
        /// </summary>
        private eLineWeight lineWeight;
        /// <summary>
        /// Accesses the public property 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// Accesses the public property 'TextStyle'.
        /// </summary>
        private eTextStyle textStyle;
        /// <summary>
        /// Contains all drawing in this layer.
        /// </summary>
        private List<eIDrawing> dwgObjects = new List<eIDrawing>();
        #endregion
        /// <summary>
        /// Point used to keep the layer aligned to others when it is turned on.
        /// </summary>
        private PointF pinPoint;
        /// <summary>
        /// The factor by which the layer has been scaled till now.
        /// </summary>
        private float zoomFactor;

        #region Constructors
        /// <summary>
        /// Creates an instance of ESADS.EGraphics.eLayer class from all the necessary parameters.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <param name="color">Color of the layer.</param>
        /// <param name="lineType">Line type of the layer.</param>
        /// <param name="lineWeight">Line thickness of the layer.</param>
        /// <param name="font">Font style of the layer.</param>
        public eLayer(string name ,Color color,eLineTypes lineType,float lineWeight,Font font, float zoomFactor = 1.0f)
        {
            this.name = name;
            this.color = new eColor(color, eChangeBy.ByLayer);
            this.lineType = new eLineType(lineType, eChangeBy.ByLayer);
            this.lineWeight = new eLineWeight(lineWeight, eChangeBy.ByLayer);
            this.layerOn = true;
            this.textStyle = new eTextStyle(font, eChangeBy.ByLayer);
            this.zoomFactor = zoomFactor;
        }

        /// <summary>
        /// Creates an istance of ESADS.EGraphics given name,color and lineType thickness with continuos lineType startPoint.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <param name="color">Color of the layer.</param>
        /// <param name="lineWeight">Color thickness of the layer.</param>
        public eLayer(string name,Color color,float lineWeight, float zoomFactor = 1.0f)
        {
            this.name = name;
            this.color.SetColor(color);
            this.lineWeight.SetLineWeight(lineWeight);
            this.lineType.SetLineType(eLineTypes.Continuous);
            this.layerOn = true;
            this.zoomFactor = zoomFactor;
        }

        /// <summary>
        /// Creates an istance of ESADS.EGraphics given name and color with continuos lineType startPoint and unity lineType thickness.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <param name="color">Color of the layer.</param>
        public eLayer(string name, Color color, float zoomFactor = 1.0f)
        {
            this.name = name;
            this.color.SetColor(color);
            this.lineWeight.SetLineWeight(1.0f);
            this.lineType.SetLineType(eLineTypes.Continuous);
            this.textStyle = new eTextStyle(new Font("Arial", 10), eChangeBy.ByLayer);
            this.layerOn = true;
            this.zoomFactor = zoomFactor;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the l
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the the line type of the layer.
        /// </summary>
        public eLineType LineType
        {
            get
            {
                return lineType;
            }
            set
            {
                lineType = value;
                OnModified();
            }
        }

        /// <summary>
        /// Gets or sets the value indication wether the layer is on or off.
        /// </summary>
        public bool LayerOn
        {
            get
            {
                return layerOn;
            }
            set
            {
                layerOn = value;
                if (value)
                    OnTurnedOn(new eLayerModifiedEventArgs(this.pinPoint, this.zoomFactor));
                else
                    OnTurnedOff(new eLayerModifiedEventArgs(this.pinPoint, this.zoomFactor));
            }
        }

        /// <summary>
        /// Gets or sets the lineType weight of the layer.
        /// </summary>
        public eLineWeight LineWeight
        {
            get
            {
                return lineWeight;
            }
            set
            {
                lineWeight = value;
                OnModified();
            }
        }

        /// <summary>
        /// Gets or sets the color of the layer.
        /// </summary>
        public eColor Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnModified();
            }
        }

        /// <summary>
        /// Gets or sets the Font of the layer.
        /// </summary>
        public eTextStyle TextStyle
        {
            get
            {
                return textStyle;
            }
            set
            {
                textStyle = value;
                OnModified();
            }
        }

        /// <summary>
        /// Gets the drawing object in this layer at the specified index .
        /// </summary>
        /// <param name="index">The index where the object found.</param>
        /// <returns>If the object </returns>
        public eIDrawing this[int index]
        {
            get
            {
                if (dwgObjects.Count != 0)
                    return dwgObjects[index];
                throw new Exception(" There is no drawing object in the layer or the index is out of range.");
            }
        }

        /// <summary>
        /// Gets the drawing objects in this layer.
        /// </summary>
        public List<eIDrawing> DwgObjects
        {
            get { return dwgObjects; }
        }
        #endregion   

        /// <summary>
        /// Gets the resultant vector that tells all the transformations of the layer.
        /// </summary>
        public PointF PinPoint
        {
            get
            {
                return pinPoint;
            }
        }

        /// <summary>
        /// Gets the cumulative zoom factor that the layer is scaled with.
        /// </summary>
        public float ZoomFactor
        {
            get
            {
                return zoomFactor;
            }
        }

        #region Methods

        /// <summary>
        /// Removes all objects in the layer.
        /// </summary>
        public void RemoveObjects()
        {
            dwgObjects = new List<eIDrawing>();
        }

        /// <summary>
        ///Adds a line to the layer.
        /// </summary>
        /// <param name="Start">The start point of the line.</param>
        /// <param name="End">The end point of the line. </param>
        public eLine AddLine(PointF Start ,PointF End)
        {
            dwgObjects.Add(new eLine(Start, End, this));
            return (eLine)dwgObjects[dwgObjects.Count - 1];
        }

        public eLine AddLine(double x1, double y1, double x2, double y2)
        {
            dwgObjects.Add(new eLine(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), this));
            return (eLine)dwgObjects[dwgObjects.Count - 1];
        }

        public eLine AddVerLine(double x, double y1, double y2)
        {
            dwgObjects.Add(new eLine(new PointF((float)x, (float)y1), new PointF((float)x, (float)y2), this));
            return (eLine)dwgObjects[dwgObjects.Count - 1];
        }

        public eLine AddHorLine(double x1, double x2, double y)
        {
            dwgObjects.Add(new eLine(new PointF((float)x1, (float)y), new PointF((float)x2, (float)y), this));
            return (eLine)dwgObjects[dwgObjects.Count - 1];
        }
        /// <summary>
        /// Adds a text_left to the layer.
        /// </summary>
        /// <param name="Text">Text to be drawn.</param>
        /// <param name="location">location of the center of the text_left.</param>
        /// <param name="Angle">Angle of the the text_left from x-axis counter clockwise.</param>
        public eText AddText(string Text, PointF Location, double Angle = 0)
        {
            dwgObjects.Add(new eText(Text, Location, (float)Angle, this));
            return (eText)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds a text_left to the layer.
        /// </summary>
        /// <param name="Text">Text to be drawn.</param>
        /// <param name="location">location of the center of the text_left.</param>
        /// <param name="Angle">Angle of the the text_left from x-axis counter clockwise.</param>
        public eText AddText(string Text, double x,double y, double Angle = 0)
        {
            dwgObjects.Add(new eText(Text, new PointF((float)x,(float)y), (float)Angle, this));
            return (eText)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds a polygon to the layer.
        /// </summary>
        public ePolygone AddPolyGon(PointF[] Points,eDrawType DrawType = eDrawType.Draw )
        {
            dwgObjects.Add(new ePolygone(Points, DrawType, this));
            return (ePolygone)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds a regular polygon to the layer.
        /// </summary>
        public ePolygone AddPolyGon(PointF Center, float Radius, int NumberOfSides, eDrawType DrawType = eDrawType.Hatch, HatchStyle HatchStyle = HatchStyle.DiagonalCross)
        {
            dwgObjects.Add(new ePolygone(Center, Radius, NumberOfSides, DrawType, this, color, color, HatchStyle, this.lineType, this.lineWeight));
            return (ePolygone)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds Dimension to the drawing.
        /// </summary>
        public eDimension AddDim(PointF Start, PointF End, string Text, eDimensionType DimType, eDimensionLinePosition DimLinePosition, float shortExtLength)
        {
            dwgObjects.Add(new eDimension(Start, End, Text, DimType, this, DimLinePosition, shortExtLength));
            return (eDimension)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds Dimension to the drawing.
        /// </summary>
        public eDimension AddDim(PointF Start, PointF End, string Text, eDimensionType DimType, float shortExtLength,eDimensionLinePosition DimLinePosition = eDimensionLinePosition.LeftOrAbove)
        {
            dwgObjects.Add(new eDimension(Start, End, Text, DimType, this, DimLinePosition, shortExtLength));
            return (eDimension)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds Dimension to the drawing.
        /// </summary>
        public eDimension AddHorDim(double x1, double x2, double y, string Text, float shortExtLength, eDimensionLinePosition DimLinePosition = eDimensionLinePosition.LeftOrAbove)
        {
            dwgObjects.Add(new eDimension(new PointF((float)x1, (float)y), new PointF((float)x2, (float)y), Text, eDimensionType.LinearHorizontal, this, DimLinePosition, shortExtLength));
            return (eDimension)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds Dimension to the drawing.
        /// </summary>
        public eDimension AddVerDim(double x, double y1, double y2, string Text, float shortExtLength, eDimensionLinePosition DimLinePosition = eDimensionLinePosition.LeftOrAbove)
        {
            dwgObjects.Add(new eDimension(new PointF((float)x, (float)y1), new PointF((float)x, (float)y2), Text, eDimensionType.LinearVertical, this, DimLinePosition, shortExtLength));
            return (eDimension)dwgObjects[dwgObjects.Count - 1];
        }


        public eDimension AddHorDistSymbol(double x1, double x2, double y)
        {
            double endMark = Math.Abs(x2 - x1) / 20;
            eDimension d = this.AddHorDim(x1, x2, y + endMark / 2, "", (float)(endMark / 2));
            d.ExtBeyondDimLines = (float)(endMark / 2);
            d.ArrowSize = (float)Math.Abs(x2 - x1) * 0.01f;
            return d;
        }

        public eDimension AddVerDistSymbol(double x, double y1, double y2)
        {
            double endMark = Math.Abs(y1 - y2) / 20;
            eDimension d = this.AddVerDim(x + endMark / 2, y1, y2, "", (float)(endMark / 2));
            d.ExtBeyondDimLines = (float)(endMark / 2);
            d.ArrowSize = (float)Math.Abs(y1 - y2) * 0.01f;
            return d;
        }

        /// <summary>
        /// Adds a circle to the layer.
        /// </summary>
        public eCircle AddCircle(PointF Location, double Radius,eDrawType DrawType = eDrawType.Draw)
        {
            dwgObjects.Add(new eCircle(Location, (float)Radius, DrawType, this));
            return (eCircle)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds a circle to the layer.
        /// </summary>
        public eCircle AddCircle(double x, double y, double Radius, eDrawType DrawType = eDrawType.Draw)
        {
            dwgObjects.Add(new eCircle(new PointF((float)x, (float)y), (float)Radius, DrawType, this));
            return (eCircle)dwgObjects[dwgObjects.Count - 1];
        }
        /// <summary>
        /// Adds a rectangle to the layer.
        /// </summary>
        public eRectangle AddRectangle(PointF Location, double Width, double Height, HatchStyle HatchStyle = HatchStyle.DiagonalCross, eDrawType DrawType = eDrawType.Draw)
        {
            dwgObjects.Add(new eRectangle(Location, (float)Width, (float)Height, DrawType, HatchStyle, this));
            return (eRectangle)dwgObjects[dwgObjects.Count - 1];
        }


        public eRectangle AddRectangle(double x, double y, double width, double height)
        {
            dwgObjects.Add(new eRectangle(new PointF((float)x, (float)y), (float)width, (float)height, this));
            return (eRectangle)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds a rectangle to the layer.
        /// </summary>
        public eRectangle AddRectangle(PointF Location, double Width, double Height)
        {
            dwgObjects.Add(new eRectangle(Location, (float)Width, (float)Height, this));
            return (eRectangle)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds a rectangle to the layer.
        /// </summary>
        public eRectangle AddRectangle(eRectangle rect)
        {
            dwgObjects.Add(rect);
            return (eRectangle)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds an arc to the layer.
        /// </summary>
        public eArc AddArc(PointF Center,double Radius,double StartAngle,double SweepAngle)
        {
            dwgObjects.Add(new eArc(Center, (float)Radius, (float)StartAngle, (float)SweepAngle,this));
            return (eArc)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds an arc to the layer.
        /// </summary>
        public eArc AddArc(double x,double y, double Radius, double StartAngle, double SweepAngle)
        {
            dwgObjects.Add(new eArc(new PointF((float)x,(float)y), (float)Radius, (float)StartAngle, (float)SweepAngle, this));
            return (eArc)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds an interaction diagram for Column
        /// </summary>
        /// <param name="column"></param>
        /// <param name="dwgForm"></param>
        /// <returns></returns>
        public eInteractionDiagram AddDiagram(eDColumn column, eModelForm dwgForm)
        {
            this.dwgObjects.Add(new eInteractionDiagram(column, this, dwgForm));
            return (eInteractionDiagram)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Creates new instance of a leader to indicate a point. This constructor cannot be used to form an arc type leader.
        /// </summary>
        /// <param name="type">The type of leader to be drawn. It cannot be an arc type leader.</param>
        /// <param name="text_left">The text_left to be drawn at the start of the leader.</param>
        /// <param name="underline">Boolean value to indicate whether to underline the text_left or not.</param>
        /// <param name="points">The sequence of points through which the leader passes.</param>
        public eLeader AddLeader(eLeaderType type, string text = null, bool underline = false, params PointF[] points)
        {

            this.dwgObjects.Add(new eLeader(type, this, text, underline, points));
            return (eLeader)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Creates an arc type leader with the start and end points and with a given curvature value.
        /// </summary>
        /// <param name="startPoint">The start point of the leader where the text_left is to be placed.</param>
        /// <param name="endPoint">The end point of the leader at the tip of the arrow.</param>
        /// <param name="curvature">The curvature value for the arc from the startPoint to the end leader with a number between 0 and 90.</param>
        /// <param name="lineType">The line type of the leader.</param>
        /// <param name="text_left">The text_left to be written with the leader.</param>
        /// <param name="underline">Boolean value whether to underline the text_left or not.</param>
        /// <param name="circleText">Value indication wheter the text_left is circled or not.</param>
        /// <param name="curveDirection">Value indicating the direction of the curve.</param>
        /// <param name="suppressArrow">Value indicating whether the membDimension arrow is visible or not.</param>
        /// <param name="suppressDot">Value indicating whether the dimesion dot is removed or not.</param>
        public eLeader AddLeader(PointF startPoint, PointF endPoint, float curvature, eLineTypes lineType, string text = null, eLeaderCurveDirection curveDirection = eLeaderCurveDirection.ToTheLeft, bool circleText = false, bool underline = false, bool suppressArrow = false, bool suppressDot = false)
        {
            this.dwgObjects.Add(new eLeader(startPoint, endPoint, curvature, this, lineType, text, curveDirection, circleText, underline, suppressArrow, suppressDot));
            return (eLeader)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds a ESADS.EGraphics.eGJoint object to the layer.
        /// </summary>
        /// <param name="location">location of the joint inidicating the point where the joint connect to the member.</param>
        /// <param name="joint">The joint related to this joint drawing.</param>
        /// <param name="orientation"> The alignment of the joint.</param>
        /// <param name="dwgForm">The form on which the drawing is done.</param>
        public eGJoint AddJoint(eGBeam beam, PointF location, eJoint joint, eLayer loadLayer, eLayer gridLayer, string gridName, eJointOrientation orientation, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGJoint(beam, location, joint, this, loadLayer, gridLayer, gridName, dwgForm, orientation));
            return (eGJoint)dwgObjects[dwgObjects.Count - 1];
        }

        /// <summary>
        /// Adds a ESADS.EGraphics.eGJoint object to the layer.
        /// </summary>
        /// <param name="location">location of the joint inidicating the point where the joint connect to the member.</param>
        /// <param name="joint">The joint related to this joint drawing.</param>
        /// <param name="dwgForm">The form on which the drawing is done.</param>
        public eGJoint AddJoint(eGBeam beam, PointF location, eJoint joint, eLayer loadLayer, eLayer gridLayer, string gridName, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGJoint(beam, location, joint, this, loadLayer, gridLayer, gridName, dwgForm));
            return (eGJoint)dwgObjects[dwgObjects.Count - 1];
        }
       
       
        /// <summary>
        /// Add a grid to this layer.
        /// </summary>
        /// <param name="location">The location of the grid point where this grid pass.</param>      
        /// <param name="text_left">The text_left to be displayed in the bubbles.</param>
        /// <param name="dwgForm">The form on which the drawing is done.</param>
        /// <param name="gridType">The orientation of the grid.</param>
        /// <param name="gridOrientation">The layer on which this drawing found.</param>
        public eGrid AddGrid(PointF location, string text, eModelForm dwgForm, eGridType gridType = eGridType.Vertical, eGridOrientation gridOrientation = eGridOrientation.TopLeft)
        {
            dwgObjects.Add(new eGrid(location, this, text, dwgForm, gridType, gridOrientation));
            eGrid g = (eGrid)dwgObjects[dwgObjects.Count - 1];

            g.Zoom(g.Location, zoomFactor);
            return g;
        }

        /// <summary>
        /// Adds a rectangular member drawing to the layer. It returns the created object for modification.
        /// </summary>
        /// <param name="member">The mechanics object associated with the drawing.</param>
        /// <param name="load_Tri">The mechanics object associated with the drawing.</param>
        /// <param name="location">The location of the far left intersection point with the member on which it is loaded/</param>
        /// <param name="dwgForm">The form on which the member is going to be drawn on.</param>
        public eGRectangularLoad AddRectangularLoad(eRectangularLoad load, eGMember membDWG, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGRectangularLoad(load, this, membDWG, dwgForm));
            return dwgObjects[dwgObjects.Count - 1] as eGRectangularLoad;
        }

        /// <summary>
        /// Zoom all the drawing in this layer.
        /// </summary>
        /// <param name="ZoomCenter">The zoom center from which zooming is done.</param>
        /// <param name="ZoomFactor">The zoom factor by which the drawing are enlarged.</param>
        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            if (!this.layerOn)
                return;

            pinPoint.X = (pinPoint.X - ZoomCenter.X) * ZoomFactor + ZoomCenter.X;
            pinPoint.Y = (pinPoint.Y - ZoomCenter.Y) * ZoomFactor + ZoomCenter.Y;

            zoomFactor *= ZoomFactor;

            Font f = this.textStyle;
            this.textStyle.SetFont(new Font(f.Name, f.Size * ZoomFactor));

            for (int i = 0; i < dwgObjects.Count; i++)
            {
                dwgObjects[i].Zoom(ZoomCenter, ZoomFactor);
            }
        }

        /// <summary>
        /// Pans all the drawing in the layer by specified offset in both axises.
        /// </summary>
        /// <param name="XOffset">The x-offset of the by which the drawing is moved in x-direction.</param>
        /// <param name="YOffset">The y-offset of the by which the drawing is moved in y-direction.</param>
        public void Pan(float XOffset, float YOffset)
        {
            if (!this.layerOn)
                return;

            pinPoint.X += XOffset;
            pinPoint.Y += YOffset;

            for (int i = 0; i < dwgObjects.Count; i++)
            {
                dwgObjects[i].Pan(XOffset, YOffset);
            }
        }

        /// <summary>
        /// Draws all the drawings in the layer.
        /// </summary>
        /// <param name="g">Graphic object on which the drawing is going to be done.</param>
        public void Draw(Graphics g)
        {
            for (int i = 0; i < dwgObjects.Count; i++)
            {
                dwgObjects[i].Draw(g);
            }
        }

        /// <summary>
        /// Firs the Modified event when ever called.
        /// </summary>
        private void OnModified()
        {
            if (Modified != null)
                Modified(this,new eLayerModifiedEventArgs(color, lineType, lineWeight, textStyle));
        }

        /// <summary>
        /// Moves this layer by the specified distance in both direction.
        /// </summary>
        /// <param name="XOffset">Distance ot be moved in X-direction.</param>
        /// <param name="YOffset">Distance to be moved in Y-direction.</param>
        public void Move(float XOffset, float YOffset)
        {
            this.Pan(XOffset, YOffset);
        }

        /// <summary>
        /// Adds a triangular member drawing to the layer. It returns the newly created object for further modification.
        /// </summary>
        /// <param name="load_Tri">The mechanics object associated with the drawing.</param>
        /// <param name="location">The far left intersection point of the member with the member.</param>
        /// <param name="dwgForm">The form on which the drawing is displayed.</param>
        public eGTriangularLoad AddTriangularLoad(eTriangularLoad load, eGMember membDWG, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGTriangularLoad(load, this, membDWG, dwgForm));
            return dwgObjects[dwgObjects.Count - 1] as eGTriangularLoad;
        }

        /// <summary>
        /// Adds a concentrated force member drawing to the layer. It returns the newly created object for further modification.
        /// </summary>
        /// <param name="member">The mechanics object connected to the drawing.</param>
        /// <param name="load_Tri">The mechanics object associated with the drawing.</param>
        /// <param name="location">The far left intersection point of the member with the member.</param>
        /// <param name="dwgForm">The form on which the drawing is displayed.</param>
        public eGConcentratedForce AddConcentratedForceLoad(eConcentratedForce load, eGMember membDWG, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGConcentratedForce(load, this, membDWG, dwgForm));
            return dwgObjects[dwgObjects.Count - 1] as eGConcentratedForce;
        }


        /// <summary>
        /// Adds a concentrated force member drawing to the layer. It returns the newly created object for further modification.
        /// </summary>
        /// <param name="member">The mechanics object connected to the drawing.</param>
        /// <param name="load_Tri">The mechanics object associated with the drawing.</param>
        /// <param name="location">The far left intersection point of the member with the member.</param>
        /// <param name="dwgForm">The form on which the drawing is displayed.</param>
        public eGConcentratedForce AddConcentratedForceLoad(eConcentratedForce load, eGJoint jointDWG, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGConcentratedForce(load, this, jointDWG, dwgForm));
            return dwgObjects[dwgObjects.Count - 1] as eGConcentratedForce;
        }

        /// <summary>
        /// Adds a concentrated moment member drawing to the layer. It returns the newly created object for further modification.
        /// </summary>
        /// <param name="load_Tri">The mechanics object associated with the drawing.</param>
        /// <param name="location">The far left intersection point of the member with the member.</param>
        /// <param name="member">The mechanics object connected to the drawing.</param>
        /// <param name="dwgForm">The form on which the drawing is displayed.</param>
        public eGConcentratedMoment AddConcentratedMomentLoad(eConcentratedMoment load, eGMember membDWG, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGConcentratedMoment(load, this, membDWG, dwgForm));
            return dwgObjects[dwgObjects.Count - 1] as eGConcentratedMoment;
        }

        /// <summary>
        /// Adds a concentrated moment member drawing to the layer. It returns the newly created object for further modification.
        /// </summary>
        /// <param name="load_Tri">The mechanics object associated with the drawing.</param>
        /// <param name="location">The far left intersection point of the member with the member.</param>
        /// <param name="member">The mechanics object connected to the drawing.</param>
        /// <param name="dwgForm">The form on which the drawing is displayed.</param>
        public eGConcentratedMoment AddConcentratedMomentLoad(eConcentratedMoment load, eGJoint jointDWG, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGConcentratedMoment(load, this, jointDWG, dwgForm));
            return dwgObjects[dwgObjects.Count - 1] as eGConcentratedMoment;
        }   

        /// <summary>
        /// Adds a trapezoidal load drawing to the layer
        /// </summary>
        public eGTrapezoidalLoad AddTrapezoidalLoad(eTriangularLoad load_Tri, eRectangularLoad load_Rect, eGMember membDWG, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGTrapezoidalLoad(load_Tri, load_Rect, this, membDWG, dwgForm));
            return dwgObjects[dwgObjects.Count - 1] as eGTrapezoidalLoad;
        }

        /// <summary>
        /// Adds a member drawing to the layer and returns the object for immediate modification.
        /// </summary>
        /// <param name="member">The mechanics object connected to the drawing.</param>
        /// <param name="location">The left end point of the member.</param>
        /// <param name="dwgForm">The form on which the drawing is displayed.</param>
        public eGMember AddMember(eGBeam beam, eAMember member, PointF start, PointF end, eLayer loadLayer, eLayer dimLayer, eModelForm dwgForm)
        {
            dwgObjects.Add(new eGMember(beam, member, start, end, this, loadLayer, dimLayer, dwgForm));
            return (dwgObjects[dwgObjects.Count - 1] as eGMember);
        }

        /// <summary>
        /// Adds a break line to the layer.
        /// </summary>
        /// <param name="point1">The first point of the break line.</param>
        /// <param name="point2">The second point of the break line.</param>
        public eBreakLine AddBreakLine(PointF point1, PointF point2)
        {
            this.dwgObjects.Add(new eBreakLine(this, point1, point2));
            return (eBreakLine)this.dwgObjects[dwgObjects.Count - 1];
        }

        public override string ToString()
        {
            return this.name;
        }

        /// <summary>
        /// Adds a curve to the layer.
        /// </summary>
        /// <param name="Points"> Array of System.Drawing.Point structures that represent the points that determine the curve.</param>       
        /// <returns>Returns the spline object added.</returns>
        public eCurve AddCurve(PointF[] Points)
        {
            this.dwgObjects.Add(new eCurve(Points, this));
            return dwgObjects[dwgObjects.Count - 1] as eCurve;
        }

        /// <summary>
        /// Adds a diagram to this layer.
        /// </summary>
        /// <param name="beam">The Beam for which the diagram is going to be added.</param>
        /// <param name="diagramType">The type of the diagram.</param>
        /// <param name="dwgForm">The Form where the drawing is done.</param>
        /// <returns></returns>
        public eDiagram AddDiagram(eGBeam beam, eDiagramType diagramType, eModelForm dwgForm)
        {
            this.dwgObjects.Add(new eDiagram(beam, diagramType, this, dwgForm));
            return dwgObjects[dwgObjects.Count - 1] as eDiagram;
        }
        #endregion

        /// <summary>
        /// Removes the given object from the layer if it exists.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        public void Remove(eIDrawing obj)
        {
            try
            {
                dwgObjects.Remove(obj);
            }
            catch { }
        }

        /// <summary>
        /// Fires the Rectangular Selection Event.
        /// </summary>
        /// <param name="e">The event argument at the occurence of the event.</param>
        internal void OnRectangularSelectionEnded(eRectangularSelectionEventArgs e)
        {
            if (this.RectangularSelectionEnded != null)
            {
                this.RectangularSelectionEnded(this, e);
            }
        }

        /// <summary>
        /// Fires the 'TurnedOn' event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        private void OnTurnedOn(eLayerModifiedEventArgs e)
        {
            if (this.TurnedOn != null)
            {
                this.TurnedOn(this, e);
            }
        }

        /// <summary>
        /// Fires the 'TurnedOff' event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        private void OnTurnedOff(eLayerModifiedEventArgs e)
        {
            if (this.TurnedOff != null)
            {
                this.TurnedOff(this, e);
            }
        }

        /// <summary>
        /// Adds any object that implements the eIDrawing interface
        /// </summary>
        /// <param name="obj">The dwg object</param>
        public void Add(eIDrawing obj)
        {
            this.dwgObjects.Add(obj);
        }

        #region Event

        /// <summary>
        /// Occurs when basic properties of the layer change.
        /// </summary>
        public event eLayerModifiedEventHandler Modified;

        #endregion

        /// <summary>
        /// Occurs when the user uses a rectangle to select a number of objects.
        /// </summary>
        public event eRectangularSelectorEventHandler RectangularSelectionEnded;

        /// <summary>
        /// Occurs when the layer is turned on.
        /// </summary>
        public event eLayerModifiedEventHandler TurnedOn;

        /// <summary>
        /// Occurs when a layer is turned off.
        /// </summary>
        public event eLayerModifiedEventHandler TurnedOff;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;

namespace ESADS.EGraphics.Beam
{
    public class eGFlexureSection : eIDrawing
    {
        /// <summary>
        /// The layer on which shear reinforcements are drawn.
        /// </summary>
        private eLayer layer_ShearBars;
        /// <summary>
        /// The layer on which labels related to shear are drawn.
        /// </summary>
        private eLayer layer_ShearLabels;
        /// <summary>
        /// The layer on which flesural bars are drawn.
        /// </summary>
        private eLayer layer_FlexureBars;
        /// <summary>
        /// The layer on which labels related to flexure are drawn.
        /// </summary>
        private eLayer layer_FlexureLabels;
        /// <summary>
        /// The layer on which general objs_texts are drawn.
        /// </summary>
        private eLayer layer_Text;
        /// <summary>
        /// The layer for dimension objects.
        /// </summary>
        private eLayer layer_Dimensions;
        /// <summary>
        /// objects inserted into shear bars layer.
        /// </summary>
        private List<eIDrawing> shearObjects;
        /// <summary>
        /// objects inserted into shear labels layer.
        /// </summary>
        private List<eIDrawing> shearLabels;
        /// <summary>
        /// objects inserted into flexure bars layer.
        /// </summary>
        private List<eIDrawing> flexureObjects;
        /// <summary>
        /// objects inserted into flexure labels layer.
        /// </summary>
        private List<eIDrawing> flexureLabelsObjects;
        /// <summary>
        /// objects inserted into objs_texts layer.
        /// </summary>
        private List<eIDrawing> texts;
        /// <summary>
        /// objects inserted into objs_texts layer.
        /// </summary>
        private List<eIDrawing> dimObjects;
        /// <summary>
        /// Holds the value of 'Section'.
        /// </summary>
        private eDFlexureSection section;
        /// <summary>
        /// Holds the value of 'Location'.
        /// </summary>
        private PointF location;
        /// <summary>
        /// Holds the value of 'Size'.
        /// </summary>
        private float size;
        /// <summary>
        /// Holds the value of 'DimensionShown'.
        /// </summary>
        private bool dimensionShown;
        /// <summary>
        /// layer on which the main beam drawing is drawn. Also holds the value for 'Layer'.
        /// </summary>
        private eLayer layer_Beam;
        /// <summary>
        /// Holds the value of 'Color'.
        /// </summary>
        private eColor color;
        /// <summary>
        /// objects inserted into the beam layer.
        /// </summary>
        private List<eIDrawing> beamLayerObjects;
        private eGBeam beam_Graphics;
        private float width;
        private float height;

        /// <summary>
        /// Represents a drawing for cross-section of a designed beam member.
        /// </summary>
        /// <param name="layers">The collection of layers to which the section can decide whether to add layers or use existing ones.</param>
        /// <param name="section">The flexure section for which the drawing is going to be drawn.</param>
        /// <param name="location">The location of the left most corner of the section.</param>
        public eGFlexureSection(eGBeam beam_Graphics, eLayers layers, eDFlexureSection section, PointF location, float size)
        {
            this.beam_Graphics = beam_Graphics;
            this.section = section;
            this.location = location;

            if (!layers.Contains("Beam"))
                layers.Add("Beam", System.Drawing.Color.Yellow, eLineTypes.Continuous, 1.5f, new Font("Arial", 12));
            if (!layers.Contains("ShearBars"))
                layers.Add("ShearBars", System.Drawing.Color.Cyan, eLineTypes.Continuous, 2.0f, new Font("Arial", 12));
            if (!layers.Contains("ShearLabels"))
                layers.Add("ShearLabels", System.Drawing.Color.Cyan, eLineTypes.Continuous, 1.0f, new Font("Arial", 12));
            if (!layers.Contains("FlexureBars"))
                layers.Add("FlexureBars", System.Drawing.Color.Magenta, eLineTypes.Continuous, 2.0f, new Font("Arial", 12));
            if (!layers.Contains("FlexureLabels"))
                layers.Add("FlexureLabels", System.Drawing.Color.Magenta, eLineTypes.Continuous, 1.0f, new Font("Arial", 15));
            if (!layers.Contains("Text"))
                layers.Add("Text", System.Drawing.Color.White, eLineTypes.Continuous, 1.0f, new Font("Arial", 22));
            if (!layers.Contains("Dimensions"))
                layers.Add("Dimensions", System.Drawing.Color.Green, eLineTypes.Continuous, 0.8f, new Font("Arial", 12));

            this.layer_Beam = layers["Beam"];
            this.layer_Dimensions = layers["Dimensions"];
            this.layer_FlexureBars = layers["FlexureBars"];
            this.layer_FlexureLabels = layers["FlexureLabels"];
            this.layer_ShearBars = layers["ShearBars"];
            this.layer_ShearLabels = layers["ShearLabels"];
            this.layer_Text = layers["Text"];

            beamLayerObjects = new List<eIDrawing>();
            dimObjects = new List<eIDrawing>();
            flexureObjects = new List<eIDrawing>();
            flexureLabelsObjects = new List<eIDrawing>();
            shearObjects = new List<eIDrawing>();
            shearLabels = new List<eIDrawing>();
            texts = new List<eIDrawing>();

            this.size = size;
            this.dimensionShown = true;

            this.GenerateDwgObjects();
        }
        /// <summary>
        /// Gets or sets the top left corner point of the section.
        /// </summary>
        public PointF Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
                GenerateDwgObjects();
            }
        }

        /// <summary>
        /// Gets or sets the layer on which the main beam section is drawn.
        /// </summary>
        public eLayer Layer
        {
            get { return this.layer_Beam; }
        }

        /// <summary>
        /// Gets or sets the color of the main object of the main beam object.
        /// </summary>
        public eColor Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Gets or sets a value which determines how large the section looks. It cannot be zero or negative.
        /// </summary>
        public float Size
        {
            get
            {
                return this.size;
            }
            set
            {
                if (value > 0)
                {
                    this.size = value;
                    this.GenerateDwgObjects();
                }
                else
                    throw new Exception("The size of flexure section cannot be zero or negative.");
            }
        }

        /// <summary>
        /// Gets or sets a value whether the dimension is shown or not.
        /// </summary>
        public bool DimensionShown
        {
            get
            {
                return this.dimensionShown;
            }
            set
            {
                this.dimensionShown = value;
            }
        }

        /// <summary>
        /// Gets or sets the associated flexure section which is represented by the drawing.
        /// </summary>
        public eDFlexureSection Section
        {
            get
            {
                return this.section;
            }
            set
            {
                this.section = value;
            }
        }

        /// <summary>
        /// Gets the total width of the graphics.
        /// </summary>
        public float Width
        {
            get
            {
                return this.width;
            }
        }

        /// <summary>
        /// Gets the total height of the graphics.
        /// </summary>
        public float Height
        {
            get
            {
                return this.height;
            }
        }

        public void Zoom(PointF ZoomCenter, float ZoomFactor)
        {
            //if the break line does not zoom properly, go to 'GenerateDwgObjects' method and avoid the addition of the drawing objects directly to the layer and do
            //the jobs of the layer here, i.e. zoom every dwg object in this method.
        }

        public void Pan(float Xoffset, float Yoffset)
        {
            //if the break line does not pan properly, go to 'GenerateDwgObjects' method and avoid the addition of the drawing objects directly to the layer and do
            //the jobs of the layer here, i.e. pan every dwg object in this method.
        }

        public void Draw(Graphics g)
        {
            //if the break line does not draw properly, go to 'GenerateDwgObjects' method and avoid the addition of the drawing objects directly to the layer and do
            //the jobs of the layer here, i.e. draw every dwg object in this method.
        }


        /// <summary>
        /// Generates all the necessary drawing objects to draw the section.
        /// </summary>
        private void GenerateDwgObjects()
        {
            //
            //Initialize collections and layers
            //
            //float size = this.size * layer_FlexureBars.ZoomFactor;

            foreach (var v in beamLayerObjects)
                layer_Beam.Remove(v);
            foreach (var v in shearObjects)
                layer_ShearBars.Remove(v);
            foreach (var v in shearLabels)
                layer_ShearLabels.Remove(v);
            foreach (var v in flexureObjects)
                layer_FlexureBars.Remove(v);
            foreach (var v in flexureLabelsObjects)
                layer_FlexureLabels.Remove(v);
            foreach (var v in dimObjects)
                layer_Dimensions.Remove(v);
            foreach (var v in texts)
                layer_Text.Remove(v);

            beamLayerObjects = new List<eIDrawing>();
            dimObjects = new List<eIDrawing>();
            flexureObjects = new List<eIDrawing>();
            flexureLabelsObjects = new List<eIDrawing>();
            shearObjects = new List<eIDrawing>();
            shearLabels = new List<eIDrawing>();
            texts = new List<eIDrawing>();
            //
            //Main beam rectangle
            //
            float b, h;
            b = (float)(size * section.Width);
            h = (float)(size * section.Depth);
            beamLayerObjects.Add(layer_Beam.AddRectangle(this.location, b, h));
            //
            //Stirrup bar
            //
            double cover = size * (section.Beam.Cover + eXBar.GetDiam(section.Beam.StirupBar));
            PointF loc = new PointF((float)(this.location.X + cover), (float)(this.location.Y + cover));
            shearObjects.Add(layer_ShearBars.AddRectangle(loc, (float)(b - 2 * cover), (float)(h - 2 * cover)));
            //
            //Dimension Lines
            //
            PointF left_b = new PointF(), left_t = new PointF(), right_b = new PointF();
            left_t = location;
            left_b.X = location.X;
            left_b.Y = location.Y + h;
            right_b.X = location.X + b;
            right_b.Y = left_b.Y;

            string depthTxt = Math.Round(eUtility.Convert(section.Depth, eUtility.SLU, beam_Graphics.Document.LengthUnit), 2).ToString();
            string widthTxt = Math.Round(eUtility.Convert(section.Width, eUtility.SLU, beam_Graphics.Document.LengthUnit), 2).ToString();

            dimObjects.Add(layer_Dimensions.AddDim(left_t, left_b, depthTxt, eDimensionType.LinearVertical, eDimensionLinePosition.LeftOrAbove, 25.0f * size));
            ((eDimension)dimObjects[dimObjects.Count - 1]).ArrowSize = 0.03f * b;
            ((eDimension)dimObjects[dimObjects.Count - 1]).TextStyle = new eTextStyle(new Font("Arial", 0.06f * b), eChangeBy.ByLayer);
            dimObjects.Add(layer_Dimensions.AddDim(left_b, right_b, widthTxt, eDimensionType.LinearHorizontal, eDimensionLinePosition.RightOrBottom, 25.0f * size));
            ((eDimension)dimObjects[dimObjects.Count - 1]).ArrowSize = 0.03f * b;
            ((eDimension)dimObjects[dimObjects.Count - 1]).TextStyle = new eTextStyle(new Font("Arial", 0.06f * b), eChangeBy.ByLayer);
            //
            //Section name
            //
            texts.Add(layer_Text.AddText("Section " + section.Name + "-" + section.Name, new PointF(location.X + b / 2.0f, location.Y + 1.5f * h)));
            (texts[texts.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.1f * b), eChangeBy.ByLayer);
            this.width = (texts[texts.Count - 1] as eText).Width;
            this.height = ((texts[texts.Count - 1] as eText).Location.Y + (texts[texts.Count - 1] as eText).Height) - location.Y;
            //
            //Longitudinal bars and their labels
            //
            if (section.Failed)
            {
                flexureObjects.Add(layer_FlexureBars.AddLine(location, new PointF(location.X + b, location.Y + h)));
                flexureObjects.Add(layer_FlexureBars.AddLine(new PointF(location.X + b, location.Y), new PointF(location.X, location.Y + h)));
                flexureObjects.Add(layer_FlexureBars.AddText(section.FailureNote, new PointF(location.X + b / 2.0f, location.Y + h / 2.0f)));
                (flexureObjects[flexureObjects.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.08f * b), eChangeBy.ByLayer);
                goto A;
            }
            if (section.IsOverReninforced)
            {
                flexureObjects.Add(layer_FlexureBars.AddLine(location, new PointF(location.X + b, location.Y + h)));
                flexureObjects.Add(layer_FlexureBars.AddLine(new PointF(location.X + b, location.Y), new PointF(location.X, location.Y + h)));
                flexureObjects.Add(layer_FlexureBars.AddText(section.FailureNote, new PointF(location.X + b / 2.0f, location.Y + h / 2.0f)));
                (flexureObjects[flexureObjects.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.08f * b), eChangeBy.ByLayer);
                goto A;
            }
            PointF TensCntr = new PointF(), CompCntr = new PointF(), lbl_1 = new PointF(), lbl_2 = new PointF();
            string lblTxt_1, lblTxt_2;
            float signFactor = 1.0f;
            int num1 = 0, num2 = 0;

            TensCntr.X = CompCntr.X = location.X + b / 2.0f;

            lbl_2.X = lbl_1.X = location.X + b * 1.4f;
            lbl_1.Y = location.Y + h * 0.3f;
            lbl_2.Y = location.Y + h * 0.65f;

            if (section.IsNegative)
            {
                signFactor = -1.0f;
                TensCntr.Y = (float)(location.Y + h);
                CompCntr.Y = (float)(location.Y);
            }
            else
            {
                TensCntr.Y = (float)(location.Y);
                CompCntr.Y = (float)(location.Y + h);
            }

            PointF p1 = new PointF(), p2 = new PointF();
            int i = 0;
            foreach (var row in section.TensileComb.Rows)
            {
                if (i > 0 && row.NumOfBar1 + row.NumOfBar2 > 2) //Adding secondary stirrups
                {
                    p1 = new PointF((float)(TensCntr.X - (b - 2 * cover) / 2), (float)(TensCntr.Y + signFactor * row.Common_Y * size));
                    p2 = new PointF((float)(TensCntr.X + (b - 2 * cover) / 2), (float)(TensCntr.Y + signFactor * row.Common_Y * size));

                    shearObjects.Add(layer_ShearBars.AddLine(p1, p2));
                }

                foreach (var bar in row.Bars)
                {
                    loc.X = (float)(TensCntr.X + bar.XCoord * size);
                    loc.Y = (float)(TensCntr.Y + signFactor * bar.YCoord * size);
                    flexureObjects.Add(layer_FlexureBars.AddCircle(loc, (float)(size * bar.Diameter / 2.0), eDrawType.Fill));

                    if (bar.Diameter == section.Bar1)
                    {
                        num1++;
                        flexureLabelsObjects.Add(layer_FlexureLabels.AddLine(loc, lbl_1));
                    }
                    else
                    {
                        num2++;
                        flexureLabelsObjects.Add(layer_FlexureLabels.AddLine(loc, lbl_2));
                    }
                }
                i++;
            }

            i = 0;
            foreach (var row in section.CompresionComb.Rows)
            {
                if (i > 0 && row.NumOfBar1 + row.NumOfBar2 > 2) //Adding secondary stirrups
                {
                    p1 = new PointF((float)(CompCntr.X - (b - 2 * cover) / 2), (float)(CompCntr.Y - signFactor * row.Common_Y * size));
                    p2 = new PointF((float)(CompCntr.X + (b - 2 * cover) / 2), (float)(CompCntr.Y - signFactor * row.Common_Y * size));

                    shearObjects.Add(layer_ShearBars.AddLine(p1, p2));
                }

                foreach (var bar in row.Bars)
                {
                    loc.X = (float)(CompCntr.X + bar.XCoord * size);
                    loc.Y = (float)(CompCntr.Y - signFactor * bar.YCoord * size);
                    flexureObjects.Add(layer_FlexureBars.AddCircle(loc, (float)(size * bar.Diameter / 2.0), eDrawType.Fill));

                    if (bar.Diameter == section.Bar1)
                    {
                        num1++;
                        flexureLabelsObjects.Add(layer_FlexureLabels.AddLine(loc, lbl_1));
                    }
                    else
                    {
                        num2++;
                        flexureLabelsObjects.Add(layer_FlexureLabels.AddLine(loc, lbl_2));
                    }
                }
                i++;
            }

            if (num1 != 0)
            {
                lblTxt_1 = num1.ToString() + "Φ" + ((int)eUtility.Convert(section.Bar1, eUtility.SLU, eLengthUnits.mm)).ToString();
                texts.Add(layer_Text.AddText(lblTxt_1, lbl_1));
                (texts[texts.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.08f * b), eChangeBy.ByLayer);
                texts[texts.Count - 1].Location = new PointF(texts[texts.Count - 1].Location.X + ((eText)texts[texts.Count - 1]).Width / 2.0f, texts[texts.Count - 1].Location.Y);

                this.width = (float)(((texts[texts.Count - 1] as eText).Width / 2.0 + (texts[texts.Count - 1] as eText).Location.X) - this.location.X);
            }

            if (num2 != 0)
            {
                lblTxt_2 = num2.ToString() + "Φ" + ((int)eUtility.Convert(section.Bar2, eUtility.SLU, eLengthUnits.mm)).ToString();
                texts.Add(layer_Text.AddText(lblTxt_2, lbl_2));
                (texts[texts.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.08f * b), eChangeBy.ByLayer);
                texts[texts.Count - 1].Location = new PointF(texts[texts.Count - 1].Location.X + ((eText)texts[texts.Count - 1]).Width / 2.0f, texts[texts.Count - 1].Location.Y);

                this.width = (float)((texts[texts.Count - 1] as eText).Width / 2.0 + (texts[texts.Count - 1] as eText).Location.X) - (this.location.X);
            }

        A:
            {
                //Place holder for the goto statement
            }
        }

        /// <summary>
        /// Releases all resources occupied by the object so that it can be destroyed by garbage collector.
        /// </summary>
        public void ReleaseResources()
        {
            foreach (var v in beamLayerObjects)
                layer_Beam.Remove(v);
            foreach (var v in shearObjects)
                layer_ShearBars.Remove(v);
            foreach (var v in shearLabels)
                layer_ShearLabels.Remove(v);
            foreach (var v in flexureObjects)
                layer_FlexureBars.Remove(v);
            foreach (var v in flexureLabelsObjects)
                layer_FlexureLabels.Remove(v);
            foreach (var v in dimObjects)
                layer_Dimensions.Remove(v);
            foreach (var v in texts)
                layer_Text.Remove(v);

            beamLayerObjects = null;
            dimObjects = null;
            flexureObjects = null;
            flexureLabelsObjects = null;
            shearObjects = null;
            shearLabels = null;
            texts = null;
        }
    }
}

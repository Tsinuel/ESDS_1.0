using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ESADS.Mechanics.Design;
using ESADS.Mechanics.Design.Beam;
using ESADS.Mechanics.Analysis;
using ESADS.Mechanics.Analysis.Beam;
using ESADS.Code;
using ESADS.Code.EBCS_1995;

namespace ESADS.EGraphics.Beam
{
    public class eBeamDetail
    {
        #region Fields
        /// <summary>
        /// layer on which the main beam drawing is drawn. Also holds the value for 'Layer'.
        /// </summary>
        private eLayer layer_Beam;
        /// <summary>
        /// The layer for dimension objects.
        /// </summary>
        private eLayer layer_Dimensions;
        /// <summary>
        /// The layer on which flesural bars are drawn.
        /// </summary>
        private eLayer layer_FlexureBars;
        /// <summary>
        /// The layer on which labels related to flexure are drawn.
        /// </summary>
        private eLayer layer_FlexureLabels;
        /// <summary>
        /// The layer on which shear reinforcements are drawn.
        /// </summary>
        private eLayer layer_ShearBars;
        /// <summary>
        /// The graphics of the beam to be detailed.
        /// </summary>
        private eGBeam beam_Graphics;
        /// <summary>
        /// The design object of the beam to be detailed.
        /// </summary>
        private eDBeam beam_Design;
        /// <summary>
        /// Holds the value of 'Location'.
        /// </summary>
        private PointF location;
        private eLayer layer_ShearLabels;
        private eLayer layer_Text;
        private List<eIDrawing> objs_beamLayer;
        private List<eIDrawing> objs_dim;
        private List<eIDrawing> objs_flexure;
        private List<eIDrawing> objs_flexureLabels;
        private List<eIDrawing> objs_shearBars;
        private List<eIDrawing> objs_shearLabels;
        private List<eIDrawing> objs_texts;
        private float size;
        private bool dimensionShown;
        private List<eGFlexureSection> flexureSections;
        private eLayers layers;
        private float overallWidth;
        private float overallHeght;
        #endregion

        #region Constructors
        /// <summary>
        /// Represents the detail drawing of a continuous beam.
        /// </summary>
        /// <param name="beam_Graphics">The graphics of the beam to be detailed.</param>
        /// <param name="layers">The collection of layers to which this object can make changes.</param>
        /// <param name="location">The location of the left most center point of the drawing.</param>
        public eBeamDetail(eGBeam beam_Graphics, eLayers layers, PointF location)
        {
            this.layers = layers;
            this.beam_Graphics = beam_Graphics;
            this.beam_Design = (beam_Graphics.Beam_Design as eDBeam);
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

            objs_beamLayer = new List<eIDrawing>();
            objs_dim = new List<eIDrawing>();
            objs_flexure = new List<eIDrawing>();
            objs_flexureLabels = new List<eIDrawing>();
            objs_shearBars = new List<eIDrawing>();
            objs_shearLabels = new List<eIDrawing>();
            objs_texts = new List<eIDrawing>();

            this.size = 1.0f;
            this.dimensionShown = true;

        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the location of the top left point of the first member.
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
        #endregion

        /// <summary>
        /// Gets the collection of flexure cross-sections.
        /// </summary>
        public List<eGFlexureSection> FlexureSections
        {
            get
            {
                return this.flexureSections;
            }
        }

        /// <summary>
        /// Gets the graphics of the beam object.
        /// </summary>
        public eGBeam Beam_Graphics
        {
            get
            {
                return this.beam_Graphics;
            }
        }

        #region Methods
        /// <summary>
        /// Generates all the necessary drawing objects for the beam.
        /// </summary>
        private void GenerateDwgObjects()
        {
            #region Prepare layers and collections
            Reset();
            #endregion

            #region Align with the analysis drawings
            this.overallWidth = 0.0f;
            this.overallHeght = 0.0f;

            this.location = new PointF(beam_Graphics.Location.X, beam_Graphics.Location.Y + beam_Graphics.MaxTotalNegOffset * 5.0f);
            this.size = (float)(beam_Graphics.GetOverallWidth() / beam_Graphics.Beam_Analysis.Length);
            #endregion

            #region The beam outline, longitudinal bar in the beam, sample stirrups

            float w1, w2, l, h, D, cover, max_y;
            PointF loc, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, pp5, pp8;
            eTextStyle txtStyle = new eTextStyle();

            loc = this.location;
            max_y = location.Y;
            h = (float)(size * beam_Graphics.LongestMemberLength * 0.1);
            w1 = (float)(beam_Graphics.Members[0].Member_Analysis.NEJoint.SupportWidth * size);
            D = (float)(beam_Graphics.Members[0].Member_Design.Depth * size);
            cover = (float)(beam_Graphics.Beam_Design.Cover * size);

            p9 = new PointF(loc.X - w1 / 2.0f + cover, loc.Y + cover);
            p11 = new PointF(p9.X, loc.Y + D - cover);
            objs_flexure.Add(layer_FlexureBars.AddLine(p9, p11));

            if (beam_Graphics.Members[0].Member_Analysis.NEJoint.Type != eJointType.Free)
            {
                pp5 = new PointF(loc.X - w1 / 2.0f, loc.Y - h);
                pp8 = new PointF(pp5.X, loc.Y + D + h);
                objs_beamLayer.Add(layer_Beam.AddLine(pp5, pp8));
            }
            else
            {
                pp5 = new PointF(loc.X - w1 / 2.0f, loc.Y);
                pp8 = new PointF(pp5.X, loc.Y + D);
                p2 = new PointF(loc.X + w1 / 2.0f, loc.Y);
                p3 = new PointF(loc.X + w1 / 2.0f, loc.Y + D);

                objs_beamLayer.Add(layer_Beam.AddLine(pp5, p2));
                objs_beamLayer.Add(layer_Beam.AddLine(pp5, pp8));
                objs_beamLayer.Add(layer_Beam.AddLine(pp8, p3));
            }

            foreach (var memb in beam_Graphics.Members)
            {
                w1 = (float)(memb.Member_Analysis.NEJoint.SupportWidth * size);
                w2 = (float)(memb.Member_Analysis.FEJoint.SupportWidth * size);
                D = (float)(memb.Member_Design.Depth * size);
                l = (float)(memb.Member_Analysis.Length * size);

                p1 = new PointF(loc.X + w1 / 2.0f, loc.Y - h);
                p2 = new PointF(p1.X, loc.Y);
                p3 = new PointF(p1.X, loc.Y + D);
                p4 = new PointF(p1.X, p3.Y + h);
                p5 = new PointF(loc.X + l - w2 / 2.0f, p1.Y);
                p6 = new PointF(p5.X, p2.Y);
                p7 = new PointF(p5.X, p3.Y);
                p8 = new PointF(p5.X, p4.Y);

                p9 = new PointF(p2.X - w1 + cover, p2.Y + cover);
                p10 = new PointF(p6.X + w2 - cover, p9.Y);
                p11 = new PointF(p9.X, p3.Y - cover);
                p12 = new PointF(p10.X, p11.Y);

                objs_flexure.Add(layer_FlexureBars.AddLine(p9, p10));
                objs_flexure.Add(layer_FlexureBars.AddLine(p11, p12));

                max_y = max_y < p8.Y ? p8.Y : max_y;
                p9.X = p11.X = p2.X;
                do
                {
                    objs_shearBars.Add(layer_ShearBars.AddLine(p9, p11));
                    p9.X = p11.X += (D - 2.0f * cover) * 0.7f;
                } while ((p9.X - p2.X) < (l / 4.0f));

                p9.X = p11.X = p6.X;
                do
                {
                    objs_shearBars.Add(layer_ShearBars.AddLine(p9, p11));
                    p9.X = p11.X -= (D - 2.0f * cover) * 0.7f;
                } while ((p6.X - p9.X) < (l / 4.0f));

                if (memb.Member_Analysis.NEJoint.Type != eJointType.Free)
                {
                    objs_beamLayer.Add(layer_Beam.AddBreakLine(pp5, p1));
                    objs_beamLayer.Add(layer_Beam.AddBreakLine(pp8, p4));
                    objs_beamLayer.Add(layer_Beam.AddLine(p1, p2));
                    objs_beamLayer.Add(layer_Beam.AddLine(p3, p4));
                }

                if (memb.Member_Analysis.FEJoint.Type != eJointType.Free)
                {
                    objs_beamLayer.Add(layer_Beam.AddLine(p6, p5));
                    objs_beamLayer.Add(layer_Beam.AddLine(p7, p8));
                }

                pp5 = p5;
                pp8 = p8;
                loc.X += l;

                objs_beamLayer.Add(layer_Beam.AddLine(p2, p6));
                objs_beamLayer.Add(layer_Beam.AddLine(p3, p7));
            }

            w2 = (float)(beam_Graphics.Members[beam_Graphics.Members.Count - 1].Member_Analysis.FEJoint.SupportWidth * size);
            D = (float)(beam_Graphics.Members[beam_Graphics.Members.Count - 1].Member_Design.Depth * size);

            p10 = new PointF(loc.X + w2 / 2.0f - cover, loc.Y + cover);
            p12 = new PointF(p10.X, p10.Y + D - 2.0f * cover);
            objs_flexure.Add(layer_FlexureBars.AddLine(p10, p12));

            if (beam_Graphics.Members[beam_Graphics.Members.Count - 1].Member_Analysis.FEJoint.Type != eJointType.Free)
            {
                p1 = new PointF(loc.X + w2 / 2.0f, pp5.Y);
                p4 = new PointF(p1.X, pp8.Y);
                objs_beamLayer.Add(layer_Beam.AddLine(p1, p4));
                objs_beamLayer.Add(layer_Beam.AddBreakLine(pp5, p1));
                objs_beamLayer.Add(layer_Beam.AddBreakLine(pp8, p4));
            }
            else
            {
                p6 = new PointF(loc.X - w2 / 2.0f, loc.Y);
                p7 = new PointF(loc.X - w2 / 2.0f, loc.Y + D);
                p2 = new PointF(loc.X + w2 / 2.0f, p6.Y);
                p3 = new PointF(loc.X + w2 / 2.0f, p7.Y);

                objs_beamLayer.Add(layer_Beam.AddLine(p6, p2));
                objs_beamLayer.Add(layer_Beam.AddLine(p2, p3));
                objs_beamLayer.Add(layer_Beam.AddLine(p3, p7));
            }
            #endregion

            #region Shear section distribution indicators

            string text;

            foreach (var v_sec in beam_Graphics.Beam_Design.ShearSections)
            {
                D = (float)(v_sec.Depth * size);
                foreach (var intrvl in v_sec.Intervals)
                {
                    p1 = new PointF((float)(location.X + intrvl[0] * size), location.Y + D / 2.0f);
                    p2 = new PointF((float)(location.X + intrvl[1] * size), location.Y + D / 2.0f);

                    if (!v_sec.BarConjested && !v_sec.FailedInDiagonalCompression && !v_sec.TransverseSpacingExceeded)
                        text = "st-" + v_sec.Name +" "+ beam_Graphics.Beam_Design.StirupBar.ToString() + " c/c=" + ((int)eUtility.Convert(v_sec.BarSpacing, eUtility.SLU, eLengthUnits.mm)).ToString() + "mm";
                    else
                        text = v_sec.FailureNote;

                    objs_shearLabels.Add(layer_ShearLabels.AddDim(p1, p2, text, eDimensionType.LinearHorizontal, eDimensionLinePosition.RightOrBottom, 0.0f));
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eDimension).TextStyle = new eTextStyle(new Font("Arial", D * 0.1f), eChangeBy.ByLayer);
                    (objs_shearLabels[objs_shearLabels.Count - 1] as eDimension).ArrowSize = 0.03f * D;
                }
            }
            #endregion

            #region Longitudinal Section Labels

            foreach (var flxsxn in beam_Design.PositiveFlexSxns)
            {
                D = (float)(flxsxn.Depth * size);
                foreach (var intrvl in flxsxn.Intervals)
                {
                    p1 = new PointF((float)(location.X + ((intrvl[0] + intrvl[1]) / 2.0f) * size), location.Y - D / 2.0f);
                    p2 = new PointF(p1.X + D / 2.0f, p1.Y);
                    p3 = new PointF(p1.X, p1.Y + 2.0f * D);
                    p4 = new PointF(p2.X, p3.Y);

                    objs_flexureLabels.Add(layer_FlexureLabels.AddLine(p1, p3));

                    objs_flexureLabels.Add(layer_FlexureLabels.AddLeader(eLeaderType.Straight, null, false, p1, p2));
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eLeader).SuppressDot = true;
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eLeader).ArrowAndDotSize = D / 8.0f;

                    objs_flexureLabels.Add(layer_FlexureLabels.AddLeader(eLeaderType.Straight, null, false, p3, p4));
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eLeader).SuppressDot = true;
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eLeader).ArrowAndDotSize = D / 8.0f;

                    objs_flexureLabels.Add(layer_FlexureLabels.AddText(flxsxn.Name, p2));
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", D * 0.1f), eChangeBy.ByLayer);
                    p1 = (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location;
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location = new PointF(p1.X + (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Width /
                        2.0f, p1.Y - (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Height / 2.0f);

                    objs_flexureLabels.Add(layer_FlexureLabels.AddText(flxsxn.Name, p4));
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", D * 0.1f), eChangeBy.ByLayer);
                    p1 = (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location;
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location = new PointF(p1.X + (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Width /
                        2.0f, p1.Y - (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Height / 2.0f);

                }
            }

            foreach (var flxsxn in beam_Design.NegativeFlexSxns)
            {
                D = (float)(flxsxn.Depth * size);
                foreach (var intrvl in flxsxn.Intervals)
                {
                    p1 = new PointF((float)(location.X + ((intrvl[0] + intrvl[1]) / 2.0f) * size), location.Y - D / 2.0f);
                    p2 = new PointF(p1.X + D / 2.0f, p1.Y);
                    p3 = new PointF(p1.X, p1.Y + 2.0f * D);
                    p4 = new PointF(p2.X, p3.Y);

                    objs_flexureLabels.Add(layer_FlexureLabels.AddLine(p1, p3));

                    objs_flexureLabels.Add(layer_FlexureLabels.AddLeader(eLeaderType.Straight, null, false, p1, p2));
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eLeader).SuppressDot = true;
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eLeader).ArrowAndDotSize = D / 8.0f;

                    objs_flexureLabels.Add(layer_FlexureLabels.AddLeader(eLeaderType.Straight, null, false, p3, p4));
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eLeader).SuppressDot = true;
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eLeader).ArrowAndDotSize = D / 8.0f;

                    objs_flexureLabels.Add(layer_FlexureLabels.AddText(flxsxn.Name, p2));
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", D * 0.1f), eChangeBy.ByLayer);
                    p1 = (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location;
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location = new PointF(p1.X + (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Width /
                        2.0f, p1.Y - (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Height / 2.0f);

                    objs_flexureLabels.Add(layer_FlexureLabels.AddText(flxsxn.Name, p4));
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", D * 0.1f), eChangeBy.ByLayer);
                    p1 = (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location;
                    (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location = new PointF(p1.X + (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Width /
                        2.0f, p1.Y - (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Height / 2.0f);
                }
            }

            #endregion

            #region Longitudinal Bar drawings

            D = (float)(beam_Design.DefaultSection.Depth * size);
            loc = new PointF(location.X, location.Y + 2 * D);

            foreach (var bar in beam_Design.LongitudinalBars)
            {

                p1 = new PointF((float)(loc.X + bar.Start * size), loc.Y + bar.Level * 0.5f * D);
                p2 = new PointF((float)(loc.X + bar.End * size), p1.Y);

                max_y = max_y < p1.Y ? p1.Y : max_y;
                text = bar.Name + " #" + bar.Number.ToString() + " Φ" + ((int)bar.Diameter).ToString() + " L=" + Math.Round(eUtility.Convert(bar.Length, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm";

                objs_flexure.Add(layer_FlexureBars.AddLine(p1, p2));
                objs_flexureLabels.Add(layer_FlexureLabels.AddText(text, new PointF((p1.X + p2.X) / 2.0f, p1.Y)));
                (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", D * 0.1f), eChangeBy.ByLayer);
                p1 = (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location;
                (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Location = new PointF(p1.X, p1.Y - (objs_flexureLabels[objs_flexureLabels.Count - 1] as eText).Height / 2.0f);
            }

            #endregion

            #region Generate the section details

            this.flexureSections = new List<eGFlexureSection>();

            loc = new PointF(location.X, max_y + h);
            float h_max = 0.0f;
            List<string> names = new List<string>();

            foreach (var s in beam_Design.PositiveFlexSxns)
            {
                if (!names.Contains(s.Name))
                {
                    flexureSections.Add(new eGFlexureSection(beam_Graphics, this.layers, s, loc, size));
                    loc.X += (flexureSections[flexureSections.Count - 1].Width * 2.0f);
                    names.Add(s.Name);
                    h_max = Math.Max(h_max, flexureSections[flexureSections.Count - 1].Height);
                }
            }

            foreach (var s in beam_Design.NegativeFlexSxns)
            {
                if (!names.Contains(s.Name))
                {
                    flexureSections.Add(new eGFlexureSection(beam_Graphics, this.layers, s, loc, size));
                    loc.X += (flexureSections[flexureSections.Count - 1].Width * 2.0f);
                    names.Add(s.Name);
                    h_max = Math.Max(h_max, flexureSections[flexureSections.Count - 1].Height);
                }
            }
            #endregion

            #region Generate stirrup drawings

            float l1, l2, l3, w;
            loc.X = location.X;
            loc.Y += h_max * 1.5f;

            p2 = p3 = p4 = p5 = p6 = p7 = new PointF();
            names = new List<string>();

            p1 = loc;
            foreach (var v_sec in beam_Design.ShearSections)
            {
                if (v_sec.FailedInDiagonalCompression || v_sec.BarConjested || v_sec.TransverseSpacingExceeded)
                    continue;
                foreach (var v_bar in v_sec.ShearBars)
                {
                    if (names.Contains(v_bar.Name))
                        continue;
                    names.Add(v_bar.Name);
                    if (v_bar.BarType == eShearBarTypes.EnclosingStirrup)
                    {
                        l1 = (float)(v_bar.Lengths[0] * size);
                        l2 = (float)(v_bar.Lengths[1] * size);
                        l3 = (float)(v_bar.Lengths[2] * size);

                        p2.X = p1.X;
                        p2.Y = p1.Y + l3;
                        p3.X = p2.X + l2;
                        p3.Y = p2.Y;
                        p4.X = p3.X;
                        p4.Y = p1.Y;
                        p5.X = (float)(p4.X - l1 * Math.Cos(Math.PI / 4));
                        p5.Y = (float)(p4.Y + l1 * Math.Sin(Math.PI / 4));
                        p6.X = (float)(p1.X + l2 * Math.Cos(Math.PI / 6));
                        p6.Y = (float)(p1.Y - l2 * Math.Sin(Math.PI / 6));
                        p7.X = (float)(p6.X - l1 * Math.Sin(Math.PI / 12));
                        p7.Y = (float)(p6.Y + l1 * Math.Cos(Math.PI / 12));

                        objs_shearBars.Add(layer_ShearBars.AddLine(p1, p2));
                        objs_shearBars.Add(layer_ShearBars.AddLine(p2, p3));
                        objs_shearBars.Add(layer_ShearBars.AddLine(p3, p4));
                        objs_shearBars.Add(layer_ShearBars.AddLine(p4, p5));
                        objs_shearBars.Add(layer_ShearBars.AddLine(p1, p6));
                        objs_shearBars.Add(layer_ShearBars.AddLine(p6, p7));

                        text = v_bar.Name + " Φ" + Math.Round(eUtility.Convert(v_bar.Diameter, eUtility.SLU, eLengthUnits.mm), 0).ToString() + " c/c=" +
                            Math.Round(eUtility.Convert(v_bar.Spacing, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm L=" +
                            Math.Round(eUtility.Convert(v_bar.Length, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm";
                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p2.X + p3.X) / 2.0f, p2.Y + l3 / 2.0f)));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.2f * l2), eChangeBy.ByLayer);
                        w = Math.Max((objs_shearLabels[objs_shearLabels.Count - 1] as eText).Width, l1);

                        txtStyle = new eTextStyle(new Font("Arial", 0.1f * l2), eChangeBy.ByLayer);

                        text = Math.Round(eUtility.Convert(v_bar.Lengths[2], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF(p1.X, (p1.Y + p2.Y) / 2.0f), 90));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        h = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Height;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - h / 2, p8.Y);

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF(p4.X, (p4.Y + p3.Y) / 2.0f), 90));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + h / 2, p8.Y);

                        text = Math.Round(eUtility.Convert(v_bar.Lengths[1], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p2.X + p3.X) / 2, p2.Y)));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X, p8.Y + h / 2);

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p6.X) / 2, (p1.Y + p6.Y) / 2), 30));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - h / 4, p8.Y - 0.866f * h / 2);

                        text = Math.Round(eUtility.Convert(v_bar.Lengths[0], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p7.X + p6.X) / 2, (p6.Y + p7.Y) / 2), 75));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + 0.9659f * h / 2, p8.Y + 0.2588f * h / 2);

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p5.X + p4.X) / 2, (p5.Y + p4.Y) / 2), 45));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - 0.707f * h / 2, p8.Y - 0.707f * h / 2);
                    }
                    else
                    {
                        l1 = (float)(v_bar.Lengths[0] * size);
                        l2 = (float)(v_bar.Lengths[1] * size);

                        p2.X = p1.X + l2;
                        p2.Y = p1.Y;
                        p3.X = (float)(p2.X - l1 * Math.Cos(Math.PI / 4));
                        if (v_bar.IsTop && beam_Design.StirrupPosn == eRelativeStirrupPosition.StirrupAtTop || !v_bar.IsTop && beam_Design.StirrupPosn == eRelativeStirrupPosition.StirrupAtBottom)
                            p3.Y = (float)(p2.Y - l1 * Math.Cos(Math.PI / 4));
                        else
                            p3.Y = (float)(p2.Y + l1 * Math.Cos(Math.PI / 4));
                        p4.X = (float)(p1.X + l1 * Math.Cos(Math.PI / 4));
                        p4.Y = p3.Y;

                        objs_shearBars.Add(layer_ShearBars.AddLine(p1, p2));
                        objs_shearBars.Add(layer_ShearBars.AddLine(p2, p3));
                        objs_shearBars.Add(layer_ShearBars.AddLine(p1, p4));

                        text = v_bar.Name + " Φ" + Math.Round(eUtility.Convert(v_bar.Diameter, eUtility.SLU, eLengthUnits.mm), 0).ToString() + " c/c=" +
                            Math.Round(eUtility.Convert(v_bar.Spacing, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm L=" +
                            Math.Round(eUtility.Convert(v_bar.Length, eUtility.SLU, eLengthUnits.mm), 0).ToString() + "mm";
                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p2.X) / 2.0f, p1.Y + l2 / 2.0f)));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = new eTextStyle(new Font("Arial", 0.2f * l2), eChangeBy.ByLayer);
                        w = Math.Max((objs_shearLabels[objs_shearLabels.Count - 1] as eText).Width, l1);

                        txtStyle = new eTextStyle(new Font("Arial", 0.1f * l2), eChangeBy.ByLayer);

                        text = Math.Round(eUtility.Convert(v_bar.Lengths[0], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p4.X) / 2, (p1.Y + p4.Y) / 2.0f), 45));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        h = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Height;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        if (v_bar.IsTop && beam_Design.StirrupPosn == eRelativeStirrupPosition.StirrupAtTop || !v_bar.IsTop && beam_Design.StirrupPosn == eRelativeStirrupPosition.StirrupAtBottom)
                            (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - 0.707f * h / 2, p8.Y + 0.707f * h / 2);
                        else
                            (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X - 0.707f * h / 2, p8.Y - 0.707f * h / 2);

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2), -45));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        if (v_bar.IsTop && beam_Design.StirrupPosn == eRelativeStirrupPosition.StirrupAtTop || !v_bar.IsTop && beam_Design.StirrupPosn == eRelativeStirrupPosition.StirrupAtBottom)
                            (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + 0.707f * h / 2, p8.Y - 0.707f * h / 2);
                        else
                            (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X + 0.707f * h / 2, p8.Y + 0.707f * h / 2);

                        text = Math.Round(eUtility.Convert(v_bar.Lengths[1], eUtility.SLU, eLengthUnits.mm), 0).ToString();

                        objs_shearLabels.Add(layer_ShearLabels.AddText(text, new PointF((p1.X + p2.X) / 2, p1.Y)));
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).TextStyle = txtStyle;
                        p8 = (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location;
                        (objs_shearLabels[objs_shearLabels.Count - 1] as eText).Location = new PointF(p8.X, p8.Y + 0.5f * h / 2);
                    }

                    p1.X += w * 1.2f;
                }
            }

            #endregion
        }

        private void Reset()
        {
            foreach (var v in objs_beamLayer)
                layer_Beam.Remove(v);
            foreach (var v in objs_dim)
                layer_Dimensions.Remove(v);
            foreach (var v in objs_flexure)
                layer_FlexureBars.Remove(v);
            foreach (var v in objs_flexureLabels)
                layer_FlexureLabels.Remove(v);
            foreach (var v in objs_shearBars)
                layer_ShearBars.Remove(v);
            foreach (var v in objs_shearLabels)
                layer_ShearLabels.Remove(v);
            foreach (var v in objs_texts)
                layer_Text.Remove(v);

            objs_beamLayer = new List<eIDrawing>();
            objs_dim = new List<eIDrawing>();
            objs_flexure = new List<eIDrawing>();
            objs_flexureLabels = new List<eIDrawing>();
            objs_shearBars = new List<eIDrawing>();
            objs_shearLabels = new List<eIDrawing>();
            objs_texts = new List<eIDrawing>();
        }

        internal void Destroy()
        {
            Reset();
            if (flexureSections == null)
                return;

            foreach (var v in flexureSections)
            {
                v.ReleaseResources();
            }

            flexureSections = null;

            layers.Remove("Beam");
            layers.Remove("ShearBars");
            layers.Remove("ShearLabels");
            layers.Remove("FlexureBars");
            layers.Remove("FlexureLabels");
            layers.Remove("Text");
        }

        internal void Generate()
        {
            this.GenerateDwgObjects();
        }
        #endregion
    }
}

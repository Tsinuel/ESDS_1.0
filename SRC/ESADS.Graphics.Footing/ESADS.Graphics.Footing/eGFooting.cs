using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using ESADS.EGraphics;
using ESADS.Mechanics.Design.Footing;
using ESADS.GUI;
using System.Windows.Forms;

namespace ESADS.EGraphics.Footing
{
    public class eGFooting:eIGModel
    {
        private eLayers layers;
        private  eDFooting f;
        private eFootingColumnConnection connection;
        private eLengthUnits lengthUnit;
        private eForceUints forceUnit;
        private int precision;
        private eFootingPlan plan;
        private eFootingSection sectn;
        private eFootingBar Lbar;
        private eFootingBar Bbar;
        private eModelForm dwgForm;
        private eDrawingStage dwgStage;
        private float gap;
        private double bowelsBentLength;
        public eDFooting Footing
        {
            get { return f; }
        }

        public double BowelsBentLength
        {
            get { return bowelsBentLength; }
            set { bowelsBentLength = value; }
        }

        public int Precision
        {
            get { return precision; }
            set { precision = value; }
        }

        public eGFooting(eModelForm dwgForm, eDFooting f)
        {
            this.dwgForm = dwgForm;
            this.f = f;
            this.lengthUnit = dwgForm.Document.LengthUnit;
            this.forceUnit = dwgForm.Document.ForceUnit;
            this.dwgForm.UnitChanged += new eUnitChangedEventHandler(dwgForm_UnitChanged);
            this.precision = 2;
            this.bowelsBentLength = 500;
            this.connection = eFootingColumnConnection.Fixed;
            InitializeComponents();
        }

        private void ReleaseHandlers()
        {
            this.dwgForm.UnitChanged -= dwgForm_UnitChanged;
            this.layers.ReleaseHandlers();
        }
        private void InitializeComponents()
        {

            if (layers != null)
                layers.ReleaseHandlers();
            this.layers = new eLayers(dwgForm);
            this.FillLayers();
            this.plan = new eFootingPlan(layers, f, lengthUnit, forceUnit);
            this.sectn = new eFootingSection(layers, f, lengthUnit, forceUnit);
            this.gap = (float)f.Width / 3;
            this.Lbar = new eFootingBar(f.LBar, layers[1],eBarAlignment.Horizontal, eBarLoaction.Alone);
            this.Bbar = new eFootingBar(f.BBar, layers[1], eBarAlignment.Horizontal, eBarLoaction.Alone);
            this.sectn.DowelsBentLength = bowelsBentLength;
            this.FootingColumnConnection = connection;
        }
        void dwgForm_UnitChanged(object sender, eUnitChangedEventArgs e)
        {
            sectn.ForceUnit = this.forceUnit = e.ForceUnit;
            Lbar.LengthUnit = Bbar.LengthUnit = plan.LengthUnit = this.lengthUnit = e.LengthUnit;
        }

        public void Design()
        {
            try
            {
                f.Design();
            }
            catch (eInsufficientDephtException)
            {
                MessageBox.Show("The provided depth is insufficient for shear. Pleas modify your input and try again!", "Insuficient Depth", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                return;
            }
            //catch (eInSufficientAnchorageLengthException)
            //{
            //    MessageBox.Show("The available anchoroge length is insufficient. Pleas modify your input and try again!", "Insuficient Anchorage", MessageBoxButtons.OKCancel,MessageBoxIcon.Stop);
            //    return;
            //}
            catch (eNoBarBetweenSpacingLimitException)
            {
                MessageBox.Show("There is no bar which can satisfiy the minimum and maximum spacing limimt.\n Pleas modify your bar preferecne and try again!", "No Between Spacing Limit Exist!", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                return;
            }
            catch (eReinfCongestedException)
            {
                MessageBox.Show("Reinforcement conjusted. Pleas modify your bar preferecne and try again!", "Reinforcement Conjustion", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                return;
            }
            GenerateDetailDrawing();
        }

        public void DrawModel()
        {
            dwgStage = eDrawingStage.ModelingStage;
            InitializeComponents();
            Regenerate();
            plan.DrawModelingStage();
            sectn.DrawModelingStage();
            plan.Move(0, 0 - plan.ContRect.Location.Y);
            sectn.Move(0, 0 - sectn.ContRect.Location.Y);
            sectn.Move(0, plan.ContRect.Height + gap);
            ZoomFit();
        }

        public eLayers Layers
        {
            get { return layers; }
        }

        public eFootingColumnConnection FootingColumnConnection
        {
            get { return connection; }
            set { connection = value; }
        }

        private void FillLayers()
        {
            layers.Add("Footing", Color.Red, eLineTypes.Continuous, 3.0f);
            layers.Add("Bars", Color.Magenta, eLineTypes.Continuous, 3.0f, new Font("Arial", (float)f.Width / 40));
            layers.Add("Dimension", Color.Cyan, eLineTypes.Continuous, 1.0f, new Font("Arial", (float)f.Length / 20));
            layers.Add("Texts", Color.White, eLineTypes.Continuous, 3.0f, new Font("Arial", (float)f.Length / 20));
            layers.Add("Accessories", Color.Yellow, eLineTypes.Continuous, 3.0f, new Font("Arial", (float)f.Length / 30));
        }
        public void ZoomFit()
        {
            float xFactor, yFactor;
            SizeF s;
            if (dwgStage == eDrawingStage.ModelingStage)
                s = new SizeF(plan.ContRect.Width, plan.ContRect.Height + sectn.ContRect.Height + gap);
            else
                s = new SizeF(plan.ContRect.Width + Bbar.ContRect.Width + gap, plan.ContRect.Height + gap + sectn.ContRect.Height + gap + Lbar.ContRect.Height);
            xFactor = dwgForm.ClientSize.Width / s.Width;
            yFactor = dwgForm.ClientSize.Height / s.Height;
            layers.Pan(0 - plan.ContRect.Location.X, 0 - plan.ContRect.Location.Y);
            layers.Zoom(new PointF(0, 0), Math.Min(0.8f * xFactor, 0.8f * yFactor));
            layers.Pan(dwgForm.ClientSize.Width / 2 - Math.Min(0.8f * xFactor, 0.8f * yFactor) * s.Width / 2, 0.1f * yFactor * s.Height);
            layers.Origin = new eAxisIcon(dwgForm);
        }

        private void Regenerate()
        {
            layers.ResetLayers();
            plan.RemoveDiagram();
            sectn.RemoveDiagram();
            try
            {
                Lbar.RemoveDiagram();
                Bbar.RemoveDwgs();
            }
            catch { }
        }

        private void GenerateDetailDrawing()
        {
            
            dwgStage = eDrawingStage.ModelingStage;            
            InitializeComponents();
            Lbar.AddDrawings();
            Bbar.AddDrawings();
            plan.DrawDetailingStage();
            sectn.DrawDetailingStage();
            plan.Move(0, 0 - plan.ContRect.Location.Y);
            sectn.Move(0, plan.ContRect.BottomLeft.Y + gap);
            Lbar.Move(f.Cover, sectn.ContRect.BottomLeft.Y + gap + Math.Abs(Lbar.ContRect.Height));
            Bbar.Move(sectn.ContRect.BottomRight.X + gap, sectn.ContRect.BottomLeft.Y);
            ZoomFit();
        }
    }
}

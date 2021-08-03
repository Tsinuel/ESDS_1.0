using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ESADS;
using ESADS.GUI;

namespace ESADS.EGraphics
{
    /// <summary>
    /// A collection containing all the layers and necessary methods conserning graphics generally.
    /// </summary>
    [Serializable]
    public class eLayers
    {
        #region Feilds

        /// <summary>
        /// Holds a value for public property 'Origin'.
        /// </summary>
        private eAxisIcon origin;
        /// <summary>
        /// Holds a value for maximum zoom extent.
        /// </summary>
        private const float maxZoomFactor = 500.00f;
        /// <summary>
        /// Holds the minimum zoom extent.
        /// </summary>
        private const float minZoomFactor = 0.01f;
        /// <summary>
        /// Holds the current zoom factor for all turned On layers.
        /// </summary>
        private float MasterZoomFactor = 1;
        /// <summary>
        /// Contains all lists of layers.
        /// </summary>
        private List<eLayer> layers = new List<eLayer>();
        /// <summary>
        /// Holds a value for public property 'IsPan'.
        /// </summary>
        private bool isPan;
        /// <summary>
        /// Represents the start of the mouse in the drawing area in dawing coordinated.
        /// </summary>
        private PointF mouseLocation;
        /// <summary>
        /// Holds a value for public property 'UnitSystem'.
        /// </summary>
        [NonSerialized]
        private Form dwgForm;
        /// <summary>
        /// Holds the value the 'Scale' property.
        /// </summary>
        private float scale;
        /// <summary>
        /// Represents the selection rectangle;
        /// </summary>
        private eSelectionRectangle selectionRectangle;
        /// <summary>
        /// The main point that keeps layers aligned when turned back on.
        /// </summary>
        private PointF masterPinPoint;
        #endregion

        #region Constructors
        /// <summary>
        /// Gets the layer on which this line objct is drawn.
        /// </summary>
        /// <param name="dwgForm"> The form on which drawing is done.</param>
        public eLayers(Form dwgForm)
        {
            this.dwgForm = dwgForm;
            this.origin = new eAxisIcon(dwgForm);
            this.selectionRectangle = new eSelectionRectangle(dwgForm);
            this.selectionRectangle.EndSelection += new eRectangularSelectorEventHandler(selectionRectangle_EndSelection);

            dwgForm.Paint += new PaintEventHandler(dwgForm_Paint);
            dwgForm.MouseMove += new MouseEventHandler(dwgForm_MouseMove);
            dwgForm.MouseDown += new MouseEventHandler(dwgForm_MouseDown);
            dwgForm.MouseWheel += new MouseEventHandler(dwgForm_MouseWheel);
        }

        private void selectionRectangle_EndSelection(object sender, eRectangularSelectionEventArgs e)
        {
            foreach (eLayer lay in this.layers)
            {
                lay.OnRectangularSelectionEnded(e);
            }
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the drawing form where all the drawing take place.
        /// </summary>
        public Form DwgForm
        {
            get { return dwgForm; }
        }

        /// <summary>
        /// Gets or sets the value indicating wheither pan state is on or off.
        /// </summary>
        public bool IsPan
        {
            get { return isPan; }
            set { isPan = value; }
        }

        /// <summary>
        /// Gets or sets the the layer at specified index.
        /// </summary>
        /// <param name="index">The index of the layer in the collection.</param>
        /// <returns></returns>
        public eLayer this[int index]
        {
            get { return layers[index]; }
            set { layers[index] = value; }
        }

        /// <summary>
        /// Gets or sets the layer with the specified name.
        /// </summary>
        /// <param name="name">Name of the layer</param>
        /// <returns></returns>
        public eLayer this[string name]
        {
            get { return layers[GetLayerIndex(name)]; }
            set { layers[GetLayerIndex(name)] = value; }
        }

        /// <summary>
        /// Gets the number layers contained in this layer collection.
        /// </summary>
        public int Count
        {
            get { return layers.Count; }
        }


        /// <summary>
        /// Gets or sets the origin of the user coordinated relative to initial user coordinate system.
        /// </summary>
        public eAxisIcon Origin
        {
            get { return origin; }
            set { origin.Location = value; }
        }

        #endregion

        #region Methods

        public void ReleaseHandlers()
        {
            dwgForm.Paint -= dwgForm_Paint;
            dwgForm.MouseMove -= dwgForm_MouseMove;
            dwgForm.MouseDown -= dwgForm_MouseDown;
            dwgForm.MouseWheel -= dwgForm_MouseWheel;
            this.selectionRectangle.EndSelection -= selectionRectangle_EndSelection;
            if(layers!=null)
                for (int i = 0; i < layers.Count; i++)
                {
                    layers[i].TurnedOff -= this.eLayers_TurnedOff;
                    layers[i].TurnedOn -= this.eLayers_TurnedOn;
                }
        }
        /// <summary>
        /// Gets or sets the scale of the drawing at the current zoom state.
        /// </summary>
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        /// <summary>
        /// Removes all drawing in each layers.
        /// </summary>
        public void ResetLayers()
        {
            for (int i = 0; i < Count; i++)
            {
                layers[i].RemoveObjects();
            }
            MasterZoomFactor = 1;
        }

        /// <summary>
        /// Adds a  layer to the collection.
        /// </summary>
        public void Add(eLayer layer)
        {
            //Checks if a layer doesn't exis with similar name.
            if (!CheckSimilarLayer(layer.Name))
            {
                //If the layer doesn't exist  adds a new layer.
                layers.Add(layer);
                layers[layers.Count - 1].TurnedOn += new eLayerModifiedEventHandler(eLayers_TurnedOn);
                layers[layers.Count - 1].TurnedOff += new eLayerModifiedEventHandler(eLayers_TurnedOff);
            }
            else
            {
                throw new Exception(" A layer with similar name already exist. Try to change the name.");
            }
        }

        private void eLayers_TurnedOn(eLayer sender, eLayerModifiedEventArgs e)
        {
            eLayer lay = sender as eLayer;

            lay.Pan(masterPinPoint.X - e.PinPoint.X, masterPinPoint.Y - e.PinPoint.Y);
            lay.Zoom(masterPinPoint, MasterZoomFactor / e.ZoomFactor);
            dwgForm.Invalidate();
        }

        /// <summary>
        /// Adds a layer in the collection with the specified properties.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <param name="color">Color of the layer.</param>
        /// <param name="lineType">Color Type of the layer.</param>
        /// <param name="lineWeight">Color Weight of the layer.</param>
        /// <param name="font">Represents the text_left style.</param>
        public void Add(string name, Color color, eLineTypes lineType, float lineWeight, Font font)
        {
            //Checks if a layer doesn't exis with similar name.
            if (!CheckSimilarLayer(name))
            {
                //If the layer doesn't exist  adds new layer.
                layers.Add(new eLayer(name, color, lineType, lineWeight, font, this.MasterZoomFactor));
                layers[layers.Count - 1].TurnedOn += new eLayerModifiedEventHandler(eLayers_TurnedOn);
                layers[layers.Count - 1].TurnedOff += new eLayerModifiedEventHandler(eLayers_TurnedOff);
            }
            else
            {
                throw new Exception(" A layer with similar name already exist. Try to change the name.");
            }
        }

        /// <summary>
        /// Checks if the layer with the specified name exists in this layers collection.
        /// </summary>
        /// <param name="Name">Name of the layer.</param>
        /// <returns></returns>
        public bool Contains(string Name)
        {
            foreach (eLayer l in layers)
                if (l.Name == Name)
                    return true;
            return false;
        }

        /// <summary>
        /// Adds a layer in the collection with the specified properties.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <param name="color">Color of the layer.</param>
        /// <param name="lineType">Color Type of the layer.</param>
        /// <param name="lineWeight">Color Weight of the layer.</param>
        public void Add(string name, Color color, eLineTypes lineType, float lineWeight)
        {
            //Checks if a layer doesn't exis with similar name.
            if (!CheckSimilarLayer(name))
            {
                //If the layer doesn't exist  adds new layer.
                layers.Add(new eLayer(name, color, lineType, lineWeight, new Font("Arial", 10.0f), this.MasterZoomFactor));
                layers[layers.Count - 1].TurnedOn += new eLayerModifiedEventHandler(eLayers_TurnedOn);
                layers[layers.Count - 1].TurnedOff += new eLayerModifiedEventHandler(eLayers_TurnedOff);
            }
            else
            {
                throw new Exception(" A layer with similar name already exist. Try to change the name.");
            }
        }

        /// <summary>
        /// Adds a layer with the specified color with unit line weight,Continious line Type and "Arail" Font
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        public void Add(string name, Color color)
        {
            if (!CheckSimilarLayer(name))
            {
                layers.Add(new eLayer(name, color, this.MasterZoomFactor));
                layers[layers.Count - 1].TurnedOn += new eLayerModifiedEventHandler(eLayers_TurnedOn);
                layers[layers.Count - 1].TurnedOff += new eLayerModifiedEventHandler(eLayers_TurnedOff);
            }
            else
            {
                throw new Exception("Duplicate layer name.");
            }
        }

        private void eLayers_TurnedOff(eLayer sender, eLayerModifiedEventArgs e)
        {
            dwgForm.Invalidate();
        }

        /// <summary>
        /// Removes the the layer with the specified name.
        /// </summary>
        /// <param name="name"> Name of the layer.</param>
        public void Remove(string name)
        {
            layers.RemoveAt(GetLayerIndex(name));
        }

        /// <summary>
        /// Removes a layer at specified index.
        /// </summary>
        /// <param name="index">The index of the layer to be removed.</param>
        public void Remove(int index)
        {
            layers.RemoveAt(index);
        }

        /// <summary>
        /// Event handler fro MouseWheel event of the drawing Form.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information related to the event.</param>
        private void dwgForm_MouseWheel(object sender, MouseEventArgs e)
        {
            bool zoom = false;// indicates a value whether zooming is done or not.
            float factor = 1;
            // Checks if the case is zoom in.
            if ((e.Delta > 0) && (MasterZoomFactor < maxZoomFactor))
            {
                factor = e.Delta / 100.00f;
                zoom = true;
            }
            // Checks if the case is zoom out
            else if ((e.Delta < 0) && (MasterZoomFactor > minZoomFactor))
            {
                factor = -100.00f / e.Delta;
                zoom = true;
            }
            if (zoom)
                Zoom(e.Location, factor);
        }

        /// <summary>
        /// Event handler fro MouseDown event of the drawing Form.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information related to the event.</param>
        private void dwgForm_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    //turns the selection mode on.
            //    showSelectionRec = true;
            //    selnRectOppCrnr = e.location;
            //}
        }

        /// <summary>
        /// Event handler fro MouseMove event of the drawing Form.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information related to the event.</param>
        private void dwgForm_MouseMove(object sender, MouseEventArgs e)
        {
            //Checks if the mouse action is pan.
            if ((e.Button == MouseButtons.Middle) || ((e.Button == MouseButtons.Left) && (isPan)))
            {
                Pan(e.X - mouseLocation.X, e.Y - mouseLocation.Y);
                dwgForm.Invalidate();
            }
            mouseLocation = e.Location;
        }

        /// <summary>
        /// Event hadler for paint event of the drawing Form.
        /// </summary>
        /// <param name="sender">Object sending this event.</param>
        /// <param name="e">Event argument containing information related to the event.</param>
        private void dwgForm_Paint(object sender, PaintEventArgs e)
        {
            // e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            for (int i = 0; i < layers.Count; i++)
            {
                //draws all the layer which are on.
                if (layers[i].LayerOn)
                    try
                    {
                        layers[i].Draw(e.Graphics);
                    }
                    catch (Exception)
                    {
                    }
            }
            //Draws the universal coordinate system icon.
            origin.Draw(e.Graphics);
            selectionRectangle.Draw(e.Graphics);
        }

        /// <summary>
        /// Returns the index o the layer with the specified name.
        /// </summary>
        /// <param name="name">Name of the layer.</param>
        /// <returns></returns>
        private int GetLayerIndex(string name)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Name == name)
                {
                    return i;
                }
            }
            throw new Exception("The specified layer not found.\nCheck if the name of the layer is correct or the collection is not empty");
        }

        /// <summary>
        /// Checks if there is no layer having similar name with new layer to be added. Returns true if layer exist and false otherwise.
        /// </summary>
        /// <param name="name">Name of the layer for which duplication is going to be checked.</param>
        /// <returns></returns>
        private bool CheckSimilarLayer(string name)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Name == name)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Moves all the drawing int the layer by the specified offsets in both axises.
        /// </summary>
        /// <param name="XOffset">X-offset to be moved.</param>
        /// <param name="YOffset">Y-offset to be moved.</param>
        public void Move(float XOffset, float YOffset)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Move(XOffset, YOffset);
            }
        }

        /// <summary>
        /// Removes all the layers in this collection.
        /// </summary>
        public void RemoveAll()
        {
            layers = new List<eLayer>();
            MasterZoomFactor = 1;
        }

        /// <summary>
        /// Resets the zoom extenst of all the drawing in this layer collection;
        /// </summary>
        internal void ResetZoomFactor()
        {
            MasterZoomFactor = 1;
        }

        /// <summary>
        /// Checks if the layer with the specified name exists in this layers collection.
        /// </summary>
        /// <param name="Name">Name of the layer.</param>
        /// <returns></returns>
        public bool Contain(string Name)
        {
            foreach (eLayer l in layers)
                if (l.Name == Name)
                    return true;
            return false;
        }
        /// <summary>
        /// Zooms all the layer in this collection.
        /// </summary>
        /// <param name="zoomCenter">Point from where zooming is done.</param>
        /// <param name="zoomFactor">The scale by which the drawing is enlarged.</param>
        public void Zoom(PointF zoomCenter, float zoomFactor)
        {
            //Zooms all the layers.
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Zoom(zoomCenter, zoomFactor);
            }

            origin.Zoom(zoomCenter, zoomFactor); //Zoom the drawing origin.
            selectionRectangle.Zoom(zoomCenter, MasterZoomFactor);
            dwgForm.Invalidate();//Invalidates the form to apply the new changes.
            masterPinPoint.X = (masterPinPoint.X - zoomCenter.X) * zoomFactor + zoomCenter.X;
            masterPinPoint.Y = (masterPinPoint.Y - zoomCenter.Y) * zoomFactor + zoomCenter.Y;
            MasterZoomFactor *= zoomFactor; //Zoom factor is multiplied each time.
            scale *= zoomFactor;
        }

        /// <summary>
        /// Pans all the layers in this collection.
        /// </summary>
        /// <param name="xOffset">X- distance by which the layers are moved.</param>
        /// <param name="yOffset">Y- distance by which the layers are moved.</param>
        public void Pan(float xOffset, float yOffset)
        {
            //dwgForm.Cursor = System.Windows.Forms.Cursors
            for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Pan(xOffset, yOffset);
            }
            origin.Pan(xOffset, yOffset);
            selectionRectangle.Pan(xOffset, yOffset);
            masterPinPoint.X += xOffset;
            masterPinPoint.Y += yOffset;
        }
        #endregion

    }
}

namespace Sparkle.Engine.Core.Components
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A component that represents a general transformation of an entity.
    /// </summary>
    public class Transform
    {
        public Transform()
        {
            this.children = new List<Transform>();
            this.Scale = new Vector3(1, 1, 1);
            this.Color = new Color(255, 255, 255, 255);
        }

        #region Fields

        private Vector3 position;

        private Vector3 scale;

        private float rotation;

        private Color color;

        private Vector3 localPosition;

        private Vector3 localScale;

        private float localRotation;

        private Color localColor;

        private Transform parent;

        public List<Transform> children;

        // TODO : improve with bit flags instead of bool

        private bool hasPositionChanged; 

        private bool hasRotationChanged;

        private bool hasScaleChanged;

        private bool hasColorChanged;

        #endregion

        #region Children

        /// <summary>
        /// Gets all the children transforms that depends on this instance.
        /// </summary>
        public Transform[] Children { get { return this.children.ToArray(); } }

        /// <summary>
        /// Gets or sets the parent of this transform.
        /// </summary>
        public Transform Parent
        {
            get { return parent; }
            set
            {
                if(parent != value)
                {
                    if (parent != null)
                    {
                        parent.children.Remove(this);
                    }

                    parent = value;

                    if(parent != null)
                    {
                        parent.children.Add(this);
                        this.HasPositionChanged = true;
                    }
                }
            }
        }

        /// <summary>
        /// Forces the transform to recalculate its absolute position when local or parent's position changes.
        /// </summary>
        protected bool HasPositionChanged
        {
            get { return hasPositionChanged; }
            set
            {
                hasPositionChanged = value;
                foreach (var child in children)
                {
                    child.HasPositionChanged = true;
                }
            }
        }

        /// <summary>
        /// Forces the transform to recalculate its absolute rotation when local or parent's position changes.
        /// </summary>
        protected bool HasRotationChanged
        {
            get { return hasRotationChanged; }
            set
            {
                hasRotationChanged = value;
                foreach (var child in children)
                {
                    // Child position also depends on this rotation
                    child.HasRotationChanged = true;
                    child.HasPositionChanged = true;
                }
            }
        }

        /// <summary>
        /// Forces the transform to recalculate its absolute scale when local or parent's position changes.
        /// </summary>
        protected bool HasScaleChanged
        {
            get { return hasScaleChanged; }
            set
            {
                hasScaleChanged = value;
                foreach (var child in children)
                {
                    child.HasScaleChanged = true;
                }
            }
        }

        /// <summary>
        /// Forces the transform to recalculate its absolute color when local or parent's position changes.
        /// </summary>
        protected bool HasColorChanged
        {
            get { return hasColorChanged; }
            set
            {
                hasColorChanged = value;
                foreach (var child in children)
                {
                    child.HasColorChanged = true;
                }
            }
        }

        /// <summary>
        /// Adds a child transform.
        /// </summary>
        /// <param name="child">The child to add</param>
        /// <returns>The updated given child.</returns>
        public Transform AddChild(Transform child)
        {
            child.parent = this;
            return child;
        }

        /// <summary>
        /// Removes a child transform.
        /// </summary>
        /// <param name="child">The child to remove</param>
        /// <returns>The updated given child.</returns>
        public Transform RemoveChild(Transform child)
        {
            child.parent = null;
            return child;
        }

        #endregion
        
        /// <summary>
        /// Gets or sets relative position from instance's parent.
        /// </summary>
        public Vector3 LocalPosition
        {
            get { return localPosition; }
            set 
            { 
                if(localPosition != value)
                {
                    localPosition = value;
                    this.HasPositionChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets relative scale from instance's parent.
        /// </summary>
        public Vector3 LocalScale
        {
            get { return localScale; }
            set
            {
                if (localScale != value)
                {
                    localScale = value;
                    this.HasScaleChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets relative rotation from instance's parent.
        /// </summary>
        public float LocalRotation
        {
            get { return localRotation; }
            set
            {
                if (localRotation != value)
                {
                    localRotation = value;
                    this.HasRotationChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets relative color from instance's parent.
        /// </summary>
        public Color LocalColor
        {
            get { return localColor; }
            set
            {
                if (localColor != value)
                {
                    localColor = value;
                    this.HasColorChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets absolute position.
        /// </summary>
        public Vector3 Position
        {
            get 
            {
                if (this.HasPositionChanged)
                    this.RecalculatePosition();

                return position; 
            }
            set
            {
                if(position != value)
                {
                    position = value; 
                    this.HasPositionChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets absolute scale.
        /// </summary>
        public Vector3 Scale
        {
            get 
            {
                if (this.HasScaleChanged)
                    this.RecalculateScale();

                return scale; 
            }
            set 
            {
                if (scale != value)
                {
                    scale = value;
                    this.HasScaleChanged = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets absolute rotation.
        /// </summary>
        public float Rotation
        {
            get
            {
                if (this.HasRotationChanged)
                    this.RecalculateRotation();

                return rotation; 
            }
            set 
            { 
                if (rotation != value)
                {
                    rotation = value;
                    this.HasRotationChanged = true;
                }
            }
        }

        public Color Color
        {
            get
            {
                if (this.HasColorChanged)
                    this.RecalculateColor();

                return color;
            }
            set
            {
                if (color != value)
                {
                    color = value;
                    this.HasColorChanged = true;
                }
            }
        }
        
        /// <summary>
        /// Recalculates the absolute positionfrom local and parents values if needed.
        /// </summary>
        protected void RecalculatePosition()
        {
            if (this.Parent == null || !this.HasPositionChanged)
                return;

            // 1. Parent values always updated on these calls

            this.position = this.Parent.Position;
            this.rotation = this.Parent.Rotation;

            // 2. Updates position

            var cos = Math.Cos(rotation);
            var sin = Math.Sin(rotation);

            this.position.X += (float)(this.LocalPosition.X * cos - this.LocalPosition.Y * sin);
            this.position.Y += (float)(this.LocalPosition.Y * sin + this.LocalPosition.X * cos);
            this.position.Z += this.LocalPosition.Z;

            this.HasPositionChanged = false;
        }

        /// <summary>
        /// Recalculates the absolute rotation from local and parents values if needed.
        /// </summary>
        protected void RecalculateRotation()
        {
            if (this.Parent == null || !this.HasRotationChanged)
                return;

            // 1. Parent values always updated on these calls

            this.rotation = this.Parent.Rotation;

            // 2. Updates rotation

            this.rotation += this.LocalRotation;

            this.HasRotationChanged = false;
        }

        /// <summary>
        /// Recalculates the absolute scale from local and parents values if needed.
        /// </summary>
        protected void RecalculateScale()
        {
            if (this.Parent == null || !this.HasScaleChanged)
                return;

            // 1. Parent values always updated on these calls

            this.scale = this.Parent.Scale;

            // 2. Updates scale

            this.scale = Vector3.Multiply(this.scale, this.LocalScale);

            this.HasScaleChanged = false;
        }

        /// <summary>
        /// Recalculates the absolute color from local and parents values if needed.
        /// </summary>
        protected void RecalculateColor()
        {
            if (this.Parent == null || !this.HasColorChanged)
                return;

            this.color = this.Parent.Color;

            this.color.R = (byte)(((int)this.color.R * (int)this.localColor.R) / 255);
            this.color.G = (byte)(((int)this.color.G * (int)this.localColor.G) / 255);
            this.color.B = (byte)(((int)this.color.B * (int)this.localColor.B) / 255);
            this.color.A = (byte)(((int)this.color.A * (int)this.localColor.A) / 255);

            this.HasColorChanged = false;
        }

        /// <summary>
        /// Recalculate each component if needed.
        /// </summary>
        public void Recalculate()
        {
            this.RecalculatePosition();
            this.RecalculateRotation();
            this.RecalculateScale();
            this.RecalculateColor();
        }
    }
}
/*
 * 
 * Copyright (c) 2008 Christian Leberfinger
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN 
 * AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace de.christianleberfinger.dotnet.pocketknife.ComponentModel
{
    /// <summary>
    /// A type description provider that generates the same descriptions for fields that
    /// are usually only created for properties. Thus, a PropertyGrid component can also
    /// show the public fields of the given object (and not only the 'real' Properties).
    /// This class is based on an excellent article from Stephen Toub.
    /// </summary>
    /// <example>To use this class, simply tell the .NET framework to add a new Provider, e.g.:
    /// <code>
    /// TypeDescriptor.AddProvider(new FieldsToPropertiesDescriptionProvider(typeof(T)), typeof(T));
    /// </code>
    /// </example>
    public class FieldDescriptionProvider : TypeDescriptionProvider
    {
        private PropertyDescriptorCollection _cachedProps;

        /// <summary>
        /// If filtering for specified attributes is required, we cache the results of the filtering.
        /// </summary>
        private FilterCache _filteredCache;

        /// <summary>
        /// The default description provider included in the .net framework.
        /// It is used to provide all the other descriptions that are supported by default.
        /// </summary>
        private TypeDescriptionProvider _defaultProvider;

        /// <summary>
        /// Constructs a new instance of <see cref="FieldDescriptionProvider"/>
        /// </summary>
        /// <param name="t"></param>
        public FieldDescriptionProvider(Type t)
        {
            // get the default provider
            _defaultProvider = TypeDescriptor.GetProvider(t);
        }

        /// <summary>
        /// Returns the new TypeDescriptor (containing the field information).
        /// </summary>
        /// <param name="objectType">type of the object to be described.</param>
        /// <param name="instance">the object to be described</param>
        /// <returns>the new TypeDescriptor (containing the field information)</returns>
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new FieldsToPropertiesTypeDescriptor(
                this, _defaultProvider.GetTypeDescriptor(objectType, instance), objectType);
        }

        /// <summary>
        /// Caches a collection of PropertyDescriptions.
        /// If properties with the same attributes are requested over and over
        /// this can boost the requests.
        /// </summary>
        private class FilterCache
        {
            public Attribute[] Attributes;
            public PropertyDescriptorCollection FilteredProperties;
            public bool IsValid(Attribute[] other)
            {
                if (other == null || Attributes == null) 
                    return false;

                if (Attributes.Length != other.Length) 
                    return false;

                for (int i = 0; i < other.Length; i++)
                {
                    if (!Attributes[i].Match(other[i])) return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Our own implementation of PropertyDescriptor, that - in reality - describes fields.
        /// </summary>
        public class FieldToPropertyDescriptor : PropertyDescriptor
        {
            private FieldInfo _field;

            /// <summary>
            /// Creates a new instance
            /// </summary>
            /// <param name="field"></param>
            public FieldToPropertyDescriptor(FieldInfo field) : base(field.Name, (Attribute[])field.GetCustomAttributes(typeof(Attribute), true))
            {
                _field = field;
            }

            /// <summary>
            /// Returns the field that this instance describes
            /// </summary>
            public FieldInfo Field { get { return _field; } }

            /// <summary>
            /// Queries equality.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                FieldToPropertyDescriptor other = obj as FieldToPropertyDescriptor;
                return other != null && other._field.Equals(_field);
            }

            /// <summary>
            /// Returns the object's hashcode (the field's hashcode)
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode() { return _field.GetHashCode(); }

            /// <summary>
            /// Always returns true, because the public fields are never read-only.
            /// </summary>
            public override bool IsReadOnly { get { return false; } }

            /// <summary>
            /// Not supported. This class cannot reset the field's values.
            /// </summary>
            /// <param name="component"></param>
            public override void ResetValue(object component) { }

            /// <summary>
            /// Not supported. This class cannot reset the field's values.
            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override bool CanResetValue(object component) { return false; }

            /// <summary>
            /// Should be serialized
            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }

            /// <summary>
            /// Returns the field's declaring type
            /// </summary>
            public override Type ComponentType
            {
                get { return _field.DeclaringType; }
            }

            /// <summary>
            /// Returns the field's type.
            /// </summary>
            public override Type PropertyType { get { return _field.FieldType; } }

            /// <summary>
            /// Returns the field's value.
            /// </summary>
            /// <param name="component"></param>
            /// <returns></returns>
            public override object GetValue(object component)
            {
                return _field.GetValue(component);
            }

            /// <summary>
            /// Sets the field's value.
            /// </summary>
            /// <param name="component"></param>
            /// <param name="value"></param>
            public override void SetValue(object component, object value)
            {
                _field.SetValue(component, value);
                
                // raise changed-event. (by calling overriden method)
                OnValueChanged(component, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Our CustomTypeDescriptor
        /// </summary>
        private class FieldsToPropertiesTypeDescriptor : CustomTypeDescriptor
        {
            private Type _objectType;
            private FieldDescriptionProvider _provider;

            public FieldsToPropertiesTypeDescriptor(FieldDescriptionProvider provider, ICustomTypeDescriptor descriptor, Type objectType)
                : base(descriptor)
            {
                if (provider == null) 
                    throw new ArgumentNullException("provider");

                if (descriptor == null) 
                    throw new ArgumentNullException("descriptor");

                if (objectType == null) 
                    throw new ArgumentNullException("objectType");
                
                _objectType = objectType;
                _provider = provider;
            }

            public override PropertyDescriptorCollection GetProperties()
            {
                return GetProperties(null);
            }

            public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                // get the cached properties and filtered properties
                bool filtering = attributes != null && attributes.Length > 0;

                FilterCache cache = _provider._filteredCache;
                PropertyDescriptorCollection props = _provider._cachedProps;

                // use the cache if it's still valid
                if (filtering && cache != null && cache.IsValid(attributes))
                {
                    return cache.FilteredProperties;
                }
                else if (!filtering && props != null)
                {
                    return props;
                }

                // create a new property collection
                props = new PropertyDescriptorCollection(null);

                // add the descriptions offered by default 
                foreach (PropertyDescriptor prop in base.GetProperties(attributes))
                {
                    props.Add(prop);
                }

                // add descriptions for all the fields (filtered by attributes)
                foreach (FieldInfo field in _objectType.GetFields())
                {
                    FieldToPropertyDescriptor fieldDesc = new FieldToPropertyDescriptor(field);
                    if (!filtering || fieldDesc.Attributes.Contains(attributes)) 
                        props.Add(fieldDesc);
                }

                // store the filtered properties in our cache
                if (filtering)
                {
                    cache = new FilterCache();
                    cache.Attributes = attributes;
                    cache.FilteredProperties = props;
                    _provider._filteredCache = cache;
                }
                else
                {
                    // update the reference to the new cache object
                    _provider._cachedProps = props;
                }

                return props;
            }
        }
    } 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Common.Controls.Emr
{
    public abstract class BaseObjectPropertyWrapper<TObject> : IObjectPropertyWrapper
    {
        public IDesignerHost designerhost = null;
        IComponentChangeService cs = null;

        public BaseObjectPropertyWrapper(IDesignerHost host)
        {
            designerhost = host;
            cs = designerhost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;

        [Browsable(false)]
        public TObject WrappedObject
        {
            get { return _wrappedObject; }
            set
            {
                if (value != null)
                {
                    if (!this.WrappedType.IsAssignableFrom(value.GetType()))
                    {
                        throw new ArgumentException("指定的对象不支持属性编辑", "value");
                    }
                }
                _wrappedObject = value;
            }
        }

        [Browsable(false)]
        object IObjectPropertyWrapper.WrappedObject
        {
            get { return this.WrappedObject; }
            set { this.WrappedObject = (TObject)value; }
        }

        [Browsable(false)]
        public Type WrappedType
        {
            get { return typeof(TObject); }
        }

        public abstract object Clone();

        protected void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(_wrappedObject.GetType());
            PropertyDescriptor prop = props.Find(propertyName, false);

            if (prop != null)
            {
                cs.OnComponentChanged(_wrappedObject, prop, oldValue, newValue);
            }

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanging(string propertyName)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(_wrappedObject.GetType());
            PropertyDescriptor prop = props.Find(propertyName, false);

            if (prop != null)
            {
                cs.OnComponentChanging(_wrappedObject, prop);
            }

            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        private TObject _wrappedObject;
    }

}

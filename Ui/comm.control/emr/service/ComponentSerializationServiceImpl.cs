using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Reflection;

namespace Common.Controls.Emr
{
    public class ComponentSerializationServiceImpl : ComponentSerializationService
    {
        IDesignerHost designerhost;
        public ComponentSerializationServiceImpl(IDesignerHost host)
        {
            designerhost = host;
        }

        public override SerializationStore CreateStore()
        {
            return null;
        }

        public override System.Collections.ICollection Deserialize(SerializationStore store, System.ComponentModel.IContainer container)
        {
            
            return null;
        }

        public override System.Collections.ICollection Deserialize(SerializationStore store)
        {
            
            return null;
        }

        public override void DeserializeTo(SerializationStore store, System.ComponentModel.IContainer container, bool validateRecycledTypes, bool applyDefaults)
        {
            
        }

        public override SerializationStore LoadStore(System.IO.Stream stream)
        {
            
            return null;
        }

        public override void Serialize(SerializationStore store, object value)
        {
            
        }

        public override void SerializeAbsolute(SerializationStore store, object value)
        {
            UndoUnits(value);
        }

        public override void SerializeMember(SerializationStore store, object owningObject, System.ComponentModel.MemberDescriptor member)
        {
            
        }

        public override void SerializeMemberAbsolute(SerializationStore store, object owningObject, System.ComponentModel.MemberDescriptor member)
        {
            UndoUnits(owningObject);
        }

        private void UndoUnits(object owningObject)
        {
            UndoEngineImpl undoEngineImpl = designerhost.GetService(typeof(UndoEngine)) as UndoEngineImpl;
            
            if (undoEngineImpl.UndoInProgress == true && undoEngineImpl.UndoUnitName != null && undoEngineImpl.UndoUnitName.IndexOf("删除") == -1 && undoEngineImpl.UndoUnitName.IndexOf("创建") == -1 && undoEngineImpl.UndoUnitName.ToLower().IndexOf("remove") == -1 && undoEngineImpl.UndoUnitName.ToLower().IndexOf("create") == -1)
            {
                for (int i = undoStockImpl.Count - 1; i >= 0; i--)
                {
                    UndoUnitImpl undoUnitImpl = undoStockImpl[i];
                    undoUnitImpl.Undo();
                    undoStockImpl.Remove(undoUnitImpl);
                    redoStockImpl.Add(undoUnitImpl);

                    if (owningObject == undoUnitImpl.OwningObject)
                    {
                        break;
                    }
                }

                ISelectionService selection = designerhost.GetService(typeof(ISelectionService)) as ISelectionService;
                selection.SetSelectedComponents(selection.GetSelectedComponents());
            }
        }

        public void ClearUndoUnitImpl()
        {
            undoStockImpl.Clear();
            redoStockImpl.Clear();
        }

        public void AddUndoUnitImpl(object owningObject, string name, object value)
        {
            UndoUnitImpl undoUnitImpl = new UndoUnitImpl();
            undoUnitImpl.OwningObject = owningObject;
            undoUnitImpl.Name = name;
            undoUnitImpl.Value = value;

            bool repeat = false;

            if (undoStockImpl.Count > 0)
            {
                UndoUnitImpl checkUnit = undoStockImpl[undoStockImpl.Count - 1];
                if (checkUnit.Name == undoUnitImpl.Name && checkUnit.OwningObject == undoUnitImpl.OwningObject && checkUnit.Value == undoUnitImpl.Value)
                {
                    repeat = true;
                }
            }

            if (repeat == false)
            {
                undoStockImpl.Add(undoUnitImpl);
            }

            redoStockImpl.Clear();
        }

        List<UndoUnitImpl> undoStockImpl = new List<UndoUnitImpl>();
        List<UndoUnitImpl> redoStockImpl = new List<UndoUnitImpl>();

        private class UndoUnitImpl
        {

            object owningObject;

            public object OwningObject
            {
                get { return owningObject; }
                set { owningObject = value; }
            }
            string name;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            object value;

            public object Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            public void Undo()
            {
                try
                {
                    //不处理新增和删除
                    Type type = owningObject.GetType();
                    PropertyInfo property = type.GetProperty(name);
                    property.SetValue(owningObject, Value, null);

                }
                catch
                { }
            }
        }
    }


}

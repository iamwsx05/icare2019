using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Common.Controls.Emr
{
    /// <summary>
    /// Undo服务
    /// </summary>
    public class UndoEngineImpl : UndoEngine//, IUndoHandler
    {
        Stack<UndoEngine.UndoUnit> undoStack = new Stack<UndoEngine.UndoUnit>();
        Stack<UndoEngine.UndoUnit> redoStack = new Stack<UndoEngine.UndoUnit>();

        string undoUnitName;

        public string UndoUnitName
        {
            get { return undoUnitName; }
            set { undoUnitName = value; }
        }


        public UndoEngineImpl(IServiceProvider provider)
            : base(provider)
        {

        }

        #region IUndoHandler
        public bool EnableUndo
        {
            get
            {
                return undoStack.Count > 0;
            }
        }

        public bool EnableRedo
        {
            get
            {
                return redoStack.Count > 0;
            }
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                UndoEngine.UndoUnit unit  = undoStack.Pop();
                undoUnitName = unit.Name;
                unit.Undo();
                redoStack.Push(unit);
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                UndoEngine.UndoUnit unit = redoStack.Pop();
                unit.Undo();
                undoStack.Push(unit);
            }
        }
        #endregion

        protected override void AddUndoUnit(UndoEngine.UndoUnit unit)
        {
            undoStack.Push(unit);
        }

        protected override UndoUnit CreateUndoUnit(string name, bool primary)
        {
            return base.CreateUndoUnit(name, primary);
        }

    }
}

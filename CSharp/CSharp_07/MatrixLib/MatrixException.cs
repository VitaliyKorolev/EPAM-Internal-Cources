using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    [Serializable]
    public class MatrixException:Exception
    {
        public int? FirstMatrixRows { get; }
        public int? FirstMatrixColumns { get; }
        public int? SecondMatrixRows { get; }
        public int? SecondMatrixColumns { get; }
        public MatrixException(string message) : base(message) { }
        public MatrixException() { }
        public MatrixException(string message, Exception inner) : base(message, inner) { }

        public MatrixException(string message, Matrix m1, Matrix m2) : base(message)
        {
            if(m1 != null)
            {
                FirstMatrixRows = m1.Rows;
                FirstMatrixColumns = m1.Columns;
            }
            if(m2 != null)
            {
                SecondMatrixRows = m2.Rows;
                SecondMatrixColumns = m2.Columns;
            }
        }

        protected MatrixException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                this.FirstMatrixRows = info.GetInt32("FirstMatrixRows");
                this.FirstMatrixColumns = info.GetInt32("FirstMatrixColumns");
                this.SecondMatrixRows = info.GetInt32("SecondMatrixRows");
                this.SecondMatrixColumns = info.GetInt32("SecondMatrixColumns");
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("FirstMatrixRows", this.FirstMatrixRows);
            info.AddValue("FirstMatrixColumns", this.FirstMatrixColumns);
            info.AddValue("SecondMatrixRows", this.SecondMatrixRows);
            info.AddValue("SecondMatrixColumns", this.SecondMatrixColumns);
        }
    }
}

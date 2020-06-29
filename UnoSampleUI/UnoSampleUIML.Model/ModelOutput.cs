// This file was auto-generated by ML.NET Model Builder. 

using System;
using Microsoft.ML.Data;

namespace UnoSampleUIML.Model
{
    public class ModelOutput
    {
        // ColumnName attribute is used to change the column name from
        // its default value, which is the name of the field.
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }
        public float[] Score { get; set; }
    }
}

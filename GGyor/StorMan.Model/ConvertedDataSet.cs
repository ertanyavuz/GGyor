using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace StorMan.Model
{
    public class ConvertedDataSetModel
    {
        public ConvertedDataSetModel()
        {
            this.Transforms = new List<TransformModel>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string SourceXmlPath { get; set; }

        public List<TransformModel> Transforms { get; set; }
    }

    public class TransformModel
    {
        public TransformModel()
        {
            this.Operations = new List<OperationModel>();
            this.Filters = new List<FilterModel>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public List<OperationModel> Operations { get; set; }
        public List<FilterModel> Filters { get; set; }
    }

    public class FilterModel
    {
        public FilterModel()
        {
            //this.Value = "";
        }
        public int ID { get; set; }
        public string FieldName { get; set; }
        public FilterTypeEnum FilterType { get; set; }
        public object Value { get; set; }

        public bool Check(DataRow row)
        {
            if (row.Table.Columns.Contains(this.FieldName))
            {
                var rowValue = row[this.FieldName];
                if (rowValue == null || rowValue == System.DBNull.Value)
                    return false;

                return rowValue.ToString() == this.Value.ToString();
            }
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static List<FilterModel> Parse(string filterString)
        {
            throw new NotImplementedException();

            var s = filterString.Split("And".ToArray());
            return s.Select(x => new FilterModel
            {

            }).ToList();
        }
    }

    public enum FilterTypeEnum
    {
        Equals
    }

    public class OperationModel
    {
        public OperationModel()
        {
            this.Value = "";
        }
        public int ID { get; set; }
        public OperationTypeEnum OperationType { get; set; }

        public string FieldName { get; set; }
        public Type DataType { get; set; }
        public object Value { get; set; }

        public void ApplyToDataRow(DataRow row)
        {
            if (row.Table.Columns.Contains(this.FieldName))
            {
                var rowValue = row[this.FieldName];
                if (rowValue == null)
                    return;
                if (this.OperationType == OperationTypeEnum.Toplama)
                {
                    var floatValue = stringToFloat(rowValue.ToString());
                    floatValue += stringToFloat(Value.ToString());
                    row[this.FieldName] = floatToStr(floatValue);
                }
                else if (this.OperationType == OperationTypeEnum.Carpma)
                {
                    var floatValue = stringToFloat(rowValue.ToString());
                    floatValue *= stringToFloat(Value.ToString());
                    row[this.FieldName] = floatToStr(floatValue);
                }
                else if (this.OperationType == OperationTypeEnum.Eşitleme)
                {
                    row[this.FieldName] = this.Value.ToString();
                }
                else if (this.OperationType == OperationTypeEnum.Ekleme)
                {
                    if (rowValue == System.DBNull.Value)
                        rowValue = "";
                    var stringValue = rowValue.ToString();
                    stringValue += this.Value.ToString().Replace("\\n", "\n").Replace("\\t", "\t");
                    row[this.FieldName] = stringValue;
                }
            }
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} - {2}", this.FieldName, this.OperationType, this.Value);
        }

        public static float stringToFloat(string floatStr)
        {
            floatStr = floatStr.Replace(",", "");
            var floatValue = float.Parse(floatStr, CultureInfo.InvariantCulture);
            return floatValue;
        }
        public static string floatToStr(float floatValue)
        {
            var floatStr = floatValue.ToString();
            floatStr = floatStr.Replace(",", ".");
            return floatStr;
        }

    }


    //// Alan (operand) Değer
    //// (Alan, Değer) => (...)
    //// Action<Alan, Değer>
    //public enum XmlOperationType
    //{
    //    FiyatAyarlama,  // Fiyat arttır-azalt. yüzde ile veya ekleyerek
    //    KurDonusturme,  // Kurları TL'ye çevirme
    //    AciklamaEkle,   // Açıklama alanlarının başına ve sonuna yazı ekleme
    //    AlanKaldirma,   // XML'deki bir alanı tümüyle kaldırma
    //    StokAyarlama,   // Stok sayısını arttırıp azaltma, yüzde ile veya adet ile.
    //    KargoAyarlama   // Alıcı-satıcı öder durumu.
    //}

    public enum OperationTypeEnum
    {
        Carpma,
        Toplama,
        Eşitleme,
        Ekleme
    }
}

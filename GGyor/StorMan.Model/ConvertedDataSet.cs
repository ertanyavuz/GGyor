﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using NCalc;

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
        public string XmlString { get; set; }

        public List<TransformModel> Transforms { get; set; }
        public DataTable DataTable { get; set; }
        public DataTable ResultTable { get; set; }

        public static List<Tuple<string, string, float>> ExchangeRates { get; set; }

        public void LoadXml()
        {
            this.DataTable = new DataTable("ProductsXml");

            var xdoc = XDocument.Load(this.SourceXmlPath);
            //var originalFile = String.Format("es_urun_{0}.xml", DateTime.s);
            //xdoc.Save("");

            var q = from d in xdoc.Root.Descendants("item")
                    select d;
            var list = q.ToList();

            foreach (var xElement in list)
            {
                var valueList = new List<object>();
                foreach (var attr in xElement.Elements())
                {
                    if (this.DataTable.Columns[attr.Name.LocalName] == null)
                        this.DataTable.Columns.Add(attr.Name.LocalName);
                }
                var dr = this.DataTable.NewRow();
                foreach (var attr in xElement.Elements())
                {
                    dr[attr.Name.LocalName] = attr.Value;
                }

                this.DataTable.Rows.Add(dr);
            }
        }
        public void ApplyTransforms()
        {
            this.ResultTable = this.DataTable.Copy();
            this.ResultTable.Rows.Clear();

            foreach (var transform in this.Transforms)
            {
                // Get a copy of the data
                var dt = this.DataTable.Copy();

                // Apply Filters
                foreach (var filterModel in transform.Filters)
                {
                    var rowsToDelete = new List<DataRow>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!filterModel.Check(row))
                            rowsToDelete.Add(row);
                    }
                    foreach (var dataRow in rowsToDelete)
                    {
                        dt.Rows.Remove(dataRow);
                    }
                }

                // Apply transforms
                int i = 0, j = 0;
                foreach (DataRow row in dt.Rows)
                {
                    i++;
                    j = 0;
                    foreach (var operationModel in transform.Operations)
                    {
                        j++;
                        operationModel.ApplyToDataRow(row);
                    }
                }

                // Append data to ResultTable
                while (dt.Rows.Count > 0)
                {
                    var dr = dt.Rows[0];
                    this.ResultTable.ImportRow(dr);
                    dt.Rows.RemoveAt(0);
                }
            }
        }
        public void SaveAsXml(string path)
        {
            var xdoc = new XDocument();
            xdoc.Add(new XElement("root"));
            //xdoc.Root = new XElement("root");
            foreach (DataRow row in this.ResultTable.Rows)
            {
                var xItem = new XElement("item");
                foreach (DataColumn column in ResultTable.Columns)
                {
                    var xElement = new XElement(column.ColumnName);
                    if (column.ColumnName == "details")
                        xElement.ReplaceNodes(new XCData(row[column].ToString()));
                    else
                        xElement.Value = row[column].ToString();
                    xItem.Add(xElement);
                }
                xdoc.Root.Add(xItem);
            }

            xdoc.Save(path);
        }
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
            return String.Format("{0} {1} {2}", FieldName, FilterType, Value);
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
        Equals,
        Aralık
    }

    public class OperationModel
    {
        public OperationModel()
        {
            this.Value = "";
            //this.Parameters = new Dictionary<string, object>();
            if (String.IsNullOrEmpty(OperationModel.CurrencyColumn))
                OperationModel.CurrencyColumn = "currencyAbbr";
        }

        #region " Properties "

        public int ID { get; set; }
        public OperationTypeEnum OperationType { get; set; }

        public string Name { get; set; }
        public string FieldName { get; set; }
        public Type DataType { get; set; }
        public object Value { get; set; }
        //public string ExpressionString { get; set; }

        //private Expression _expression;
        //public Expression Expression
        //{
        //    get
        //    {
        //        if (_expression == null)
        //        {
        //            if (String.IsNullOrWhiteSpace(this.ExpressionString) && this.OperationType == OperationTypeEnum.Karmaşıkİfade)
        //                this.ExpressionString = this.Value.ToString();
        //            if (!String.IsNullOrWhiteSpace(this.ExpressionString))
        //                _expression = new Expression(this.ExpressionString);
        //            else
        //            {
        //                switch (this.OperationType)
        //                {
        //                    case OperationTypeEnum.Toplama:
        //                        this.ExpressionString = String.Format("[{0}] + {1}", this.FieldName, this.Value);
        //                        break;
        //                    case OperationTypeEnum.Carpma:
        //                        this.ExpressionString = String.Format("[{0}] * {1}", this.FieldName, this.Value);
        //                        break;
        //                    //case OperationTypeEnum.Eşitleme:
        //                    //    this.ExpressionString = String.Format("[{0}]", this.FieldName);
        //                    //    break;
        //                    //case OperationTypeEnum.Ekleme:
        //                    //    break;
        //                    //case OperationTypeEnum.KurDönüşümü:
        //                    //    this.ExpressionString = String.Format("[{0}] * {1}", this.FieldName, this.Value);
        //                    //    break;
        //                    //case OperationTypeEnum.Karmaşıkİfade:
        //                    //    this.ExpressionString = String.Format("[{0}] + {1}", this.FieldName, this.Value);
        //                    //    break;
        //                }
        //            }

        //            if (!String.IsNullOrWhiteSpace(this.ExpressionString))
        //            {
        //                this.Parameters = new Dictionary<string, object>();
        //                var matches = System.Text.RegularExpressions.Regex.Matches(this.ExpressionString, "\\[[\\w\\d]+\\]");
        //                foreach (Match m in matches)
        //                {
        //                    var varNameWithoutBrackets = m.Value.Replace("[", "").Replace("]", "");
        //                    this.Parameters.Add(varNameWithoutBrackets, null);
        //                    this.ExpressionString = this.ExpressionString.Replace(m.Value, varNameWithoutBrackets);
        //                }
        //                _expression = new Expression(this.ExpressionString);
        //            }
        //        }
        //        return _expression;
        //    }
        //}

        //public Dictionary<string, object> Parameters { get; set; }
        private Dictionary<string, float> evaluationTable = new Dictionary<string, float>();

        // Kur Dönüşümü için
        public static string CurrencyColumn { get; set; }

    #endregion
        
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
                    floatValue = roundFloat(floatValue);
                    floatValue += stringToFloat(Value.ToString());
                    row[this.FieldName] = floatToStr(floatValue);
                }
                else if (this.OperationType == OperationTypeEnum.Carpma)
                {
                    var floatValue = stringToFloat(rowValue.ToString());
                    floatValue = roundFloat(floatValue);
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
                else if (this.OperationType == OperationTypeEnum.KurDönüşümü)
                {
                    var currentCurrency = row[CurrencyColumn].ToString();
                    var newCurrency = this.Value.ToString();
                    if (newCurrency == currentCurrency)
                        return;
                    var rateTuple = ConvertedDataSetModel.ExchangeRates.FirstOrDefault(x => x.Item1 == currentCurrency && x.Item2 == newCurrency);
                    float rate;
                    if (rateTuple == null)
                    {
                        // try the inverse of it
                        rateTuple = ConvertedDataSetModel.ExchangeRates.FirstOrDefault(x => x.Item2 == currentCurrency && x.Item1 == newCurrency);
                        if (rateTuple == null)
                            throw new Exception(String.Format("{0} - {1} kuru bulunamadı.", currentCurrency, newCurrency));
                        rate = (float) Math.Round(1 / rateTuple.Item3, 4);
                    }
                    else
                        rate = rateTuple.Item3;

                    row[CurrencyColumn] = newCurrency;
                    var currentValue = stringToFloat(row[this.FieldName].ToString());
                    var newValue = currentValue*rate;
                    row[this.FieldName] = newValue;
                    
                    //// [buyingPrice]=[currencyAbbr] ? TL: [buyingPrice], USD: [buyingPrice]*1.95, EUR: [buyingPrice]*2.54
                    //var exp = String.Format("[{0}]=[{1}] ? {2}: [{0}], ", this.FieldName, CurrencyColumn, TLText);

                    //exp += CurrencyExchangeTable.Select(x => String.Format("{0}: [{1}]*{2}", x.Item1, this.FieldName, x.Item3))
                    //                            .Aggregate((x,y) => x + ", " + y);

                    //applyExpression(row, exp);
                }
                else if (this.OperationType == OperationTypeEnum.Karmaşıkİfade)
                {
                    //foreach (var parameter in this.Expression.Parameters.Keys)
                    //{
                    //    this.Expression.Parameters[parameter] = row[parameter];
                    //}

                    var lines = this.Value.ToString().Split('\n');
                    foreach (var line in lines)
                    {
                        if (String.IsNullOrWhiteSpace(line))
                            continue;
                        applyExpression(row, line);
                    }
                }

            }
        }

    #region " Privates "

        private void applyExpression(DataRow row, string line)
        {
            ////var sides = line.Split('=');
            //var m = System.Text.RegularExpressions.Regex.Match(line, "\\[([\\w\\d]+)\\]\\s*=\\s*([\\w\\d\\.\\[\\]]+)\\s*([\\+\\-\\*\\/])\\s*([\\w\\d\\.\\[\\]]+)");
            //if (m.Success)
            //{
            //    var leftSide = m.Groups[1].Value;
            //    var operand1 = m.Groups[2].Value;
            //    var op = m.Groups[3].Value;
            //    var operand2 = m.Groups[4].Value;
            //    var operand1Value = getOperandValue(row, operand1);
            //    var operand2Value = getOperandValue(row, operand2);

            //    if (op == "+")
            //    {
            //        row[leftSide] = operand1Value + operand2Value;
            //    }
            //    else if (op == "-")
            //    {
            //        row[leftSide] = operand1Value - operand2Value;
            //    }
            //    else if (op == "*")
            //    {
            //        row[leftSide] = operand1Value * operand2Value;
            //    }
            //    else if (op == "/")
            //    {
            //        row[leftSide] = Math.Round(operand1Value / operand2Value, 4);
            //    }
            //}

            var m = System.Text.RegularExpressions.Regex.Match(line, "\\[([\\w\\d]+)\\]\\s*(=>*)\\s*([\\w\\W]+)\\s*");
            if (m.Success)
            {
                var leftSide = m.Groups[1].Value;
                var opSign = m.Groups[2].Value;
                var expression = m.Groups[3].Value;
                if (expression.Contains("?"))
                {
                    // conditional expression
                    var decisionExpArr = expression.Split('?');
                    var decisionValueStr = getOperandValue(row, decisionExpArr[0].Trim());

                    var branchesArr = decisionExpArr[1].Split(',');
                    foreach (var branch in branchesArr)
                    {
                        var branchOperands = branch.Trim().Split(':');
                        if (branchOperands[0].StartsWith("<"))
                        {
                            // Checking for range
                            var decisionValue = stringToFloat(decisionValueStr);
                            var branchValueStr = branchOperands[0].Substring(1);
                            var branchValue = evaluateExpression(branchValueStr);

                            if (decisionValue < branchValue)
                            {
                                // Take branch
                                var valueToSet = branchOperands[1].Trim();
                                try
                                {   // Try to evaluate in case this is an arithmetic expression
                                    valueToSet = floatToStr(evaluateExpression(branchOperands[1], row));
                                }
                                catch { }

                                row[leftSide] = valueToSet;
                                break;
                            }
                        }
                        else
                        {
                            // Checking for exach match
                            if (decisionValueStr.Trim() == branchOperands[0])
                            {
                                // Take branch
                                var valueToSet = branchOperands[1].Trim();
                                try
                                {   // Try to evaluate in case this is an arithmetic expression
                                    valueToSet = replaceVariables(valueToSet, row);
                                    valueToSet = floatToStr(evaluateExpression("(" + branchOperands[1] + ") * 1", row));
                                }
                                catch { }

                                row[leftSide] = valueToSet;
                                break;
                            }
                        }
                    }

                }
                else
                {
                    var floatResult = evaluateExpression(expression, row);
                    row[leftSide] = floatToStr(floatResult);
                }
                
            }
        }
        private string getOperandValue(DataRow row, string expression)
        {
            expression = expression.Trim();
            if (expression.StartsWith("["))
            {
                return row[expression.Replace("[", "").Replace("]", "")] as string;
            }
            else
            {
                return expression;
            }
        }

        private string replaceVariables(string expression, DataRow row)
        {
            var matches = System.Text.RegularExpressions.Regex.Matches(expression, "\\[[\\w\\d]+\\]");
            foreach (Match match in matches)
            {
                var value = getOperandValue(row, match.Value);
                expression = expression.Replace(match.Value, value);
            }
            return expression;
        }
        private float evaluateExpression(string expression, DataRow row)
        {
            //var matches = System.Text.RegularExpressions.Regex.Matches(expression, "\\[[\\w\\d]+\\]");
            //foreach (Match match in matches)
            //{
            //    var value = getOperandValue(row, match.Value);
            //    expression = expression.Replace(match.Value, value);
            //}
            expression = replaceVariables(expression, row);
            var floatResult = evaluateExpression(expression);

            return floatResult;
        }
        private static int hitCount = 0;
        private static int missCount = 0;
        private float evaluateExpression(string expression)
        {
            var dt = new DataTable();
            dt.Columns.Add("", typeof (int), "");

            float result;
            if (!evaluationTable.ContainsKey(expression))
            {
                var e = new Expression(expression, EvaluateOptions.NoCache);
                result = (float)Convert.ToDouble(e.Evaluate());
                //evaluationTable.Add(expression, result);
                missCount++;
            }
            else
            {
                result = evaluationTable[expression];
                hitCount++;
            }

            //if (e.HasErrors())
            //    return float.MinValue;

            result = roundFloat(result);
            return result;
        }

    #endregion

        public override string ToString()
        {
            var substr = new Func<string, int, string>((str, len) => str.Substring(0, Math.Min(str.Length, len)));

            switch (OperationType)
            {
                case OperationTypeEnum.Karmaşıkİfade:
                    return String.Format("{0} - {1} - {2}", this.FieldName, this.OperationType, this.Value.ToString());
                case OperationTypeEnum.KurDönüşümü:
                    return String.Format("{0} - Kur Dönüşümü", this.FieldName);

                default:
                    return String.Format("{0} - {1} - {2}", this.FieldName, this.OperationType, this.Value);
            }

        }

    #region " Statics "
    
        public static float stringToFloat(string floatStr)
        {
            floatStr = floatStr.Replace(",", ".");
            var floatValue = float.Parse(floatStr, CultureInfo.InvariantCulture);
            return floatValue;
        }
        public static string floatToStr(float floatValue)
        {
            floatValue = roundFloat(floatValue);
            
            var floatStr = floatValue.ToString();
            floatStr = floatStr.Replace(",", ".");
            return floatStr;
        }
        public static float roundFloat(float floatValue)
        {
            //floatValue *= 100;
            //floatValue = (float) Math.Ceiling(floatValue);
            //floatValue = floatValue/100;

            var roundedValue = (float) Math.Round(floatValue, 2);

            return roundedValue;
        }
    
    #endregion
    
        public OperationModel Copy(bool doNotCopyId = false)
        {
            return new OperationModel
                {
                    DataType = this.DataType,
                    //ExpressionString = this.ExpressionString,
                    //Expression = !String.IsNullOrWhiteSpace(this.ExpressionString) ? new Expression(this.ExpressionString) : null,
                    //Parameters = new List<string>(),
                    Name = this.Name,
                    FieldName = this.FieldName,
                    ID = doNotCopyId ? 0 : this.ID,
                    OperationType = this.OperationType,
                    Value = this.Value
                };
        }

        public int? Order { get; set; }
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
        Ekleme,
        KurDönüşümü,
        Karmaşıkİfade
    }

}

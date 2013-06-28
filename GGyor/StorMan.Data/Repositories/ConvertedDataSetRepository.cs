using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorMan.Model;

namespace StorMan.Data.Repositories
{
    public class ConvertedDataSetRepository : RepositoryBase
    {
        public ConvertedDataSetRepository()
        {
            _context = new StorManEntities();
        }

        public List<ConvertedDataSetModel> getConvertedDataSets()
        {
            var list = _context.ConvertedDataSets.OrderBy(x => x.ID).ToList();

            return list.Select(x => new ConvertedDataSetModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    SourceXmlPath = x.SourceXmlPath,
                    Transforms = new List<TransformModel>()
                }).ToList();
        }

        public List<TransformModel> getTransforms(int convertedDataSetID)
        {
            var transformList = _context.Transforms.Where(x => x.ConvertedDataSetID == convertedDataSetID)
                               .ToList();

            var list = transformList.Select(x => new TransformModel
                {
                    ID = x.ID,
                    Filters = x.Filters.Select(y => new FilterModel
                        {
                            FieldName = y.FieldName,
                            FilterType = (FilterTypeEnum) (y.FilterType ?? 1),
                            Value = y.Value
                        }).ToList(),
                    Operations = x.Operations.Select(y => new OperationModel
                        {
                            FieldName = y.FieldName,
                            OperationType = (OperationTypeEnum) (y.OperationType ?? 0),
                            //DataType = y.
                            Value = y.Value
                        }).ToList()
                }).ToList();

            return list;
        }

        public int createConvertedDataSet(string name, string sourceXmlPath)
        {
            var cds = new ConvertedDataSet
                {
                    Name = name,
                    SourceXmlPath = sourceXmlPath
                };
            _context.ConvertedDataSets.Add(cds);
            _context.SaveChanges();

            return cds.ID;

        }

        
    }
}

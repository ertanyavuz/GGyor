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
                    Name = x.Name,
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

        public TransformModel getTransform(int transformId)
        {
            var transform = _context.Transforms.FirstOrDefault(x => x.ID == transformId);

            if (transform == null)
                return null;

            var model = new TransformModel
                {
                    ID = transform.ID,
                    Name = transform.Name,
                    Filters = transform.Filters.Select(y => new FilterModel
                        {
                            FieldName = y.FieldName,
                            FilterType = (FilterTypeEnum) (y.FilterType ?? 1),
                            Value = y.Value
                        }).ToList(),
                    Operations = transform.Operations.Select(y => new OperationModel
                        {
                            FieldName = y.FieldName,
                            OperationType = (OperationTypeEnum) (y.OperationType ?? 0),
                            //DataType = y.
                            Value = y.Value
                        }).ToList()
                };

            return model;
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


        public bool deleteTransform(int transformID)
        {
            var transform = _context.Transforms.FirstOrDefault(x => x.ID == transformID);
            if (transform == null)
                return false;

            var filterList = transform.Filters.ToList();
            var opList = transform.Operations.ToList();
            foreach (var filter in filterList)
            {
                _context.Filters.Remove(filter);
            }
            foreach (var operation in opList)
            {
                _context.Operations.Remove(operation);
            }
            _context.Transforms.Remove(transform);

            _context.SaveChanges();

            return true;
        }

        public int createTransform(int cdsID, TransformModel transformModel)
        {
            var dbTransform = new Transform
                {
                    ConvertedDataSetID = cdsID,
                    Name = transformModel.Name
                };
            _context.Transforms.Add(dbTransform);

            foreach (var filterModel in transformModel.Filters)
            {
                var dbFilter = new Filter
                    {
                        FieldName = filterModel.FieldName,
                        Transform = dbTransform,
                        FilterType = (int?) filterModel.FilterType,
                        Value = filterModel.Value.ToString()
                    };
                _context.Filters.Add(dbFilter);
            }
            foreach (var operationModel in transformModel.Operations)
            {
                var dbOperation = new Operation
                    {
                        Transform = dbTransform,
                        FieldName = operationModel.FieldName,
                        OperationType = (int?) operationModel.OperationType,
                        Value = operationModel.Value.ToString()
                    };
                _context.Operations.Add(dbOperation);
            }
            _context.SaveChanges();

            return dbTransform.ID;
        }

        public bool updateTransform(TransformModel transformModel)
        {
            var transform = _context.Transforms.FirstOrDefault(x => x.ID == transformModel.ID);
            if (transform == null)
                return false;

            transform.Name = transformModel.Name;
            _context.SaveChanges();

            var filterList = transform.Filters.ToList();

            this.Sync(transformModel.Filters, filterList, (filterModel, dbFilter) => filterModel.ID.CompareTo(dbFilter.ID),
                        (filterModel, dbFilter) =>
                            {
                                if (dbFilter.FieldName != filterModel.FieldName)
                                    dbFilter.FieldName = filterModel.FieldName;
                                if (dbFilter.FilterType != (int?) filterModel.FilterType)
                                    dbFilter.FilterType = (int?) filterModel.FilterType;
                                if (dbFilter.Value != filterModel.Value.ToString())
                                    dbFilter.Value = filterModel.Value.ToString();
                                if (dbFilter.Transform == null)
                                    dbFilter.Transform = transform;
                                return true;
                            });

            var opList = transform.Operations.ToList();
            this.Sync(transformModel.Operations, opList, (opModel, dbOp) => opModel.ID.CompareTo(dbOp.ID),
                        (opModel, dbOp) =>
                            {
                                if (dbOp.FieldName != opModel.FieldName)
                                    dbOp.FieldName = opModel.FieldName;
                                if (dbOp.OperationType != (int) opModel.OperationType)
                                    dbOp.OperationType = (int) opModel.OperationType;
                                if (dbOp.Value != opModel.Value.ToString())
                                    dbOp.Value = opModel.Value.ToString();
                                if (dbOp.Transform == null)
                                    dbOp.Transform = transform;
                                return true;
                            });
            _context.SaveChanges();

            return true;
        }

    }
}

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

        public List<ConvertedDataSetModel> getConvertedDataSets(bool getAll = false)
        {
            ConvertedDataSetModel.ExchangeRates = getExchangeRates();

            var cdsList = _context.ConvertedDataSets.OrderBy(x => x.ID);
            var list = cdsList.ToList().Select(x => new ConvertedDataSetModel
                                {
                                    ID = x.ID,
                                    Name = x.Name,
                                    SourceXmlPath = x.SourceXmlPath,
                                    Transforms = new List<TransformModel>()
                                }).ToList();

            if (getAll)
            {
                var transformList = _context.Transforms.OrderBy(x => x.ConvertedDataSetID).ThenBy(x => x.ID).ToList();
                var filterList = _context.Filters.OrderBy(x => x.TransformID).ThenBy(x => x.ID).ToList();
                var opList = _context.Operations.OrderBy(x => x.TransformID).ThenBy(x => x.ID).ToList();

                foreach (var cds in list)
                {
                    cds.Transforms = transformList.Where(x => x.ConvertedDataSetID == cds.ID).ToList()
                                            .Select(x => new TransformModel
                                                {
                                                    ID = x.ID,
                                                    Name = x.Name ?? "",
                                                    Filters = filterList.Where(y => y.TransformID == x.ID)
                                                                        .Select(y => new FilterModel
                                                                        {
                                                                            ID = y.ID,
                                                                            FieldName = y.FieldName ?? "",
                                                                            FilterType = (FilterTypeEnum)(y.FilterType ?? 1),
                                                                            Value = y.Value
                                                                        }).ToList(),
                                                    Operations = opList.Where(y => y.TransformID == x.ID)
                                                                        .Select(y => new OperationModel
                                                                            {
                                                                                ID = y.ID,
                                                                                Name = y.Name ?? "",
                                                                                FieldName = y.FieldName ?? "",
                                                                                OperationType = (OperationTypeEnum)(y.OperationType ?? 0),
                                                                                Value = y.Value,
                                                                                Order = y.Order
                                                                            }).ToList()
                                                }).ToList();
                }
            }

            return list;
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
                    Operations = x.Operations.OrderBy(y => y.Order).Select(y => new OperationModel
                        {
                            Name = y.Name,
                            FieldName = y.FieldName,
                            OperationType = (OperationTypeEnum) (y.OperationType ?? 0),
                            Value = y.Value,
                            Order = y.Order
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
                    Operations = transform.Operations.OrderBy(y => y.Order).Select(y => new OperationModel
                        {
                            Name = y.Name,
                            FieldName = y.FieldName,
                            OperationType = (OperationTypeEnum) (y.OperationType ?? 0),
                            //DataType = y.
                            Value = y.Value,
                            Order = y.Order
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

        public bool updateConvertedDataSet(int cdsId, string name, string sourceXmlPath)
        {
            var cds = _context.ConvertedDataSets.FirstOrDefault(x => x.ID == cdsId);
            if (cds == null)
                return false;

            cds.Name = name;
            cds.SourceXmlPath = sourceXmlPath;
            _context.SaveChanges();

            return true;
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
            var order = 10;
            foreach (var operationModel in transformModel.Operations)
            {
                var dbOperation = new Operation
                    {
                        Name = operationModel.Name,
                        Transform = dbTransform,
                        FieldName = operationModel.FieldName,
                        OperationType = (int?) operationModel.OperationType,
                        Value = operationModel.Value.ToString(),
                        Order = order
                    };
                _context.Operations.Add(dbOperation);
                order += 10;
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
                                if (dbOp.Name != opModel.Name)
                                    dbOp.Name = opModel.Name;
                                if (dbOp.FieldName != opModel.FieldName)
                                    dbOp.FieldName = opModel.FieldName;
                                if (dbOp.OperationType != (int) opModel.OperationType)
                                    dbOp.OperationType = (int) opModel.OperationType;
                                if (dbOp.Value != opModel.Value.ToString())
                                    dbOp.Value = opModel.Value.ToString();
                                if (dbOp.Order != opModel.Order)
                                    dbOp.Order = opModel.Order;
                                if (dbOp.Transform == null)
                                    dbOp.Transform = transform;
                                return true;
                            });
            _context.SaveChanges();

            return true;
        }


        public bool deleteConvertedDataSet(int cdsId)
        {
            var opList = _context.Operations.Where(x => x.Transform.ConvertedDataSetID == cdsId).ToList();
            foreach (var operation in opList)
            {
                _context.Operations.Remove(operation);
            }

            var filterList = _context.Filters.Where(x => x.Transform.ConvertedDataSetID == cdsId).ToList();
            foreach (var filter in filterList)
            {
                _context.Filters.Remove(filter);
            }

            var tranList = _context.Transforms.Where(x => x.ConvertedDataSetID == cdsId).ToList();
            foreach (var transform in tranList)
            {
                _context.Transforms.Remove(transform);
            }

            var cds = _context.ConvertedDataSets.FirstOrDefault(x => x.ID == cdsId);
            if (cds != null)
                _context.ConvertedDataSets.Remove(cds);

            var count = _context.SaveChanges();

            return count > 0;

        }

        public List<Tuple<string, string, float>> getExchangeRates()
        {
            var list = _context.ExchangeRates.ToList()
                                    .Select(x => new Tuple<string, string, float>(x.FromCurrency, x.ToCurrency, (float) x.Rate))
                                    .ToList();
            return list;
        }

    }
}

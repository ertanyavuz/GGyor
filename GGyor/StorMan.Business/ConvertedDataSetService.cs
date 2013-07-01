using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorMan.Data.Repositories;
using StorMan.Model;

namespace StorMan.Business
{
    public class ConvertedDataSetService
    {
        private ConvertedDataSetRepository _repository;

        public ConvertedDataSetService()
        {
            _repository = new ConvertedDataSetRepository();
        }

        public int createConvertedDataSet(string name, string sourceXmlPath)
        {
            return _repository.createConvertedDataSet(name, sourceXmlPath);
            
        }

        public List<ConvertedDataSetModel> getConvertedDataSets()
        {
            return _repository.getConvertedDataSets();
        }

        public List<TransformModel> getTransforms(int convertedDataSetID)
        {
            return _repository.getTransforms(convertedDataSetID);
        }

        public bool deleteTransform(int transformID)
        {
            return _repository.deleteTransform(transformID);
        }

        public int createTransform(int cdsID, TransformModel transform)
        {
            return _repository.createTransform(cdsID, transform);
        }

        public bool updateTransform(TransformModel transform)
        {
            return _repository.updateTransform(transform);
        }
    }
}

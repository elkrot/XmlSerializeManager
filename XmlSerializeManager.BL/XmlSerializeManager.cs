using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlSerializeManager.BL
{

    public class XmlSerializeManager<T> where T : class
    {
        #region Fields
        T _entity;
        string _path;

        #endregion

        #region Constructors
        public XmlSerializeManager()
        {

        }
        public XmlSerializeManager(T entity) : this()
        {
            _entity = entity;
        }

        public XmlSerializeManager(string path)
        {
            _path = path;
        }
        public XmlSerializeManager(T entity, string path) : this(path)
        {
            _entity = entity;
        }
        #endregion

        #region AsyncMethods
        public async Task<MethodResult<T>> TryGetEntityFromXmlStringAsync(string xml)
        {
            return await Task.Run(() => TryGetEntityFromXmlString(xml));
        }

        public async Task<MethodResult<string>> TryGetStringXmlFromEntityAsync(T entity)
        {
            return await Task.Run(() => TryGetStringXmlFromEntity(entity));

        }

        public async Task<MethodResult<T>> TryLoadEntityFromXmlFileAsync(string path)
        {
            return await Task.Run(() => TryLoadEntityFromXmlFile(path));
        }

        public async Task<MethodResult> TrySaveEntityToXmlFileAsync(string path, T entity)
        {
            return await Task.Run(() => TrySaveEntityToXmlFile( path, entity));
        }
            #endregion

        #region SafeMethods

            #region TryGetEntityFromXmlString
            public MethodResult<T> TryGetEntityFromXmlString(string xml)
        {
            T result = null;
            MethodResult<T> methodResult = new MethodResult<T>(result);
            try
            {
                result = GetEntityFromXmlString(xml);
                methodResult.Result = result;
                methodResult.Success = true;
            }
            catch (Exception ex)
            {
                methodResult.Messages.Add(ex.Message);
                methodResult.Success = false;
            }

            return methodResult;
        }

        #endregion

        #region TryGetStringXmlFromEntity
        public MethodResult<string> TryGetStringXmlFromEntity(T entity)
        {
            MethodResult<string> methodResult = new MethodResult<string>("");
            try
            {
                methodResult.Result = GetStringXmlFromEntity(entity);
                methodResult.Success = true;
            }
            catch (Exception ex)
            {
                methodResult.Messages.Add(ex.Message);
                methodResult.Success = false;
            }
            return methodResult;
        }

        #endregion

        #region TryLoadEntityFromXmlFile
        public MethodResult<T> TryLoadEntityFromXmlFile(string path)
        {
            MethodResult<T> methodResult = new MethodResult<T>(null);
            try
            {
                methodResult.Result = LoadEntityFromXmlFile(path);
                methodResult.Success = true;
            }
            catch (Exception ex)
            {
                methodResult.Messages.Add(ex.Message);
                methodResult.Success = false;
            }
            return methodResult;
        }

        #endregion

        #region TrySaveEntityToXmlFile
        public MethodResult TrySaveEntityToXmlFile(string path, T entity)
        {
            MethodResult methodResult = new MethodResult();
            try
            {
                SaveEntityToXmlFile(path, entity);
                methodResult.Success = true;
            }
            catch (Exception ex)
            {
                methodResult.Messages.Add(ex.Message);
                methodResult.Success = false;
            }

            return methodResult;
        }
        #endregion

        #endregion

        #region Private Methods
        #region GetEntityFromXmlString
        private T GetEntityFromXmlString(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) throw new ArgumentException("xml", "Строка xml не может быть равна пусто");

            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new StringReader(xml))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    return serializer.Deserialize(reader) as T;
                }
            }
        }
        #endregion

        #region GetStringXmlFromEntity

        private string GetStringXmlFromEntity(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity", "entity не может быть равна null");
            var serializer = new XmlSerializer(typeof(T));
            using (var sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, entity);
                    return sw.ToString();
                }
            }
        }
        #endregion

        #region LoadEntityFromXmlFile
        private T LoadEntityFromXmlFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("path", "Строка path не может быть равна пусто");
            var encoding = System.Text.Encoding.UTF8;
            var serializer = new XmlSerializer(typeof(T));

            using (var stream = new StreamReader(path, encoding, false))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    return serializer.Deserialize(reader) as T;
                }
            }
        }
        #endregion

        #region SaveEntityToXmlFile
        private void SaveEntityToXmlFile(string path, T entity)
        {
            var formatter = new XmlSerializer(typeof(T));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, entity);
            }
        }
        #endregion

        #endregion
    }





}

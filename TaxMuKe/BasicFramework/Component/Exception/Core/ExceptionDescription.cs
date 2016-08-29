using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace BasicFramework.Component
{
    public class ExceptionDescription
    {
        private readonly string _descriptionXmlPath;
        private readonly Dictionary<string, DescriptionItem[]> _descriptionItemses;

        public ExceptionDescription()
            : this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExceptionDescription.xml"))
        {

        }

        public ExceptionDescription(string descriptionXmlPath)
        {
            _descriptionXmlPath = descriptionXmlPath;
            if (File.Exists(_descriptionXmlPath) == false)
            {
                throw new FileNotFoundException("异常描述文件不存在或无法访问。", _descriptionXmlPath);
            }

            var xmlDoc = new XmlDocument();
            _descriptionItemses = new Dictionary<string, DescriptionItem[]>();
            var commonArray = new DescriptionItem[0];
            try
            {
                xmlDoc.Load(_descriptionXmlPath);
                var commonNode = xmlDoc.SelectSingleNode("/Description/Common");
                if (commonNode != null)
                {
                    commonArray =
                        commonNode.ChildNodes.OfType<XmlNode>().Where(x => x.Attributes != null)
                            .Select(
                                x => new DescriptionItem()
                                         {
                                             Id = int.Parse(x.Attributes["Id"].Value),
                                             Message = x.Attributes["Message"].Value
                                         }).ToArray();

                    _descriptionItemses.Add("Common", commonArray);
                }

                var businessNodes = xmlDoc.SelectNodes("/Description/Business");
                if (businessNodes != null)
                {
                    foreach (var businessNode in businessNodes.OfType<XmlNode>())
                    {
                        if (businessNode.Attributes == null)
                        {
                            throw new XmlException("Business 节点的 Attributes 为 null.");
                        }


                        var businessNameAtt = businessNode.Attributes["Name"];
                        if (businessNameAtt == null)
                        {
                            throw new XmlException("缺少属性 \"Name\"。");
                        }

                        var businessArray =
                            businessNode.ChildNodes.OfType<XmlNode>().Where(x => x.Attributes != null)
                                .Select(x => new DescriptionItem()
                                                 {
                                                     Id = int.Parse(x.Attributes["Id"].Value),
                                                     Message = x.Attributes["Message"].Value
                                                 }).ToArray();

                        if (_descriptionItemses.ContainsKey(businessNameAtt.Value))
                        {
                            throw new XmlException("Business 节点存在同名。");
                        }

                        _descriptionItemses.Add(businessNameAtt.Value, businessArray.Concat(commonArray).ToArray());
                    }
                }
            }
            catch (FormatException formatException)
            {
                throw new XmlException("异常描述文件分析转换错误。", formatException);
            }
            catch (XmlException e)
            {
                throw new XmlException("异常描述文件格式分析错误。", e);
            }
        }

        public DescriptionItem GetDescriptionItem(string businessName, int exceptionId)
        {
            if (_descriptionItemses.ContainsKey(businessName) == false)
            {
                return null;
            }

            var itemResult = _descriptionItemses[businessName].Where(b => b.Id == exceptionId).ToArray();
            return itemResult.Any() ? itemResult.ElementAt(0) : null;
        }
    }
}

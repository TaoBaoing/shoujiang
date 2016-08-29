using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace BasicFramework.Component
{
    public static class MapperExtensions
    {
        static readonly ObjectMapperManager mapperManager = EmitMapper.ObjectMapperManager.DefaultInstance;

        public static T To<T>(this object source)
        {
            return (T)mapperManager.GetMapperImpl(source.GetType(), typeof(T), DefaultMapConfig.Instance).Map(source);
        }

        /// <summary>
        ///模仿 Value Injecter的方式为原值注入值
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void Inject(this object target, object source)
        {
            throw new NotImplementedException("暂时还没实现，敬请期待");
        }
    }
}

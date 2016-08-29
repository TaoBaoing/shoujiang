using System.Configuration;

namespace BasicFramework.Component
{
    [ConfigurationCollection(typeof(ItemElement))]
    public class ItemsElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ItemElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ItemElement)element).Name;
        }

        public new ItemElement this[string name]
        {
            get
            {
                var ele = BaseGet(name);

                return (ItemElement)(ele);
            }
        }

        public ItemElement this[int index]
        {
            get
            {
                return (ItemElement)BaseGet(index);
            }
        }

        protected void BaseAdd(ItemElement element)
        {
            BaseAdd(element);
        }

        protected ItemElement GetElement(string name)
        {
            return (ItemElement)BaseGet(name);
        }
    }
}

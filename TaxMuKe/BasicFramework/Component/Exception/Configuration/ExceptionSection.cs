using System.Configuration;

namespace BasicFramework.Component
{
    public class ExceptionSection : ConfigurationSection
    {
        [ConfigurationProperty("items", IsDefaultCollection = true)]
        public ItemsElementCollection Items
        {
            get { return (ItemsElementCollection)base["items"]; }
        }
    }
}

namespace BasicFramework.Component
{
    public class ItemElement : System.Configuration.ConfigurationElement
    {
        [System.Configuration.ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }

        [System.Configuration.ConfigurationProperty("provider", IsRequired = true)]
        public string Provider
        {
            get { return this["provider"].ToString(); }
            set { this["provider"] = value; }
        }

        [System.Configuration.ConfigurationProperty("descriptionXml", IsRequired = true)]
        public string DescriptionXml
        {
            get { return this["descriptionXml"].ToString(); }
            set { this["descriptionXml"] = value; }
        }

        [System.Configuration.ConfigurationProperty("businessName", IsRequired = false)]
        public string BusinessName
        {
            get { return this["businessName"].ToString(); }
            set { this["businessName"] = value; }
        }
    }
}

namespace EletroGestao.Dominio.Core.Annotations
{
    public class TableAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public TableAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    public class ComplexWithCicularDependency
    {
        public ComplexWithCicularDependency CurcularProperty { get; set; }

        public static ComplexWithCicularDependency Create()
        {
            var instance = new ComplexWithCicularDependency();
            instance.CurcularProperty = instance;
            return new ComplexWithCicularDependency { CurcularProperty = instance };
        }
    }
}
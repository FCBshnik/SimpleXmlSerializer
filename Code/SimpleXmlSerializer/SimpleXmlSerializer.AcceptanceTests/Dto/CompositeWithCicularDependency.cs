namespace SimpleXmlSerializer.AcceptanceTests.Dto
{
    public class CompositeWithCicularDependency
    {
        public CompositeWithCicularDependency CurcularProperty { get; set; }

        public static CompositeWithCicularDependency Create()
        {
            var instance = new CompositeWithCicularDependency();
            instance.CurcularProperty = instance;
            return new CompositeWithCicularDependency { CurcularProperty = instance };
        }
    }
}
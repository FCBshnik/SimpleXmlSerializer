# SimpleXmlSerializer
Simple xml serialization library for .NET

# Features
Built-in serialization of:
- primitive types: bool, byte, int, long, float, double, decimal, char, string, TimeSpan, DateTime, DateTimeOffset, Guid, Uri, Type, Enums, Nullable<>
- collection types: Array, IList, List, ICollection, Collection, IEnumerable, IDictionary, Dictionary and theirs geneic analogs
- composite types with parameterless constructors

Providing xml elements names via:
- Xml* attributes (XmlRoot, XmlElement, XmlArray, XmlArrayItem, XmlAttribute, XmlIgnore)
- Data* attributes (DataContract, DataMember, DataCollection, IgnoreDataMember)

# Examples
Simplest scenario:
```
public class Company
{
    public string Name { get; set; }
    public IEnumerable<Employee> Employees { get; set; }
}

public class Employee
{
    public string FirstName { get; set; }
    public int Age { get; set; }
}
```
executing
```
var serializer = new XmlSerializer();
var xml = serializer.SerializeToString(new Company{ ... });
```
produces
```
<?xml version="1.0" encoding="utf-8"?>
<Company>
  <Name>Some company</Name>
  <Employees>
    <Add>
      <FirstName>John</FirstName>
      <Age>45</Age>
    </Add>
  </Employees>
</Company>
```

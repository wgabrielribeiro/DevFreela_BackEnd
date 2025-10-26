using Bogus;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Fakes;
public class FakeDataHelper
{
    private static readonly Faker _faker = new();

    public static Project CreateFakeProjectV1()
    {
        return new Project(
            _faker.Commerce.ProductName(),
            _faker.Lorem.Sentence(),
            _faker.Random.Int(1, 100),
            _faker.Random.Int(101, 200),
            _faker.Random.Decimal(1000, 10000)
        );
    }

    public static readonly Faker<Project> _projectFaker = new Faker<Project>()
        .CustomInstantiator(faker => new Project(
            faker.Commerce.ProductName(),
            faker.Lorem.Sentence(),
            faker.Random.Int(1, 100),
            faker.Random.Int(101, 200),
            faker.Random.Decimal(1000, 10000)
        ));

    private static readonly Faker<InsertProjectCommand> _insertProjectCommandFaker = new Faker<InsertProjectCommand>()
        .RuleFor(c => c.Title, faker => faker.Commerce.ProductName())
        .RuleFor(c => c.Description, faker => faker.Lorem.Sentence())
        .RuleFor(c => c.IdCliente, faker => faker.Random.Int(1, 100))
        .RuleFor(c => c.IdFreelancer, faker => faker.Random.Int(101, 200))
        .RuleFor(c => c.TotalCost, faker => faker.Random.Decimal(1000, 10000));

    public static Project CreateFakeProject() => _projectFaker.Generate();
    public static List<Project> CreateFakeProjectList() => _projectFaker.Generate(5);

    public static InsertProjectCommand CreateFakeInsertProjectCommand() => _insertProjectCommandFaker.Generate();

    //Precisamos realmente disso?
    public static DeleteProjectCommand CreateFakeDeleteProjectCommand(int id) => new(id);

}

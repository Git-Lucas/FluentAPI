//Fluent API é uma forma de composição de código, obedecendo determinada ordem de métodos
//Neste caso, há a implementação do Design Pattern "Builder"

var workflow = GitHubActionBuilder.Configure()
                                  .WithName("nome")
                                  .WithMoreNames("nome")
                                  .WithMoreNames("nome")
                                  .Step("step 01")
                                  .MoreStep("step 02")
                                  .MoreStep("step 03")
                                  .Build();

//Classe principal, que será a base para acessar os métodos (desde a instanciação da classe, até seus métodos ordenados: a ordem das interfaces importa)
public class GitHubActionBuilder : IWorkFlow, IWorkflowName, IStep, IBuild
{
    //Construtor privado, que impede a instanciação pública da classe (obriga a utilização dos métodos criados dentro da classe: que por si fará a instanciação da classe)
    private GitHubActionBuilder()
    {
    }

    //Como não será possível a instanciação da classe fora do método, é utilizado o modificador "static"
    //Método que fará a instanciação da classe
    public static IWorkFlow Configure() => new GitHubActionBuilder();

    //Com o IWorkFlow implementado através do "Configure()", é possível acessar seu método "IWorkflowName"
    //Builder entra em ação, pois obriga a ordem dos métodos
    public IWorkflowName WithName(string name) => this;

    //"WithName" retorna um "IWorkflowName", que por consequência, permite o método "WithMoreNames" retornando a si mesmo (isso traz o resultado de chamadas infitas do mesmo método)
    public IWorkflowName WithMoreNames(string name) => this;

    //Método presente também na classe "IWorkflowName", que retorna / direciona para a interface "IStep"
    public IStep Step(string step) => this;

    //Assim como o "WithMoreNames", existe um método que retorna a própria interface, permitindo várias chamadas ao método
    public IStep MoreStep(string step) => this;

    //Até que por fim, o método presente na interface "IStep", retorna um IBuild, com a condição de ser chamado um "IStep" anteriormente
    public IBuild Build() => this;
}

public interface IWorkFlow
{
    public IWorkflowName WithName(string name);
}

public interface IWorkflowName
{
    public IWorkflowName WithMoreNames(string name);
    public IStep Step(string step);
}

public interface IStep
{
    public IStep MoreStep(string step);
    public IBuild Build();
}

public interface IBuild
{
}
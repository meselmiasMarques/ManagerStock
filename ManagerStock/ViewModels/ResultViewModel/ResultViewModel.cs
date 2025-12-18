using Microsoft.AspNetCore.Identity;

namespace ManagerStock.ViewModels.ResultViewModel;

public class ResultViewModel<T>
{
    private readonly List<string> _errors = new();

    public ResultViewModel(T data)
    {
        Data = data;
    }

    public ResultViewModel(string error)
    {
        Errors.Add(error);
    }

    public ResultViewModel()
    {
        
    }

    public T Data { get; set; }

    private List<string> Errors => _errors;
}
using System.ComponentModel.DataAnnotations;

namespace ManagerStock.ViewModels.Product;

public record EditorProductViewModel(

    [Required(ErrorMessage = "Campo Nome é obrigatorio")]
    string? Name,

    [Required(ErrorMessage = "Campo Descrição é obrigatorio")]
    string? Description,

    [Required(ErrorMessage = "Campo Preço é obrigatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "Valor Minímo invalido")]
    decimal Price,

    [Required(ErrorMessage = "Campo Estoque é obrigatorio")]
    int StockAmout

);

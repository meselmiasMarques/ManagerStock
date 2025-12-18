using System.ComponentModel.DataAnnotations;

namespace ManagerStock.ViewModels.Product;

public record EditorStockViewModel(
    
    [Required(ErrorMessage = "Campo Estoque é obrigatorio")]
    int StockAmout 
    );
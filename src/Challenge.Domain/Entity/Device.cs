using Challenge.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Challenge.Domain.Entity;

[ExcludeFromCodeCoverage]
[Table("Device", Schema = "DBO")]
public class Device
{
    private readonly string _nameErrorMessage = "Name cannot be empty.";
    private readonly string _brandErrorMessage = "Invalid brand.";
    private readonly string _stateErrorMessage = "Invalid state.";

    public Device(string name, string brand, int state)
    {
        Name = name;
        Brand = brand;
        State = state;
        CreatedAt = DateTime.UtcNow;
    }

    public static Device Create(string name, string brand, int state)
        => new(name, brand, state);

    [Key]
    [Required]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [JsonPropertyName("name")]
    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        private set => SetName(value);
    }

    [Column("Brand")]
    [JsonPropertyName("brand")]
    private string _brand = string.Empty;
    public string Brand
    {
        get => _brand;
        private set => SetBrand(value);
    }

    [Column("State")]
    [JsonPropertyName("state")]
    private int _state;
    public int State
    {
        get => _state;
        private set => SetState(value);
    }

    [Column("CreatedAt")]
    [JsonPropertyName("createdAt")]
    private DateTime _createdAt;
    public DateTime CreatedAt
    {
        get => _createdAt;
        private set => _createdAt = value;
    }

    #region Public Methods

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(_nameErrorMessage);

        _name = name;
    }

    public void SetBrand(string brand)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException(_brandErrorMessage);

        _brand = brand;
    }

    public void SetState(int state)
    {
        if (!System.Enum.IsDefined(typeof(StateEnum), state))
            throw new ArgumentException(_stateErrorMessage);

        _state = state;
    }

    #endregion
}
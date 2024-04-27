namespace Domain.Entities;

public class User
{
    public string Identifier { get; set; }
    public string Name { get; set; }
    public uint Credits { get; set; }
    public float Grade { get; set; }
    public uint Year { get; set; }
}
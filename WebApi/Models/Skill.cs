﻿namespace WebApi.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public int Damage { get; set; } = 0;
    public List<Character> Characters { get; set; }
}
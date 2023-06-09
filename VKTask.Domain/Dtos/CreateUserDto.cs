﻿using System.ComponentModel.DataAnnotations;

namespace VKTask.Domain.Dtos;

public class CreateUserDto
{
    public string Login { get; set; }
    public string Password { get; set; }

    [Range(1,2, ErrorMessage ="Incorrect value")]
    public int UserGroupId { get; set; }
}
